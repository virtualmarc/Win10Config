using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Win10Config.Utils
{
    class App
    {
        public static void uninstall(String sName)
        {
            PowerShell ps = PowerShell.Create();
            ps.AddCommand("Get-AppxPackage");
            ps.AddArgument("-AllUsers");
            ps.AddArgument("*" + sName + "*");
            ps.AddCommand("Remove-AppxPackage");
            try {
                ps.Invoke();
            }
            catch (Exception)
            {}
            ps.Commands.Clear();
            ps.AddCommand("Get-AppxPackage");
            ps.AddArgument("*" + sName + "*");
            ps.AddCommand("Remove-AppxPackage");
            try
            {
                ps.Invoke();
            }
            catch (Exception)
            { }
            ps.Commands.Clear();
            ps.AddCommand("Get-AppxProvisionedPackage");
            ps.AddArgument("-Online");
            ps.AddCommand("where");
            ps.AddArgument("-Property DisplayName");
            ps.AddArgument("-eq " + sName);
            ps.AddCommand("Remove-AppxProvisionedPackage");
            ps.AddArgument("-Online");
            try
            {
                ps.Invoke();
            }
            catch (Exception)
            { }
            String[] dirs = Directory.GetDirectories(@"C:\Program Files\WindowsApps", "*" + sName + "*");
            foreach (String dir in dirs)
            {
                try
                {
                    Directory.Delete(dir, true);
                }
                catch (Exception)
                { }
            }
        }
    }
}
