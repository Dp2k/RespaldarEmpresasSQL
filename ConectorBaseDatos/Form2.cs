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
            int numDBs = 0;
            // Directorio inicial Escritorio
            openFileDialog1.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            // filtro de archivos.
            openFileDialog1.Filter = "Archivos de texto (*.txt)|*.txt";
            
            // codigo para abrir el cuadro de dialogo
            if (openFileDialog1.ShowDialog() == DialogResult.OK){
                try{
                    string str_RutaArchivo = openFileDialog1.FileName;
                    string line;
                    System.IO.StreamReader file = new System.IO.StreamReader(@str_RutaArchivo);
                    textBox1.Text = str_RutaArchivo;
                    while ((line = file.ReadLine()) != null){
                        listBox1.Items.Add(line);
                        numDBs++;
                    }
                }
                catch (Exception){
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

                        label13.Text = "# Coinidencias:";
                        Stack matches = new Stack();
                        List<List<String>> listalistas = new List<List<string>>();
                        String[] Databases = listBox1.Items.OfType<string>().ToArray();
                        String[] Files = listBox2.Items.OfType<string>().ToArray();

                        foreach (String Database in Databases){
                            List<String> list = new List<string>();
                           
                            foreach (String File in Files){   
                                if (File.Contains(Database)){
                                    list.Add(File);   
                                }
                            }

                            listalistas.Add(list);
                        }

                        string sourcePath = @textBox2.Text;
                        string targetPath = @textBox3.Text;

                        try {
                            for (int i = 0; i < listalistas.Count; i++){
                                for (int j = 0; j < listalistas[i].Count; j++){
                                    listBox3.Items.Add(listalistas[i][j]);
                                    string sourceFile = System.IO.Path.Combine(sourcePath, listalistas[i][j]);
                                    string destFile = System.IO.Path.Combine(targetPath, listalistas[i][j]);
                                    System.IO.File.Copy(sourceFile, destFile, true);
                                }
                            }
                            MessageBox.Show("Se han respaldado los archivos correctamente en el direcctorio: "+ targetPath);
                        }
                        catch (Exception ex) {
                            MessageBox.Show(ex.Message);
                        }
                        


                      

                        label13.Text += " " + listBox3.Items.Count;

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

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
