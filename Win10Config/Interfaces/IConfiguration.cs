using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win10Config.Interfaces
{
    interface IConfiguration
    {
        /// <summary>
        /// Display Name for the Table
        /// </summary>
        /// <returns>Name</returns>
        String getDisplayName();

        /// <summary>
        /// Display Value for the Table
        /// </summary>
        /// <returns>Value</returns>
        String getDisplayValue();

        /// <summary>
        /// Called when User wants to change the Value
        /// </summary>
        void changeValue();

        /// <summary>
        /// Run Configuration Change
        /// </summary>
        /// <returns>true = Success, false = Failure</returns>
        Boolean run();

        /// <summary>
        /// Error Text on Failure
        /// </summary>
        /// <returns>Error Text</returns>
        String getErrorText();
    }
}
