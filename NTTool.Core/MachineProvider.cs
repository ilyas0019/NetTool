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
    public class MachineProvider : IMachineProvider
    {
        private static IMachineProvider obj;

        public List<SoftwareEntity> GetListOfInstalledSoftwares(string machineName)
        {
            List<SoftwareEntity> programs = null;
            string softwareRegLoc = @"Software\Microsoft\Windows\CurrentVersion\Uninstall";
            try
            {
                // Open Remote Machine Registry Key 
                RegistryKey remoteKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, machineName);

                RegistryKey regKey = remoteKey.OpenSubKey(softwareRegLoc);

                // Open Registry Sub Key
                RegistryKey subKey;

                // Read Value from Registry Sub Key
                string softwareName;
                string displayVersion;

                programs = new List<SoftwareEntity>();

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
            }

            catch (Exception)
            {
                throw;
            }
            return programs;
        }

        public static IMachineProvider GetInstance()
        {
            if (obj == null)
            {
                obj = new MachineProvider();
            }
            return obj;
        }
    }
}
