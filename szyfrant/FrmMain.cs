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
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            startup();
        }

        string path;
        string pass;

        void startup()
        {
            label2.Text = "\u00a9 Computerman 2018";
            textBox2.UseSystemPasswordChar = true;
        }

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

            textBox1.Text = file;

            return file;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            path=openFile();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            encrypt();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            decrypt();
        }

        void decrypt()
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
        }

        void encrypt()
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
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void zakończToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = !checkBox1.Checked;
        }

        private void logToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmLogs frmLogs = new FrmLogs();
            frmLogs.ShowDialog();
        }

        private void ustawieniaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSettings frmSettings = new FrmSettings();
            frmSettings.ShowDialog();
        }
    }
}
