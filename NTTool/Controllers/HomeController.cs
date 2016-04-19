using NTTool.Core;
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

            var domain = MachineProvider.EnumerateDomains();
            var machines = MachineProvider.NetworkComputers(domain.FirstOrDefault());

            return View(machines);

        }

        public ActionResult Details(string id)
        {
            var listOfSoftwares = SoftwareProvider.GetListOfInstalledSoftwares(id);
            return View(listOfSoftwares);
        }

    }
}
