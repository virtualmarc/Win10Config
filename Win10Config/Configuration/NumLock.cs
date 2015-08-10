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
    class NumLock : IConfiguration
    {
        private Boolean bRun = false;
        private String sError;
        private String sRegistryKey = @"HKEY_USERS\.DEFAULT\Control Panel\Keyboard";
        private String sRegistryValue = "InitialKeyboardIndicators"; // 0x1

        public NumLock()
        {
            try {
                bRun = (String)Registry.GetValue(sRegistryKey, sRegistryValue, "2147483648") == "2147483650";
            }
            catch (Exception)
            {
                bRun = false;
            }
        }

        public void changeValue()
        {
            bRun = Input.inputBoolean(getDisplayName(), "Enable Num Lock on Boot?", bRun);
        }

        public string getDisplayName()
        {
            return "Enable Num Lock on Boot";
        }

        public string getDisplayValue()
        {
            return bRun ? "Yes" : "No";
        }

        public bool run()
        {
            try
            {
                Registry.SetValue(sRegistryKey, sRegistryValue, bRun ? "2147483650" : "2147483648", RegistryValueKind.String);
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
