using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConectorBaseDatos
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            label11.Text = "# BD's:";
            openFileDialog1.InitialDirectory = "C:\\";
            // filtro de archivos.
            openFileDialog1.Filter = "Archivos de texto (*.txt)|*.txt";
            int numDBs = 0;
            // codigo para abrir el cuadro de dialogo
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string str_RutaArchivo = openFileDialog1.FileName;
                    int counter = 0;
                    string line;
                    System.IO.StreamReader file = new System.IO.StreamReader(@str_RutaArchivo);
                    textBox1.Text = str_RutaArchivo;
                    while ((line = file.ReadLine()) != null)
                    {
                        listBox1.Items.Add(line);
                        counter++;
                        numDBs++;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            
            label11.Text += " " + numDBs;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int numFiles = 0;
            listBox2.Items.Clear();
            label8.Text = "# Archivos:";
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            if (FBD.ShowDialog() == DialogResult.OK)
            {
                string[] files = Directory.GetFiles(FBD.SelectedPath);
                textBox2.Text = FBD.SelectedPath;
                foreach (string file in files)
                {
                    listBox2.Items.Add(Path.GetFileName(file));
                    numFiles++;
                }
            }
            label8.Text += " " + numFiles;
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            if (FBD.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = FBD.SelectedPath;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if ((textBox1.Text != "") && (textBox2.Text != "") && (textBox3.Text != "") && (comboBox1.Text != ""))
            {
                button4.Enabled = true;
            }
            else {
                button4.Enabled = false;
            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if ((textBox1.Text != "") && (textBox2.Text != "") && (textBox3.Text != "") && (comboBox1.Text != ""))
            {
                button4.Enabled = true;
            }
            else
            {
                button4.Enabled = false;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if ((textBox1.Text != "") && (textBox2.Text != "") && (textBox3.Text != "") && (comboBox1.Text != ""))
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
            if ((textBox1.Text != "") && (textBox2.Text != "") && (textBox3.Text != "") && (comboBox1.Text != ""))
            {
                button4.Enabled = true;
            }
            else
            {
                button4.Enabled = false;
            }
        }
    }
}
