using NTTool.Core;
using NTTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTTool.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Scan()
        {
            var domain = NetworkProvider.EnumerateDomains();
            var machines = NetworkProvider.DomainNetworkComputers(domain.FirstOrDefault());
            return View(machines);
        }

        public ActionResult Details(MachineEntity obj)
        {
            ViewBag.MachineName = obj.MachineName;
            var listOfSoftwares = MachineProvider.GetListOfInstalledSoftwares(obj.MachineName);

            obj = NetworkProvider.GetMachineInformation(obj.MachineName, obj.DomainName, obj);

            return View(new ViewModel { MachineInfo=obj,SoftwareList=listOfSoftwares });
        }

    }
}
