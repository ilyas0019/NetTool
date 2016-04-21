using NTTool.Models;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace NTTool.Core
{
    public class NetworkProvider
    {

        public static List<string> EnumerateDomains()
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
                return alDomains;
            }
            catch
            {

                throw;
            }
        }

        public static List<MachineEntity> DomainNetworkComputers(string domainName)
        {
            List<MachineEntity> ComputerNames = new List<MachineEntity>();

            try
            {


                DirectoryEntry entry = new DirectoryEntry("LDAP://" + domainName);
                DirectorySearcher mySearcher = new DirectorySearcher(entry);
                mySearcher.Filter = ("(objectClass=computer)");
                mySearcher.SizeLimit = int.MaxValue;
                mySearcher.PageSize = int.MaxValue;
                MachineEntity objMachine=null;
                DirectoryEntry machineAdInfo;
                foreach (SearchResult resEnt in mySearcher.FindAll())
                {
                    objMachine=new MachineEntity();
                    
                    machineAdInfo = resEnt.GetDirectoryEntry();

                    ComputerNames.Add(new MachineEntity { MachineName = machineAdInfo.Name.Replace("CN=", ""), MachineAdInfo = machineAdInfo, DomainName=domainName });
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

        public static MachineEntity GetMachineInformation(string machine, string domain, MachineEntity objMachine)
        {

            ManagementScope scope = new ManagementScope();
            try
            {

                ConnectionOptions options = new ConnectionOptions();
                options.Authority = "ntlmdomain:" + domain;

                scope = new ManagementScope(@"\\" + machine + "\\root\\CIMV2", options);

                scope.Connect();

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

                    }

                }

            }

            catch (Exception ex)
            {

                return null;
            }

            return objMachine;
        }
    }
}
