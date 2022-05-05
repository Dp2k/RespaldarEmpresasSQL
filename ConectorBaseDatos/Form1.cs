using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Collections;

namespace ConectorBaseDatos{
    public partial class Form1 : Form{
        
        public Form1(){
            InitializeComponent();
            GetDataSources2();
        }

   
        private void GetDataSources2(){
            string ServerName = Environment.MachineName;
            RegistryView registryView = Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32;
            using (RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, registryView)){
                RegistryKey instanceKey = hklm.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names\SQL", false);
                if (instanceKey != null){
                    foreach (var instanceName in instanceKey.GetValueNames()){
                        comboBox1.Items.Add(ServerName + "\\" + instanceName);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e){
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            if (FBD.ShowDialog() == DialogResult.OK){
                string[] files = Directory.GetFiles(FBD.SelectedPath);
                textBox3.Text = FBD.SelectedPath;
            }
        }


        private void button1_Click(object sender, EventArgs e){
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            if (xlApp == null){
                MessageBox.Show("Excel no esta instalado en la maquina, no se puede generar el reporte.");
                return;


            }



            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            SqlConnection cnn;
            string connetionString = "Data Source=" + comboBox1.Text + ";User ID=" + textBox1.Text + ";Password=" +textBox2.Text;
            cnn = new SqlConnection(connetionString);
            if (comboBox2.Text == "CONTPAQ i Nominas")
            {
                try
                {
                    cnn.Open();

                    SqlCommand cmd = new SqlCommand("use nomgenerales select NombreEmpresa, RutaEmpresa, GUIDDSL from nom10000 ", cnn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    xlWorkSheet.Cells[1, 1] = "NombreEmpresa";
                    xlWorkSheet.Cells[1, 2] = "NombreBaseDatos";
                    xlWorkSheet.Cells[1, 3] = "ID ADD BasesDatos";

                    int cont = 2;
                    while (dr.Read())
                    {
                        xlWorkSheet.Cells[cont, 1] = dr[0].ToString();
                        xlWorkSheet.Cells[cont, 2] = dr[1].ToString();
                        xlWorkSheet.Cells[cont, 3] = dr[2].ToString();
                        cont++;
                    }

                    xlWorkBook.SaveAs(textBox3.Text + @"\ReporteNominasDB.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                    xlWorkBook.Close(true, misValue, misValue);
                    xlApp.Quit();
                    MessageBox.Show("Archivo Generado Con Exito");

                    Marshal.ReleaseComObject(xlWorkSheet);
                    Marshal.ReleaseComObject(xlWorkBook);
                    Marshal.ReleaseComObject(xlApp);

                    cnn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }else {
                try{

                    cnn.Open();

                    SqlCommand cmd = new SqlCommand("use generalessql select Nombre,AliasBDD from listaempresas ", cnn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    
                    xlWorkSheet.Cells[1, 1] = "NombreEmpresa";
                    xlWorkSheet.Cells[1, 2] = "NombreBaseDatos";
                    xlWorkSheet.Cells[1, 3] = "ID ADD BasesDatos";

                    int cont = 2;
                   
                    Stack nombresEmpresas = new Stack();
                    Stack nombresDB = new Stack();

                    while (dr.Read()){
                        nombresEmpresas.Push(dr[0].ToString());
                        nombresDB.Push(dr[1].ToString());
                    }
                    dr.Close();
                     
                    foreach (string db in nombresDB) {
                        try {
                            SqlCommand cmd2 = new SqlCommand("use " + db + " select guiddsl from parametros", cnn);
                            SqlDataReader dr2 = cmd2.ExecuteReader();
                            dr2.Read();
                            xlWorkSheet.Cells[cont, 1] = nombresEmpresas.Pop();
                            xlWorkSheet.Cells[cont, 2] = db;
                            xlWorkSheet.Cells[cont, 3] = dr2[0].ToString();
                            cont++;
                            dr2.Close();
                        } catch(Exception){
                            xlWorkSheet.Cells[cont, 1] = nombresEmpresas.Pop();
                            xlWorkSheet.Cells[cont, 2] = db;
                            xlWorkSheet.Cells[cont, 3] = "empresa no existe";
                            cont++;

                        }
                        
                       
                    }
                    xlWorkBook.SaveAs(textBox3.Text + @"\ReporteContabilidadDB.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                    xlWorkBook.Close(true, misValue, misValue);
                    xlApp.Quit();
                    MessageBox.Show("Archivo Generado Con Exito");

                    Marshal.ReleaseComObject(xlWorkSheet);
                    Marshal.ReleaseComObject(xlWorkBook);
                    Marshal.ReleaseComObject(xlApp);

                    cnn.Close();

                }
                catch (Exception ex){
                    MessageBox.Show("Error: " + ex.Message);
                }
            
            
            } 
        }

        private void textBox1_TextChanged(object sender, EventArgs e){
            button1.Enabled = (textBox1.Text != "") && (textBox2.Text != "") && (textBox3.Text != "") && (comboBox1.Text != "") && (comboBox2.Text != "") ? true : false;
        }

        private void textBox2_TextChanged(object sender, EventArgs e){
            button1.Enabled = (textBox1.Text != "") && (textBox2.Text != "") && (textBox3.Text != "") && (comboBox1.Text != "") && (comboBox2.Text != "") ? true : false;
        }

        private void textBox3_TextChanged(object sender, EventArgs e){
            button1.Enabled = (textBox1.Text != "") && (textBox2.Text != "") && (textBox3.Text != "") && (comboBox1.Text != "") && (comboBox2.Text != "") ? true : false;
        }

        private void comboBox1_TextChanged(object sender, EventArgs e){
            button1.Enabled = (textBox1.Text != "") && (textBox2.Text != "") && (textBox3.Text != "") && (comboBox1.Text != "") && (comboBox2.Text != "") ? true : false;
        }

        private void comboBox2_TextChanged(object sender, EventArgs e){
            button1.Enabled = (textBox1.Text != "") && (textBox2.Text != "") && (textBox3.Text != "") && (comboBox1.Text != "") && (comboBox2.Text != "") ? true : false;
        }

        private void respaldarBasesDeDatosToolStripMenuItem_Click(object sender, EventArgs e){
            bool isOpen = false;
            foreach (Form f in Application.OpenForms) {
                if (f.Text == "Form2") {
                    isOpen = true;
                    f.Focus();
                    break;
                }
            }
            if (!isOpen) {
                Form2 f2 = new Form2();
                f2.ShowDialog();
            }
        }



        private void attachListaBasesDeDatosToolStripMenuItem_Click(object sender, EventArgs e){
            bool isOpen = false;
            foreach (Form f in Application.OpenForms){
                if (f.Text == "Form3") {
                    isOpen = true;
                    f.Focus();
                    break;
                }
            }
            if (!isOpen){
                Form3 f3 = new Form3();
                f3.ShowDialog();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e){
            textBox1.Enabled = (checkBox1.CheckState == CheckState.Checked) ? false : true;
            textBox2.Enabled = (checkBox1.CheckState == CheckState.Checked) ? false : true;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = checkBox2.CheckState == CheckState.Checked ? false : true;
        }
    }
}
