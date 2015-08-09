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
    class AskRunExeMsi : IConfiguration
    {
        private Boolean bRun = false;
        private String sError;
        private String sRegistryKey = @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Associations";
        private String sRegistryValue = "LowRiskFileTypes";

        public void changeValue()
        {
            bRun = Input.inputBoolean(getDisplayName(), "Disable Prompt to run .exe and .msi?", bRun);
        }

        public string getDisplayName()
        {
            return "Disable Prompt to run .exe and .msi";
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
                    Registry.SetValue(sRegistryKey, sRegistryValue, ".exe;.msi;", RegistryValueKind.String);
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
