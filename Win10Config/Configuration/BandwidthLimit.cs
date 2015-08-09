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
    class BandwidthLimit : IConfiguration
    {
        private Boolean bRun = false;
        private String sError;
        private String sRegistryKey = @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Psched";
        private String sRegistryValue = "NonBestEffortLimit";

        public void changeValue()
        {
            bRun = Input.inputBoolean(getDisplayName(), "Disable Bandwidth Limit?", bRun);
        }

        public string getDisplayName()
        {
            return "Disable Bandwidth Limit";
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
                    Registry.SetValue(sRegistryKey, sRegistryValue, 0x0, RegistryValueKind.DWord);
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
