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
using System.Collections;


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
            //openFileDialog1.InitialDirectory = "C:\\";
            openFileDialog1.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
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
                    while ((line = file.ReadLine()) != null){
                        listBox1.Items.Add(line);
                        counter++;
                        numDBs++;
                    }
                }
                catch (Exception ex)
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

        private void button4_Click(object sender, EventArgs e)
        {
            if (File.Exists(textBox1.Text)){
                if (Directory.Exists(textBox2.Text)){
                    if (Directory.Exists(textBox3.Text)){

                        MessageBox.Show("TODO CHIDO");


                    }
                    else {
                        MessageBox.Show("El direcctorio "+textBox3.Text+" no existe seleccione un directorio existente.");
                    }
                }else {
                    MessageBox.Show("El direcctorio " + textBox2.Text + " no existe seleccione un directorio existente.");
                }
            }else {
                MessageBox.Show("El archivo " + textBox1.Text + " no existe seleccione un archivo existente.");
            }


            /*
            int numMatches = 0;
            label13.Text = "# Coinidencias:";
            Stack stack = new Stack();
            String[] Databases = listBox1.Items.OfType<string>().ToArray();
            String[] Files = listBox2.Items.OfType<string>().ToArray();
            foreach (String Database in Databases)
            {
                foreach (String File in Files)
                {
                    if (File.Contains(Database))
                    {
                        stack.Push(File);
                    }
                }
            }
            foreach (String item in stack)
            {
                listBox3 .Items.Add(item);

            }
            numMatches = listBox3.Items.Count;

            
            string sPath = textBox3.Text+@"\save.txt";

            System.IO.StreamWriter SaveFile = new System.IO.StreamWriter(sPath);
            foreach (var item in listBox3.Items)
            {
                SaveFile.WriteLine(item.ToString()) ;
            }
            SaveFile.Close();
            label13.Text += " " + numMatches;
            */
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
