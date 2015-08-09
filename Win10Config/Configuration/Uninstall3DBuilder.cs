using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win10Config.Interfaces;
using Win10Config.Utils;

namespace Win10Config.Configuration
{
    class Uninstall3DBuilder : IConfiguration
    {
        private Boolean bRun = false;
        private String sError = "";

        public void changeValue()
        {
            bRun = Input.inputBoolean(getDisplayName(), "Uninstall 3D Builder?", bRun);
        }

        public string getDisplayName()
        {
            return "Uninstall 3D Builder";
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
                    App.uninstall("Microsoft.3DBuilder");
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
