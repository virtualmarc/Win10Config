using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win10Config.Interfaces;
using Win10Config.Utils;

namespace Win10Config.Configuration
{
    class OneDrive : IConfiguration
    {
        private Boolean bRun = false;
        private String sError = "";

        public void changeValue()
        {
            bRun = Input.inputBoolean(getDisplayName(), "Uninstall OneDrive?", bRun);
        }

        public string getDisplayName()
        {
            return "Uninstall OneDrive";
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
            if (bRun)
            {
                try
                {
                    Registry.SetValue(@"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows", "DisableFileSyncNGSC", 0x1, RegistryValueKind.DWord);
                    Process.Start("taskkill.exe", "/f /im OneDrive.exe");
                    String sSysRoot = Environment.GetEnvironmentVariable("SystemRoot");
                    String sPath = sSysRoot;
                    if (File.Exists(sSysRoot + @"\System32\OneDriveSetup.exe"))
                    {
                        sPath += @"\System32\OneDriveSetup.exe";
                    }
                    else
                    {
                        sPath += @"\SysWOW64\OneDriveSetup.exe";
                    }
                    Process.Start(sPath, "/uninstall");
                    String sUserProfile = Environment.GetEnvironmentVariable("UserProfile");
                    String sLocalAppData = Environment.GetEnvironmentVariable("LocalAppData");
                    String sProgramData = Environment.GetEnvironmentVariable("ProgramData");
                    try
                    {
                        Directory.Delete(sUserProfile + @"\OneDrive", true);
                    }
                    catch (Exception) { }
                    try
                    {
                        Directory.Delete(sLocalAppData + @"\Microsoft\OneDrive", true);
                    }
                    catch (Exception) { }
                    try
                    {
                        Directory.Delete(sProgramData + @"\Microsoft OneDrive", true);
                    }
                    catch (Exception) { }
                    try
                    {
                        Directory.Delete(@"C:\OneDriveTemp", true);
                    }
                    catch (Exception) { }
                    try
                    {
                        Registry.ClassesRoot.OpenSubKey("CLSID", true).DeleteSubKeyTree("{018D5C66-4533-4307-9B53-224DE2ED1FE6}");
                    }
                    catch (Exception) { }
                    try
                    {
                        Registry.ClassesRoot.OpenSubKey("Wow6432Node").OpenSubKey("CLSID", true).DeleteSubKeyTree("{018D5C66-4533-4307-9B53-224DE2ED1FE6}");
                    }
                    catch (Exception) { }
                    String[] dirs = Directory.GetDirectories(@"C:\Windows\WinSxS", "*onedrive*");
                    foreach (String dir in dirs)
                    {
                        try
                        {
                            Directory.Delete(dir, true);
                        }
                        catch (Exception) { }
                    }
                    try
                    {
                        Registry.ClassesRoot.OpenSubKey("CLSID", true).CreateSubKey("{018D5C66-4533-4307-9B53-224DE2ED1FE6}");
                    }
                    catch (Exception) { }
                    Registry.SetValue(@"HKEY_CLASSES_ROOT\CLSID\{018D5C66-4533-4307-9B53-224DE2ED1FE6}", "IsPinnedToNameSpaceTree", 0x0, RegistryValueKind.DWord);
                    Registry.SetValue(@"HKEY_CLASSES_ROOT\CLSID\{018D5C66-4533-4307-9B53-224DE2ED1FE6}", "System.IsPinnedToNameSpaceTree", 0x0, RegistryValueKind.DWord);
                    try
                    {
                        Registry.ClassesRoot.OpenSubKey("Wow6432Node").OpenSubKey("CLSID", true).CreateSubKey("{018D5C66-4533-4307-9B53-224DE2ED1FE6}");
                    }
                    catch (Exception) { }
                    Registry.SetValue(@"HKEY_CLASSES_ROOT\Wow6432Node\CLSID\{018D5C66-4533-4307-9B53-224DE2ED1FE6}", "IsPinnedToNameSpaceTree", 0x0, RegistryValueKind.DWord);
                    Registry.SetValue(@"HKEY_CLASSES_ROOT\Wow6432Node\CLSID\{018D5C66-4533-4307-9B53-224DE2ED1FE6}", "System.IsPinnedToNameSpaceTree", 0x0, RegistryValueKind.DWord);
                    return true;
                }
                catch (Exception ex)
                {
                    sError += ex.Message;
                    return false;
                }
            } else
            {
                return true;
            }
        }
    }
}
