using Microsoft.Win32;
using NTTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace NTTool.Core
{
    public class SoftwareProvider
    {
        public static List<SoftwareEntity> GetListOfInstalledSoftwares(string machineName)
        {
            List<SoftwareEntity> programs = new List<SoftwareEntity>();

            try
            {


                string softwareRegLoc = @"Software\Microsoft\Windows\CurrentVersion\Uninstall";
                // Open Remote Machine Registry Key 
                RegistryKey remoteKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, machineName);

                RegistryKey regKey = remoteKey.OpenSubKey(softwareRegLoc);

                // Open Registry Sub Key
                RegistryKey subKey;

                // Read Value from Registry Sub Key
                string softwareName;
                string displayVersion;


                foreach (string subKeyName in regKey.GetSubKeyNames())
                {
                    // Open Registry Sub Key
                    subKey = regKey.OpenSubKey(subKeyName);

                    // Read Value from Registry Sub Key
                    softwareName = (string)subKey.GetValue("DisplayName");
                    displayVersion = (string)subKey.GetValue("DisplayVersion");


                    if (!string.IsNullOrEmpty(softwareName))
                    {
                        programs.Add(new SoftwareEntity { DisplayName = softwareName, DisplayVersion = displayVersion });
                    }
                }

                return programs;
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
