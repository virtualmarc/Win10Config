using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Win10Config.Configuration;
using Win10Config.Utils;

namespace Win10Config
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            MessageBox.Show("You need to run this Program as Administrator (Richt Click -> Run as Administrator) or some Options won't work!", "Administrator", MessageBoxButtons.OK, MessageBoxIcon.Information);

            lstOptions.Items.Add(new W10ListViewItem(new RegistredOwner()));
            lstOptions.Items.Add(new W10ListViewItem(new IconMarginHorizontal()));
            lstOptions.Items.Add(new W10ListViewItem(new IconMarginVertical()));
        }

        private void lstOptions_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lstOptions.SelectedItems.Count > 0)
            {
                W10ListViewItem item = (W10ListViewItem)lstOptions.Items[lstOptions.SelectedItems[0].Index];
                item.changeValue();
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            Boolean bError = false;
            String sErrorText = "";
            foreach (W10ListViewItem item in lstOptions.Items)
            {
                Boolean bSuccess;
                try
                {
                    bSuccess = item.config.run();
                }
                catch (Exception)
                {
                    bSuccess = false;
                }
                if (bSuccess)
                {
                    item.BackColor = Color.Green;
                }
                else
                {
                    item.BackColor = Color.Red;
                    bError = true;
                    sErrorText += item.config.getDisplayName();
                    sErrorText += ":\r\n";
                    sErrorText += item.config.getErrorText();
                    sErrorText += "\r\n\r\n";
                }
            }
            if (bError)
            {
                MessageBox.Show("At least one Option was not successful", "An Error occoured", MessageBoxButtons.OK, MessageBoxIcon.Error);
                new frmErrorLog(sErrorText).Show();
            }
            else
            {
                MessageBox.Show("All Options have completed successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
