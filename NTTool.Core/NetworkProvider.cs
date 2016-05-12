﻿using NTTool.Models;
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
using System.Diagnostics;

namespace NTTool.Core
{
    public class NetworkProvider : INetworkProvider
    {
        [DllImport("iphlpapi.dll", ExactSpelling = true)]
        public static extern int SendARP(int DestIP, int SrcIP, [Out] byte[] pMacAddr, ref int PhyAddrLen);

        private static INetworkProvider obj;

        public List<MachineEntity> DomainNetworkComputers(string domainName, bool onLineMachinesOnly=false)
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
                MachineEntity obj = null;
                List<MachineEntity> listofOnlineMachines = DomainOnlineComputers(domainName);
                foreach (SearchResult resEnt in mySearcher.FindAll())
                {
                    machineAdInfo = resEnt.GetDirectoryEntry();

                    objMachine = new MachineEntity();
                    objMachine.MachineStatus = MachineStatus.Offline;
                    objMachine.MachineName = machineAdInfo.Properties["cn"].Value.ToString();
                    objMachine.OpratingSystem = machineAdInfo.Properties["operatingSystem"].Value == null ? "NA" : machineAdInfo.Properties["operatingSystem"].Value.ToString();
                    objMachine.OpratingSystemVersion = machineAdInfo.Properties["operatingSystemVersion"].Value == null ? "NA" : machineAdInfo.Properties["operatingSystemVersion"].Value.ToString();
                    objMachine.DNSHostName = machineAdInfo.Properties["dNSHostName"].Value.ToString();
                    objMachine.DomainName = domainName;

                    objOnlineMachine = listofOnlineMachines.FirstOrDefault(x => x.MachineName == objMachine.MachineName);

                    if (objOnlineMachine != null)
                    {
                        objMachine.MachineStatus = MachineStatus.Online;
                        obj =  listofOnlineMachines.Where(x => x.MachineName == objMachine.MachineName).FirstOrDefault();
                        objMachine.IPAddress = obj.IPAddress;
                        objMachine.MachineMACAddress = obj.MachineMACAddress;
                        if (onLineMachinesOnly)
                        {
                            ComputerNames.Add(objMachine);
                        }

                    }

                    if (!onLineMachinesOnly)
                    {
                        ComputerNames.Add(objMachine);
                    }
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

        private List<MachineEntity> DomainOnlineComputers(string domainName)
        {
            var ComputerNames = new List<MachineEntity>();
            try
            {
                DirectoryEntry DomainEntry = new DirectoryEntry("WinNT://" + domainName);
                DomainEntry.Children.SchemaFilter.Add("computer");
                Parallel.ForEach(DomainEntry.Children.OfType<DirectoryEntry>(), (DirectoryEntry machine) =>
                {
                    ProcessMachine(machine, ComputerNames);
                });

                return ComputerNames;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void ProcessMachine(DirectoryEntry machine, List<MachineEntity> computerNames)
        {
            MachineEntity machineEntity = new MachineEntity();
            string[] Ipaddr = new string[3];
            System.Net.IPHostEntry Tempaddr = null;
            byte[] ab;
            int len;
            int r;
            string mac = string.Empty;

            Ipaddr[0] = machine.Name;

            try
            {
                Tempaddr = (System.Net.IPHostEntry)Dns.GetHostEntry(machine.Name);
            }
            catch (Exception)
            {
                return;
            }

            foreach (IPAddress TempA in Tempaddr.AddressList)
            {
                Ipaddr[1] = TempA.ToString();
                ab = new byte[6];
                len = ab.Length;
                r = SendARP(TempA.GetHashCode(), 0, ab, ref len);
                mac = BitConverter.ToString(ab, 0, 6);
                if (mac == "00-00-00-00-00-00")
                {
                    return;
                }
                Ipaddr[2] = mac;
            }

            machineEntity.MachineStatus = MachineStatus.Online;
            machineEntity.IPAddress = Ipaddr[1];
            machineEntity.MachineName = machine.Name;
            machineEntity.MachineMACAddress = Ipaddr[2];

            computerNames.Add(machineEntity);
        }

        private List<MachineEntity> NetworkComputers()
        {
            return NetApi32.GetNetWorkMachines();
        }

        public static INetworkProvider GetInstance()
        {
            if (obj == null)
            {
                obj = new NetworkProvider();
            }
            return obj;
        }

    }
}
