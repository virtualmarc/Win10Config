﻿using Microsoft.Win32;
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
    class DisableLoginOptions : IConfiguration
    {
        private Boolean bRun = false;
        private String sError;
        private String sRegistryKey = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\PolicyManager\default\Settings";
        private String sRegistryValue = "AllowSignInOptions";

        public DisableLoginOptions()
        {
            try {
                bRun = (int)Registry.GetValue(sRegistryKey, sRegistryValue, 0x1) == 0x0;
            }
            catch (Exception)
            {
                bRun = false;
            }
        }

        public void changeValue()
        {
            bRun = Input.inputBoolean(getDisplayName(), "Disable Signin Options (PIN, Image, etc)?", bRun);
        }

        public string getDisplayName()
        {
            return "Disable Signin Options (PIN, Image, etc)";
        }

        public string getDisplayValue()
        {
            return bRun ? "Yes" : "No";
        }

        public bool run()
        {
            try
            {
                Registry.SetValue(sRegistryKey, sRegistryValue, bRun ? 0x0 : 0x1, RegistryValueKind.DWord);
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
