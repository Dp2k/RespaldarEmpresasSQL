using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

                SqlCommand cmd = new SqlCommand("use nomgenerales select NombreEmpresa, RutaEmpresa, GUIDDSL from nom10000 ", cnn);
                SqlDataReader dr = cmd.ExecuteReader();
               
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
