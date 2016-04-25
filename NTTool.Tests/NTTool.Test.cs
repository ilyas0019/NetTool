using Microsoft.VisualStudio.TestTools.UnitTesting;
using NTTool.Core;
using NTTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTool.Tests
{
    [TestClass]
    public class AppTest
    {

        [TestMethod]
        public void GetListOfInstalledSoftwares()
        {
            var obj = new MockMachineProivder();
            var list = obj.GetListOfInstalledSoftwares("machine");
            Assert.IsTrue(list.Count > 0);
        }

        [TestMethod]
        public void GetDomains()
        {
            var network = new MockNetworkProivder();
            Assert.IsTrue(network.EnumerateDomains().Count > 0);
        }

        [TestMethod]
        public void GetDomainNetworkComputers()
        {
            var network = new MockNetworkProivder();
            Assert.IsTrue(network.DomainNetworkComputers("Domain").Count > 0);
        }
        [TestMethod]
        public void GetAdditionalMachineInformation()
        {
            var network = new MockNetworkProivder();
            var machineInfo=new MachineEntity();
            machineInfo= network.GetMachineInformation("machine", "domain", machineInfo);
            Assert.IsTrue(machineInfo.OpratingSystem=="Windows");
        }


    }

    public class MockMachineProivder : IMachineProvider
    {

        public List<Models.SoftwareEntity> GetListOfInstalledSoftwares(string machineName)
        {
            var objList = new List<Models.SoftwareEntity>();
            objList.Add(new Models.SoftwareEntity { DisplayName = "Test" });
            return objList;
        }
    }

    public class MockNetworkProivder : INetworkProvider
    {
        public List<string> EnumerateDomains()
        {
            var obj = new List<string>();
            obj.Add("Domain");
            return obj;
        }

        public List<Models.MachineEntity> DomainNetworkComputers(string domainName)
        {
            var objList = new List<Models.MachineEntity>();
            objList.Add(new Models.MachineEntity { DomainName = "Domain", OpratingSystem = "Windows" });
            return objList;
        }

        public Models.MachineEntity GetMachineInformation(string machine, string domain, Models.MachineEntity objMachine)
        {
            objMachine.DomainName = "Domain";
            objMachine.OpratingSystem = "Windows";

            return objMachine;
        }
    }

}
