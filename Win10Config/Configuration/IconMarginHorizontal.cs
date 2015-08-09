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
    class IconMarginHorizontal : IConfiguration
    {
        private String sValue;
        private String sError;
        private String sRegistryKey = @"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics";
        private String sRegistryValue = "IconSpacing";

        public IconMarginHorizontal()
        {
            sValue = (String)Registry.GetValue(sRegistryKey, sRegistryValue, "");
        }

        public void changeValue()
        {
            String sTempValue = Input.inputString(getDisplayName(), "New Horizontal Icon Margin. Windows 10: -1710, Windows 7: -1710", sValue);
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
            return "Horizontal Icon Margin";
        }

        public string getDisplayValue()
        {
            return sValue;
        }

        public bool run()
        {
            try
            {
                Registry.SetValue(sRegistryKey, sRegistryValue, sValue);
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
