using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using Win10Config.Interfaces;
using Win10Config.Utils;

namespace Win10Config.Configuration
{
    class TippsForWindows : IConfiguration
    {
        private Boolean bRun = false;
        private String sError;

        public void changeValue()
        {
            bRun = Input.inputBoolean(getDisplayName(), "Disable Tipps for Windows?", bRun);
        }

        public string getDisplayName()
        {
            return "Disable Tipps for Windows";
        }

        public string getDisplayValue()
        {
            return bRun ? "Yes" : "No";
        }

        public bool run()
        {
            try
            {
                if (bRun)
                {
                    Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\SoftLanding", "Enabled", 0x0, RegistryValueKind.DWord);
                }
                return true;
            }
            catch (Exception ex)
            {
                sError = ex.Message;
                return false;
            }
        }

        public string getErrorText()
        {
            return sError;
        }
    }
}
