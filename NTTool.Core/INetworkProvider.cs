using NTTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTool.Core
{
    public interface INetworkProvider
    {
        List<string> EnumerateDomains();

        List<MachineEntity> DomainNetworkComputers(string domainName);

        MachineEntity GetMachineInformation(string machine, string domain, MachineEntity objMachine);

    }
}
