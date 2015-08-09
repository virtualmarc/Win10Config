using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Win10Config.Utils
{
    public partial class frmErrorLog : Form
    {
        public frmErrorLog(String sLog)
        {
            InitializeComponent();
            txtLog.Text = sLog;
        }

        private void frmErrorLog_Load(object sender, EventArgs e)
        {

        }
    }
}
