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
            pass = textBox2.Text;
            Cryptographer cryptographer = new Cryptographer();
            cryptographer.encrypt(path, pass);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pass = textBox2.Text;
            Cryptographer cryptographer = new Cryptographer();
            cryptographer.decrypt(path, pass);
        }
    }
}
