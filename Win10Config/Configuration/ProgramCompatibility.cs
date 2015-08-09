using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Win10Config.Interfaces;
using Win10Config.Utils;

namespace Win10Config.Configuration
{
    class ProgramCompatibility : IConfiguration
    {
        private Boolean bRun = false;
        private String sError;

        public void changeValue()
        {
            bRun = Input.inputBoolean(getDisplayName(), "Disable Prompt if Setup was successful?", bRun);
        }

        public string getDisplayName()
        {
            return "Disable Prompt if Setup was successful";
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
                    ServiceController service = new ServiceController("PcaSvc");
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped);
                    Process.Start("sc.exe", "config PcaSvc start=disabled");
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
