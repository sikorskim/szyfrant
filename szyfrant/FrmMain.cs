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

            string version = " wersja 1.01";
            label2.Text = "Szyfrant" + version + " \u00a9 Computerman 2018";
            this.Text += version;
            textBox2.UseSystemPasswordChar = true;
            button3.Enabled = false;
            button4.Enabled = false;
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
                string fileExt = file.Substring(file.Length - 5, 5);

                if (fileExt == ".szfr")
                {
                    button3.Enabled = false;
                    button4.Enabled = true;
                }
                else
                {
                    button3.Enabled = true;
                    button4.Enabled = false;
                }
            }

            textBox1.Text = file;

            return file;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            path = openFile();
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
            folderBrowserDialog.RootFolder = Environment.SpecialFolder.DesktopDirectory;
            DialogResult dialogResult = folderBrowserDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                destPath = folderBrowserDialog.SelectedPath;
                Cryptographer cryptographer = new Cryptographer();
                if (cryptographer.decrypt(path, pass, destPath))
                {
                    MessageBox.Show("Deszyfrowanie zakończone sukcesem.");
                }
                else
                {
                    MessageBox.Show("Nieprawidłowe hasło!");
                }
            }
            uiReset();
        }

        void encrypt()
        {
            string destFile;
            pass = textBox2.Text;

            if (checkPassLength(pass))
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.AddExtension = true;
                saveFileDialog.ValidateNames = true;
                saveFileDialog.Filter = "Zaszyfrowane| *.szfr";
                saveFileDialog.FileName = DateTime.Now.ToFileTime().ToString();

                DialogResult dialogResult = saveFileDialog.ShowDialog();

                if (dialogResult == DialogResult.OK)
                {
                    destFile = saveFileDialog.FileName;
                    Cryptographer cryptographer = new Cryptographer();
                    if (cryptographer.encrypt(path, pass, destFile))
                    {
                        MessageBox.Show("Szyfrowanie zakończone sukcesem.");
                    }
                    else
                    {
                        MessageBox.Show("Wystąpił błąd. Proszę spróbować ponownie.");
                    }
                }
                uiReset();
            }
            else
            {
                MessageBox.Show("Hasło powinno składać się z minimum 8 znaków!");
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

        void uiReset()
        {
            path = "";
            textBox1.Clear();
            textBox2.Clear();
            button3.Enabled = false;
            button4.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = PasswordGenerator.generate(32);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length > 0)
            {
                Clipboard.SetText(textBox2.Text);
            }
        }

        bool checkPassLength(string pass)
        {
            if (pass.Length < 8)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
