using NTTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTool.Core
{
    public interface IMachineProvider
    {
         List<SoftwareEntity> GetListOfInstalledSoftwares(string machineName);
        
    }
}
