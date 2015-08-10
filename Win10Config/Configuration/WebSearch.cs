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
    class WebSearch : IConfiguration
    {
        private Boolean bRun = false;
        private String sError;
        private String sRegistryKey = @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Windows Search";
        private String sRegistryValue = "DisableWebSearch"; // 0x1
        private String sRegistryValue2 = "ConnectedSearchUseWeb"; // 0x0
        private String sRegistryValue3 = "ConnectedSearchUseWebOverMeteredConnections"; // 0x0

        public WebSearch()
        {
            try {
                bRun = (int)Registry.GetValue(sRegistryKey, sRegistryValue, 0x0) == 0x1;
            }
            catch (Exception)
            {
                bRun = false;
            }
        }

        public void changeValue()
        {
            bRun = Input.inputBoolean(getDisplayName(), "Disable Web Search?", bRun);
        }

        public string getDisplayName()
        {
            return "Disable Web Search";
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
                Registry.SetValue(sRegistryKey, sRegistryValue2, bRun ? 0x0 : 0x1, RegistryValueKind.DWord);
                Registry.SetValue(sRegistryKey, sRegistryValue3, bRun ? 0x0 : 0x1, RegistryValueKind.DWord);
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
