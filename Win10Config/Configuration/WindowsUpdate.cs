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
    class WindowsUpdate : IConfiguration
    {
        private Boolean bRun = false;
        private String sError;

        public void changeValue()
        {
            bRun = Input.inputBoolean(getDisplayName(), "Configure Windows Update?", bRun);
        }

        public string getDisplayName()
        {
            return "Configure Windows Update";
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
                    // Auto Reboot
                    Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\WindowsUpdate\UX\Settings", "UxOption", 0x0, RegistryValueKind.DWord);
                    // Defer Upgrade
                    Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\WindowsUpdate\UX\Settings", "DeferUpgrade", 0x0, RegistryValueKind.DWord);
                    // Torrent
                    Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\DeliveryOptimization", "SystemSettingsDownloadMode", 0x1, RegistryValueKind.DWord);
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
