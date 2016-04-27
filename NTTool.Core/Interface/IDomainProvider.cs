using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NTTool.Core
{
    public interface IDomainProvider
    {
        bool IsDomainAdministrator { get; set; }

        List<string> EnumerateDomains();

    }
}
