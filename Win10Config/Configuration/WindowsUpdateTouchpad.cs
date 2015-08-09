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
    class WindowsUpdateTouchpad : IConfiguration
    {
        private Boolean bRun = false;
        private String sError = "";

        public void changeValue()
        {
            bRun = Input.inputBoolean(getDisplayName(), "Disable Touchpad Drivers (Synaptics and ELAN) from Windows Update?", bRun);
        }

        public string getDisplayName()
        {
            return "Disable Touchpad Drivers (Synaptics and ELAN) from Windows Update";
        }

        public string getDisplayValue()
        {
            return bRun ? "Yes" : "No";
        }

        public string getErrorText()
        {
            return sError;
        }

        public bool run()
        {
            try
            {
                if (bRun)
                {
                    PowerShell ps = PowerShell.Create();
                    ps.AddScript("Set-ExecutionPolicy Unrestricted -Force \r\n" +
                        "Import-Module PSWindowsUpdate \r\n" +
                        "Hide-WUUpdate -Title \"Synaptics*\" -HideStatus:$true -Confirm:$false \r\n" +
                        "Hide-WUUpdate -Title \"ELAN*\" -HideStatus:$true -Confirm:$false");
                    try {
                        ps.Invoke();
                    } catch (Exception) { }
                }
                return true;
            }
            catch (Exception ex)
            {
                sError += ex.Message;
                return false;
            }
        }
    }
}
