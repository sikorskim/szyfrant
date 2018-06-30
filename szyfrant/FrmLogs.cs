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
    public partial class FrmLogs : Form
    {
        public FrmLogs()
        {
            InitializeComponent();
            startup();
        }

        void startup()
        {
            dataGridView1.DataSource = Logger.getData();
        }

        private void FrmLogs_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            exportLog();
        }

        void exportLog()
        {
            string destFile;

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.AddExtension = true;
            saveFileDialog.ValidateNames = true;
            saveFileDialog.Filter = "Pliki XML| *.xml";
            saveFileDialog.FileName = "logs_"+DateTime.Now.ToShortDateString();
            
            DialogResult dialogResult = saveFileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                destFile = saveFileDialog.FileName;
                Logger.exportLogFile(destFile);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
