﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win10Config.Interfaces;
using Win10Config.Utils;

namespace Win10Config.Configuration
{
    class IconMarginVertical : IConfiguration
    {
        private String sValue;
        private String sError;
        private String sRegistryKey = @"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics";
        private String sRegistryValue = "IconVerticalSpacing";

        public IconMarginVertical()
        {
            sValue = (String)Registry.GetValue(sRegistryKey, sRegistryValue, "");
        }

        public void changeValue()
        {
            String sTempValue = Input.inputString(getDisplayName(), "New Vertical Icon Margin. Windows 10: -1710, Windows 7: -1125", sValue);
            try
            {
                Int32.Parse(sTempValue);
                sValue = sTempValue;
            }
            catch (Exception)
            {
                changeValue();
            }
        }

        public string getDisplayName()
        {
            return "Vertical Icon Margin";
        }

        public string getDisplayValue()
        {
            return sValue;
        }

        public bool run()
        {
            try
            {
                Registry.SetValue(sRegistryKey, sRegistryValue, sValue, RegistryValueKind.String);
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
