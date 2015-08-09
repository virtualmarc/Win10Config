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
    class WebSearchUnknownTypes : IConfiguration
    {
        private Boolean bRun = false;
        private String sError;
        private String sRegistryKey = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer";
        private String sRegistryValue = "NoInternetOpenWith";

        public void changeValue()
        {
            bRun = Input.inputBoolean(getDisplayName(), "Disable WebSearch for unknown file types?", bRun);
        }

        public string getDisplayName()
        {
            return "Disable WebSearch for unknown file types";
        }

        public string getDisplayValue()
        {
            return bRun ? "Yes" : "No";
        }

        public bool run()
        {
            try
            {
                Registry.SetValue(sRegistryKey, sRegistryValue, bRun ? 0x1 : 0x0, RegistryValueKind.DWord);
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
