using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace ConectorBaseDatos
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            GetDataSources2();
        }

        private void GetDataSources2()
        {
            string ServerName = Environment.MachineName;
            RegistryView registryView = Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32;
            using (RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, registryView))
            {
                RegistryKey instanceKey = hklm.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names\SQL", false);
                if (instanceKey != null)
                {
                    foreach (var instanceName in instanceKey.GetValueNames())
                    {
                        comboBox1.Items.Add(ServerName + "\\" + instanceName);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label11.Text = "# BD's:";
            listBox1.Items.Clear();
            openFileDialog1.InitialDirectory = "C:\\";
            // filtro de archivos.
            openFileDialog1.Filter = "Archivos de texto (*.txt)|*.txt";
            // codigo para abrir el cuadro de dialogo
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string str_RutaArchivo = openFileDialog1.FileName;
                    string line;
                    System.IO.StreamReader file = new System.IO.StreamReader(@str_RutaArchivo);
                    textBox1.Text = str_RutaArchivo;
                    while ((line = file.ReadLine()) != null)
                    {
                        listBox1.Items.Add(line);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            label11.Text += " " + listBox1.Items.Count;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection cnn;
            string connetionString = "Data Source=" + comboBox1.Text + ";User ID=" + textBox5.Text + ";Password=" + textBox4.Text;
            cnn = new SqlConnection(connetionString);

            try
            {
                cnn.Open();
                foreach (string item in listBox1.Items) {
                    try {
                       // SqlCommand cmd = new SqlCommand(@"EXEC sp_attach_db @dbname = N'other_" + item + @"_metadata', @filename1 = N'C:\Program Files\Microsoft SQL Server\MSSQL14.COMPAC\MSSQL\DATA\other_" + item + @"_metadata.mdf', @filename2 = N'C:\Program Files\Microsoft SQL Server\MSSQL14.COMPAC\MSSQL\DATA\other_" + item + "_metadata.ldf';", cnn);
                        //SqlDataReader dr = cmd.ExecuteReader();

                        SqlCommand cmd = new SqlCommand(@"EXEC sp_attach_db @dbname = N'other_" + item + @"_content', @filename1 = N'C:\Program Files\Microsoft SQL Server\MSSQL14.COMPAC\MSSQL\DATA\other_" + item + @"_content.mdf', @filename2 = N'C:\Program Files\Microsoft SQL Server\MSSQL14.COMPAC\MSSQL\DATA\other_" + item + "_content.ldf';", cnn);
                        SqlDataReader dr = cmd.ExecuteReader();
                        //SqlCommand cmd2 = new SqlCommand(@"EXEC sp_attach_db @dbname = N'other_" + item + @"_metadata', @filename1 = N'C:\Program Files\Microsoft SQL Server\MSSQL14.COMPAC\MSSQL\DATA\other_" + item + "_metadata.ldf';", cnn);
                        //SqlDataReader dr2 = cmd2.ExecuteReader();
                        dr.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message+"GUID: "+item);
                    }
            


                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if ((textBox1.Text != "") && (textBox4.Text != "") && (textBox5.Text != "") && (comboBox1.Text != ""))
            {
                button4.Enabled = true;
            }
            else
            {
                button4.Enabled = false;
            }
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if ((textBox1.Text != "") && (textBox4.Text != "") && (textBox5.Text != "") && (comboBox1.Text != ""))
            {
                button4.Enabled = true;
            }
            else
            {
                button4.Enabled = false;
            }

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if ((textBox1.Text != "") && (textBox4.Text != "") && (textBox5.Text != "") && (comboBox1.Text != ""))
            {
                button4.Enabled = true;
            }
            else
            {
                button4.Enabled = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if ((textBox1.Text != "") && (textBox4.Text != "") && (textBox5.Text != "") && (comboBox1.Text != ""))
            {
                button4.Enabled = true;
            }
            else
            {
                button4.Enabled = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            if (xlApp == null)
            {
                MessageBox.Show("Excel no esta instalado en la maquina");
                return;
            }

            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            SqlConnection cnn;
            string connetionString = "Data Source=" + comboBox1.Text + ";User ID=" + textBox5.Text + ";Password=" + textBox4.Text;
            cnn = new SqlConnection(connetionString);


            try
            {
                cnn.Open();

                xlWorkSheet.Cells[1, 1] = "Nombre Base de datos";
                xlWorkSheet.Cells[1, 2] = "Estado";
                int cont = 2;
                foreach (string item in listBox1.Items)
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand("use master select name, state_desc from sys.databases where name like '%"+item+"%'", cnn);
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            xlWorkSheet.Cells[cont, 1] = dr[0].ToString();
                            xlWorkSheet.Cells[cont, 2] = dr[1].ToString();
                            cont++;
                        }
                        cont++;
                        dr.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message + "GUID: " + item);
                    }
                }

                xlWorkBook.SaveAs(textBox3.Text + @"\Banana.xls", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
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
        }
    }
}
