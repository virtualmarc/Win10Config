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
    class RegistredOwner : IConfiguration
    {
        private String sOwnerName;
        private String sError;
        private String sRegistryKey = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion";
        private String sRegistryValue = "RegisteredOwner";

        public RegistredOwner()
        {
            sOwnerName = (String)Registry.GetValue(sRegistryKey, sRegistryValue, "");
        }

        public void changeValue()
        {
            sOwnerName = Input.inputString(getDisplayName(), "Name of the registred Owner", sOwnerName);
        }

        public string getDisplayName()
        {
            return "Registred Owner Name";
        }

        public string getDisplayValue()
        {
            return sOwnerName;
        }

        public string getErrorText()
        {
            return sError;
        }

        public bool run()
        {
            try
            {
                Registry.SetValue(sRegistryKey, sRegistryValue, sOwnerName, RegistryValueKind.String);
                return true;
            }
            catch (Exception ex)
            {
                sError = ex.Message;
                return false;
            }
        }
    }
}
