using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace HitnTrail
{
    class Program
    {
        static void Main(string[] args)
        {

            DisplayDhcpServerAddresses();
            GetComputersOnNetwork();

            
        }

        static void  GetComputersOnNetwork()
        {
            

            DirectoryEntry entry = new DirectoryEntry("LDAP://pyramidconsultinginc.com");
            DirectorySearcher mySearcher = new DirectorySearcher(entry);
            mySearcher.Filter = ("(objectClass=computer)");
            Console.WriteLine("Listing of computers in the Active Directory"); 
            Console.WriteLine("============================================");
            var counter = 0;
            foreach(SearchResult resEnt in mySearcher.FindAll())
            {
                counter++;
                Console.WriteLine(resEnt.GetDirectoryEntry().Name.ToString()); 
            }
                       
            
            Console.WriteLine("=========== End of Listing =============");
            Console.WriteLine(counter);
            Console.Read();

            
        }


        public static void DisplayDhcpServerAddresses()
        {
            Console.WriteLine("DHCP Servers");
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in adapters)
            {

                IPInterfaceProperties adapterProperties = adapter.GetIPProperties();
                IPAddressCollection addresses = adapterProperties.DhcpServerAddresses;
                if (addresses.Count > 0)
                {
                    Console.WriteLine(adapter.Description);
                    foreach (IPAddress address in addresses)
                    {
                        Console.WriteLine("  Dhcp Address ............................ : {0}",
                            address.ToString());
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
