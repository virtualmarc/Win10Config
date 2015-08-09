using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Windows.Forms;

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

        /// <summary>
        /// Show Boolean Input Dialog
        /// </summary>
        /// <param name="sTitle">Dialog Title</param>
        /// <param name="sText">Dialog Text</param>
        /// <param name="bDefault">Default Value</param>
        /// <returns>true if Yes, false if No, default if Cancel</returns>
        public static Boolean inputBoolean(String sTitle, String sText, Boolean bDefault)
        {
            DialogResult result = MessageBox.Show(sText, sTitle, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
            {
                return bDefault;
            }
            return result == DialogResult.Yes;
        }
    }
}
