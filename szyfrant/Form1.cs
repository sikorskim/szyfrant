using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace szyfrant
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string path;
        string pass;

        string openFile()
        {
            string file = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            DialogResult dialogResult = openFileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                file = openFileDialog.FileName;
            }
            else
            {
                MessageBox.Show("Nie wybrano pliku!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                file = null;
            }

            textBox1.Text = file;

            return file;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            path=openFile();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string destFile;
            pass = textBox2.Text;

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.AddExtension = true;
            saveFileDialog.ValidateNames = true;
            saveFileDialog.Filter = "Zaszyfrowane| *.sfr";

            DialogResult dialogResult = saveFileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                destFile = saveFileDialog.FileName;
                Cryptographer cryptographer = new Cryptographer();
                cryptographer.encrypt(path, pass, destFile);
            }
            else
            {
                MessageBox.Show("Wystąpił błąd! Spróbuj ponownie.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pass = textBox2.Text;           

            string destPath = null;

            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            DialogResult dialogResult = folderBrowserDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                destPath = folderBrowserDialog.SelectedPath;
                Cryptographer cryptographer = new Cryptographer();
                cryptographer.decrypt(path, pass, destPath);
            }
            else
            {
                MessageBox.Show("Wystąpił błąd! Spróbuj ponownie.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }
        //void zipFile()
        //{
        //    string destinationFile = null;
        //    string sourceDirectory = tempDirectory;
        //    SaveFileDialog saveFileDialog = new SaveFileDialog();
        //    saveFileDialog.ValidateNames = true;

        //    DialogResult dialogResult = saveFileDialog.ShowDialog();

        //    if (dialogResult == DialogResult.OK)
        //    {
        //        destinationFile = saveFileDialog.FileName;
        //    }
        //    else
        //    {
        //        MessageBox.Show("Wystąpił błąd! Spróbuj ponownie.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
    }
}
