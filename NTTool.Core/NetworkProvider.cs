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

                    if (Dns.GetHostName() == objMachine.MachineName  ||IsDomainAdministrator)
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
       //         options.Authority = "ntlmdomain:" + domain;
                scope = new ManagementScope(@"\\" + machine + "\\root\\CIMV2", options);
                scope.Connect();

                var macAddress = GetMACAddress(scope);

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

                if (tempMacAddrObj == null) //Skip objects without a MACAddress
                {
                    continue;
                }
                if (macAddress == String.Empty) // only return MAC Address from first card that has a MAC Address
                {
                    macAddress = tempMacAddrObj.ToString();
                }
                objMO.Dispose();
            }

            return macAddress;
        }
       
    }
}
