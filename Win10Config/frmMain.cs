using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management.Automation;
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
            // Dangerous, don't do this at Home ;)
            CheckForIllegalCrossThreadCalls = false;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            MessageBox.Show("You need to run this Program as Administrator (Richt Click -> Run as Administrator) or some Options won't work!", "Administrator", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MessageBox.Show("Run at your own risk. I assume no liability for damages or loss of data. This Program was only tested with Windows 10 Pro, not ome or Enterprise.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            lstOptions.Items.Add(new W10ListViewItem(new RegistredOwner()));
            lstOptions.Items.Add(new W10ListViewItem(new IconMarginHorizontal()));
            lstOptions.Items.Add(new W10ListViewItem(new IconMarginVertical()));
            lstOptions.Items.Add(new W10ListViewItem(new ActivateDarkTheme()));
            lstOptions.Items.Add(new W10ListViewItem(new LockScreen()));
            lstOptions.Items.Add(new W10ListViewItem(new NumLock()));
            lstOptions.Items.Add(new W10ListViewItem(new Cortana()));
            lstOptions.Items.Add(new W10ListViewItem(new WebSearch()));
            lstOptions.Items.Add(new W10ListViewItem(new BlockMSAccount()));
            lstOptions.Items.Add(new W10ListViewItem(new DisableLoginOptions()));
            lstOptions.Items.Add(new W10ListViewItem(new BandwidthLimit()));
            lstOptions.Items.Add(new W10ListViewItem(new AskRunExeMsi()));
            lstOptions.Items.Add(new W10ListViewItem(new ProgramCompatibility()));
            lstOptions.Items.Add(new W10ListViewItem(new WebSearchUnknownTypes()));
            lstOptions.Items.Add(new W10ListViewItem(new Privacy()));
            lstOptions.Items.Add(new W10ListViewItem(new WindowsUpdate()));
            lstOptions.Items.Add(new W10ListViewItem(new Startmenu()));
            lstOptions.Items.Add(new W10ListViewItem(new TippsForWindows()));
            lstOptions.Items.Add(new W10ListViewItem(new OneDrive()));
            lstOptions.Items.Add(new W10ListViewItem(new Uninstall3DBuilder()));
            lstOptions.Items.Add(new W10ListViewItem(new UninstallBingFinance()));
            lstOptions.Items.Add(new W10ListViewItem(new UninstallBingNews()));
            lstOptions.Items.Add(new W10ListViewItem(new UninstallBingSports()));
            lstOptions.Items.Add(new W10ListViewItem(new UninstallBingWeather()));
            lstOptions.Items.Add(new W10ListViewItem(new UninstallGetStarted()));
            lstOptions.Items.Add(new W10ListViewItem(new UninstallGetOffice()));
            lstOptions.Items.Add(new W10ListViewItem(new UninstallSolitaire()));
            lstOptions.Items.Add(new W10ListViewItem(new UninstallOneNote()));
            lstOptions.Items.Add(new W10ListViewItem(new UninstallPeople()));
            lstOptions.Items.Add(new W10ListViewItem(new UninstallPhotos()));
            lstOptions.Items.Add(new W10ListViewItem(new UninstallAlarms()));
            lstOptions.Items.Add(new W10ListViewItem(new UninstallCalculator()));
            lstOptions.Items.Add(new W10ListViewItem(new UninstallCamera()));
            lstOptions.Items.Add(new W10ListViewItem(new UninstallMaps()));
            lstOptions.Items.Add(new W10ListViewItem(new UninstallPhone()));
            lstOptions.Items.Add(new W10ListViewItem(new UninstallSoundRecorder()));
            lstOptions.Items.Add(new W10ListViewItem(new UninstallXbox()));
            lstOptions.Items.Add(new W10ListViewItem(new UninstallMusic()));
            lstOptions.Items.Add(new W10ListViewItem(new UninstallVideo()));
            lstOptions.Items.Add(new W10ListViewItem(new WindowsUpdateNvidia()));
            lstOptions.Items.Add(new W10ListViewItem(new WindowsUpdateTouchpad()));
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
            if (!bgwRun.IsBusy)
            {
                btnRun.Enabled = false;
                bgwRun.RunWorkerAsync();
            }
        }

        private void bgwRun_DoWork(object sender, DoWorkEventArgs e)
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
                bgwRun.ReportProgress(100, new Object[] { false, sErrorText });
            }
            else
            {
                MessageBox.Show("All Options have completed successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (MessageBox.Show("To apply the settings you need to restart your computer. Would you like to restart now?", "Restart neede", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Process.Start("shutdown.exe", "-r -t 0");
            }
            btnRun.Invoke((Action)(() => { btnRun.Enabled = true; }));
        }

        private void bgwRun_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try {
                if (e.ProgressPercentage == 100 && (bool)((Object[])e.UserState)[0] == false)
                {
                    new frmErrorLog((String)((Object[])e.UserState)[1]).Show();
                }
            } catch (Exception) { }
        }
    }
}
