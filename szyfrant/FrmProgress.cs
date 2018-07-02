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
    public partial class FrmProgress : Form
    {
        public FrmProgress()
        {
            InitializeComponent();
            startup();
        }

        void startup()
        {
            progressBar1.Style = ProgressBarStyle.Marquee;
        }

        private void FrmProgress_Load(object sender, EventArgs e)
        {

        }
    }
}
