using NTTool.Models;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace NTTool.Core
{
    public class MachineProvider
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

        public static List<MachineEntity> NetworkComputers(string domainName)
        {
            List<MachineEntity> ComputerNames = new List<MachineEntity>();

            try
            {


                DirectoryEntry entry = new DirectoryEntry("LDAP://" + domainName);
                DirectorySearcher mySearcher = new DirectorySearcher(entry);
                mySearcher.Filter = ("(objectClass=computer)");
                mySearcher.SizeLimit = int.MaxValue;
                mySearcher.PageSize = int.MaxValue;

                foreach (SearchResult resEnt in mySearcher.FindAll())
                {
                    var ComputerName = resEnt.GetDirectoryEntry();
                    ComputerNames.Add(new MachineEntity { MachineName = ComputerName.Name.Replace("CN=", ""), MachineAdInfo = ComputerName });
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
    }
}
