using NTTool.Models;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using NTTool.Core.Models;
using System.Security.Principal;

namespace NTTool.Core
{
    public class NetworkProvider : INetworkProvider
    {
        private static INetworkProvider obj;

        public bool IsDomainAdministrator { get; set; }

        public List<string> EnumerateDomains()
        {
            List<string> alDomains = new List<string>();

            try
            {
                Forest currentForest = Forest.GetCurrentForest();
                DomainCollection myDomains = currentForest.Domains;

                foreach (Domain objDomain in myDomains)
                {
                    alDomains.Add(objDomain.Name);
                }


                IsDomainAdministrator = IsAdministrator(alDomains.FirstOrDefault());

                return alDomains;
            }
            catch
            {
                throw;
            }
        }

        public List<MachineEntity> DomainNetworkComputers(string domainName)
        {
            List<MachineEntity> ComputerNames = new List<MachineEntity>();

            try
            {
                DirectoryEntry entry = new DirectoryEntry("LDAP://" + domainName);
                DirectorySearcher mySearcher = new DirectorySearcher(entry);
                mySearcher.Filter = ("(objectClass=computer)");
                mySearcher.SizeLimit = int.MaxValue;
                mySearcher.PageSize = int.MaxValue;
                MachineEntity objMachine = null;
                MachineEntity objOnlineMachine = null;
                DirectoryEntry machineAdInfo;
                List<MachineEntity> listofOnlineMachines = NetworkComputers();
                foreach (SearchResult resEnt in mySearcher.FindAll())
                {
                    machineAdInfo = resEnt.GetDirectoryEntry();

                    objMachine = new MachineEntity();
                    objMachine.MachineStatus = MachineStatus.Offline;
                    objMachine.MachineName = machineAdInfo.Name.Replace("CN=", "");
                    objMachine.DomainName = domainName;
                    objMachine.MachineAdInfo = machineAdInfo;
                    objOnlineMachine = listofOnlineMachines.FirstOrDefault(x => x.MachineName == objMachine.MachineName);

                    if (objOnlineMachine != null)
                    {
                        objMachine.MachineStatus = MachineStatus.Online;
                        objMachine.IPAddress = listofOnlineMachines.Where(x => x.MachineName == objMachine.MachineName).FirstOrDefault().IPAddress;

                    }

                    if (Dns.GetHostName() == objMachine.MachineName || IsDomainAdministrator)
                    {
                        objMachine = GetMachineAdditionalInformation(objMachine.MachineName, domainName, objMachine);
                    }

                    ComputerNames.Add(objMachine);
                }

                mySearcher.Dispose();
                entry.Dispose();
                return ComputerNames;
            }
            catch
            {

                throw;
            }
        }

        public List<MachineEntity> NetworkComputers()
        {
            return NetApi32.GetNetWorkMachines();
        }

        public MachineEntity GetMachineAdditionalInformation(string machine, string domain, MachineEntity objMachine)
        {

            ManagementScope scope = new ManagementScope();
            try
            {
                ConnectionOptions options = new ConnectionOptions();
                scope = new ManagementScope(@"\\" + machine + "\\root\\CIMV2", options);
                scope.Connect();

                var macAddress = GetMACAddress(scope);

                var ipaddresses = GetIPAddresses(scope);

                var networkDevices = GetNetworkDevices(scope);

                objMachine.IPAddresses = ipaddresses;

                objMachine.ListOfNetworkDevices = networkDevices;


                SelectQuery query = new SelectQuery("SELECT * FROM Win32_OperatingSystem");

                ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);

                using (ManagementObjectCollection queryCollection = searcher.Get())
                {
                    foreach (ManagementObject m in queryCollection)
                    {
                        objMachine.MachineName = m["csname"].ToString();
                        objMachine.OpratingSystem = m["Caption"].ToString();
                        objMachine.OpratingSystemVersion = m["Version"].ToString();
                        objMachine.SystemDirectory = m["WindowsDirectory"].ToString();
                        objMachine.Manufacturer = m["Manufacturer"].ToString();
                        objMachine.MachineMACAddress = macAddress;
                    }

                }



            }

            catch (Exception ex)
            {

                return objMachine;
            }

            return objMachine;
        }

        private List<NetworkDevices> GetNetworkDevices(ManagementScope scope)
        {
            ManagementObjectSearcher objSearcher;
            ManagementObjectCollection objColl;
            ObjectQuery objQuery;
            List<NetworkDevices> objListOfNetworkDevices = new List<NetworkDevices>(); ;
            ConnectionOptions connOpts = new ConnectionOptions();
            objQuery = new ObjectQuery("SELECT * FROM Win32_NetworkAdapter WHERE AdapterTypeID <> NULL");
            scope.Connect();

            objSearcher = new ManagementObjectSearcher(scope, objQuery);
            objSearcher.Options.Timeout = new TimeSpan(0, 0, 0, 0, 7000);
            objColl = objSearcher.Get();
            NetworkDevices objNetworkDevice;
            foreach (ManagementObject mo in objColl)
            {
                objNetworkDevice = new NetworkDevices();

                objNetworkDevice.DeviceID = mo["DeviceID"] == null ? "Unavailble" : mo["DeviceID"].ToString();
                objNetworkDevice.Adaptertype = mo["AdapterType"] == null ? "Unavailble" : mo["AdapterType"].ToString();
                objNetworkDevice.Description = mo["Description"] == null ? "Unavailble" : mo["Description"].ToString();
                objNetworkDevice.MACaddress = mo["MACAddress"] == null ? "Unavailble" : mo["MACAddress"].ToString();
                objNetworkDevice.Manufacturer = mo["Manufacturer"] == null ? "Unavailble" : mo["Manufacturer"].ToString();

                var ip = GetIPAddressByMacAddress(scope, objNetworkDevice.MACaddress);
                objNetworkDevice.IPAddresses = ip;

                objListOfNetworkDevices.Add(objNetworkDevice);

            }

            return objListOfNetworkDevices;

        }

        public static INetworkProvider GetInstance()
        {
            if (obj == null)
            {
                obj = new NetworkProvider();
            }
            return obj;
        }

        private bool IsAdministrator(string domainName)
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);

            return IsDomainAdmin(domainName, identity.Name.Split('\\')[1]);
        }

        private bool IsDomainAdmin(string domain, string userName)
        {
            string adminDn = GetAdminDn(domain);
            SearchResult result = (new DirectorySearcher(
                new DirectoryEntry("LDAP://" + domain),
                "(&(objectCategory=user)(samAccountName=" + userName + "))",
                new[] { "memberOf" })).FindOne();
            return result.Properties["memberOf"].Contains(adminDn);
        }

        private string GetAdminDn(string domain)
        {
            return (string)(new DirectorySearcher(
                new DirectoryEntry("LDAP://" + domain),
                "(&(objectCategory=group)(cn=Domain Admins))")
                .FindOne().Properties["distinguishedname"][0]);
        }

        private string GetMACAddress(ManagementScope scope)
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
        
        private string[] GetIPAddressByMacAddress(ManagementScope scope,string macAddress)
        {

            SelectQuery query = new SelectQuery("SELECT IPAddress FROM Win32_NetworkAdapterConfiguration WHERE MACAddress = '"+ macAddress +"'");

            ManagementObjectSearcher NetworkSearcher = new ManagementObjectSearcher(scope, query);
            string[] arrIPAddress = null;
            foreach (ManagementObject NetworkObj in NetworkSearcher.Get())
            {
                arrIPAddress = (string[])(NetworkObj["IPAddress"]);
            }

            return arrIPAddress;
        }

    }
}
