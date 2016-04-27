using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace NTTool.Core
{
    public class DomainProvider : IDomainProvider
    {
        private static IDomainProvider obj;

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

        public static IDomainProvider GetInstance()
        {
            if (obj == null)
            {
                obj = new DomainProvider();
            }
            return obj;
        }
    }
}
