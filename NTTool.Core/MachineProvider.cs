using Microsoft.Win32;
using NTTool.Core.Models;
using NTTool.Models;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Management;
using System.Security.Principal;
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
                string installDate;
                string publisher;
                string estimatedSize;
                programs = new List<SoftwareEntity>();

                foreach (string subKeyName in regKey.GetSubKeyNames())
                {
                    // Open Registry Sub Key
                    subKey = regKey.OpenSubKey(subKeyName);

                    // Read Value from Registry Sub Key
                    softwareName = (string)subKey.GetValue("DisplayName");
                    displayVersion = (string)subKey.GetValue("DisplayVersion");
                    installDate = (string)subKey.GetValue("InstallDate");

                    if (string.IsNullOrEmpty(installDate))
                    {
                        DateTime startDate = new DateTime(1970, 1, 1, 0, 0, 0);
                        Int64 regVal = Convert.ToInt64(installDate);
                        installDate = startDate.AddSeconds(regVal).ToString();
                    }

                    publisher = (string)subKey.GetValue("Publisher");
                    estimatedSize = subKey.GetValue("EstimatedSize")==null ? "NA" : subKey.GetValue("EstimatedSize").ToString();

                    if (!string.IsNullOrEmpty(softwareName))
                    {
                        programs.Add(new SoftwareEntity { DisplayName = softwareName, 
                            DisplayVersion = displayVersion, 
                            Publisher = publisher, InstallDate = installDate, EstimatedSize=estimatedSize });
                    }
                }
            }

            catch (Exception)
            {
                throw;
            }
            return programs;
        }

        public MachineEntity GetMachineAdditionalInformation(string machine, string domain, MachineEntity objMachine)
        {

            ManagementScope scope = new ManagementScope();
            try
            {
                var options = new ConnectionOptions();
                options.Authentication = AuthenticationLevel.Default;
                options.Impersonation = ImpersonationLevel.Impersonate;
                options.EnablePrivileges = true;

                scope = new ManagementScope(@"\\" + machine + "\\root\\CIMV2", options);
                scope.Connect();

                var ipaddresses = GetIPAddresses(scope);

                var networkDevices = GetNetworkDevices(scope);

                if (ipaddresses.Length > 0)
                {
                    objMachine.IPAddress = ipaddresses[0];
                }

                objMachine.IPAddresses = ipaddresses;

                objMachine.MachineMACAddress = GetDefaultMACAddress(scope);

                objMachine.ListOfNetworkDevices = networkDevices;

            }

            catch (Exception ex)
            {

                return objMachine;
            }

            return objMachine;
        }

        private string[] GetIPAddresses(ManagementScope scope)
        {

            SelectQuery query = new SelectQuery("SELECT IPAddress FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = 'TRUE'");

            ManagementObjectSearcher NetworkSearcher = new ManagementObjectSearcher(scope, query);
            string[] arrIPAddress = null;
            foreach (ManagementObject NetworkObj in NetworkSearcher.Get())
            {
                arrIPAddress = (string[])(NetworkObj["IPAddress"]);
            }

            return arrIPAddress;
        }

        private string[] GetIPAddressByMacAddress(ManagementScope scope, string macAddress)
        {

            SelectQuery query = new SelectQuery("SELECT IPAddress FROM Win32_NetworkAdapterConfiguration WHERE MACAddress = '" + macAddress + "'");

            ManagementObjectSearcher NetworkSearcher = new ManagementObjectSearcher(scope, query);
            string[] arrIPAddress = null;
            foreach (ManagementObject NetworkObj in NetworkSearcher.Get())
            {
                arrIPAddress = (string[])(NetworkObj["IPAddress"]);
            }

            return arrIPAddress;
        }

        private List<NetworkDevices> GetNetworkDevices(ManagementScope scope)
        {
            ManagementObjectSearcher objSearcher;
            ManagementObjectCollection objColl;
            ObjectQuery objQuery;
            List<NetworkDevices> objListOfNetworkDevices = new List<NetworkDevices>(); ;

            objQuery = new ObjectQuery("SELECT * FROM Win32_NetworkAdapter WHERE AdapterTypeID <> NULL");
            scope.Connect();

            objSearcher = new ManagementObjectSearcher(scope, objQuery);
            objSearcher.Options.Timeout = new TimeSpan(0, 0, 0, 0, 7000);
            objColl = objSearcher.Get();
            NetworkDevices objNetworkDevice;
            string[] ip = null;
            foreach (ManagementObject mo in objColl)
            {
                objNetworkDevice = new NetworkDevices();

                objNetworkDevice.DeviceID = mo["DeviceID"] == null ? "Unavailble" : mo["DeviceID"].ToString();
                objNetworkDevice.Adaptertype = mo["AdapterType"] == null ? "Unavailble" : mo["AdapterType"].ToString();
                objNetworkDevice.Description = mo["Description"] == null ? "Unavailble" : mo["Description"].ToString();
                objNetworkDevice.MACaddress = mo["MACAddress"] == null ? "Unavailble" : mo["MACAddress"].ToString();
                objNetworkDevice.Manufacturer = mo["Manufacturer"] == null ? "Unavailble" : mo["Manufacturer"].ToString();

                ip = GetIPAddressByMacAddress(scope, objNetworkDevice.MACaddress);
                objNetworkDevice.IPAddresses = ip;

                objListOfNetworkDevices.Add(objNetworkDevice);

            }

            return objListOfNetworkDevices;

        }

        private string GetDefaultMACAddress(ManagementScope scope)
        {
            SelectQuery query = new SelectQuery("Select * FROM Win32_NetworkAdapterConfiguration");
            ManagementObjectSearcher objMOS = new ManagementObjectSearcher(scope, query);
            ManagementObjectCollection objMOC = objMOS.Get();
            string macAddress = String.Empty;
            foreach (ManagementObject objMO in objMOC)
            {
                object tempMacAddrObj = objMO["MacAddress"];

                if (tempMacAddrObj == null)
                {
                    continue;
                }
                if (macAddress == String.Empty)
                {
                    macAddress = tempMacAddrObj.ToString();
                }
                objMO.Dispose();
            }

            return macAddress;
        }

        public MachineEntity GetStorageInfoOfMachine(string machine, string domain, MachineEntity objMachine)
        {

            ManagementScope scope = new ManagementScope();
            try
            {
                ConnectionOptions options = new ConnectionOptions();
                scope = new ManagementScope(@"\\" + machine + "\\root\\CIMV2", options);
                scope.Connect();

                SelectQuery query = new SelectQuery("SELECT * FROM Win32_LogicalDisk");

                ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
                var storageDevices = new StorageDevices();
                objMachine.ListOfStoragekDevices = new List<StorageDevices>();
                using (ManagementObjectCollection queryCollection = searcher.Get())
                {
                    foreach (ManagementObject m in queryCollection)
                    {
                        storageDevices = new StorageDevices();

                        storageDevices.Name = m["Name"] == null ? "Unavailble" : m["Name"].ToString();
                        storageDevices.Caption = m["Caption"] == null ? "Unavailble" : m["Caption"].ToString();
                        storageDevices.FreeSpace = FreeSpaceInGB(m["FreeSpace"] == null ? "0" : m["FreeSpace"].ToString());
                        storageDevices.SerialNumber = m["VolumeSerialNumber"] == null ? "Unavailble" : m["VolumeSerialNumber"].ToString();

                        objMachine.ListOfStoragekDevices.Add(storageDevices);
                    }

                }



            }

            catch (Exception ex)
            {

                return objMachine;
            }

            return objMachine;
        }

        private string FreeSpaceInGB(string freeSpace)
        {

            return ((Convert.ToDouble(freeSpace) / 1024 / 1024) / 1024).ToString("0.00");

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



