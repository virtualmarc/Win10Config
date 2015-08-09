using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Win10Config.Utils
{
    class Input
    {
        /// <summary>
        /// Show String Input Dialog
        /// </summary>
        /// <param name="sTitle">Dialog Title</param>
        /// <param name="sText">Dialog Text</param>
        /// <param name="sDefault">Default Value</param>
        /// <returns>Input Text</returns>
        public static String inputString(String sTitle, String sText, String sDefault)
        {
            String sValue = Interaction.InputBox(sText, sTitle, sDefault);
            if (sValue == "")
            {
                return sDefault;
            }
            return sValue;
        }
    }
}
