using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Web;

namespace NTTool.Models
{
    public class MachineEntity 
    {
        public string MachineName { get; set; }

        public string IPAddress { get; set; }

        public string MachineType { get; set; }

        public string OpratingSystem { get; set; }

        public DirectoryEntry MachineAdInfo { get; set; }

    }
}