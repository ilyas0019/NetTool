using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NTTool.Core;
using NTTool.Models;

namespace Dashboard
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            var domains= NetworkProvider.EnumerateDomains();

            var listOfMachines = NetworkProvider.DomainNetworkComputers(domains.FirstOrDefault());

            lstView.DataSource = listOfMachines;
            lstView.DisplayMember = "MachineName";
            lblInfo.Text = string.Format("Total no of machines is {0}", listOfMachines.Count);
        }

       

        private void GetListofSofwares()
        {
            var obj = (MachineEntity)lstView.SelectedItem;

            var listOfSoftwares = MachineProvider.GetListOfInstalledSoftwares(obj.MachineName);
            lslSoftware.DataSource = listOfSoftwares;
            lslSoftware.DisplayMember = "DisplayName";
            lblSoftware.Text = string.Format("Total no of machines is {0}", listOfSoftwares == null ? 0 : listOfSoftwares.Count);
        }

        private void lslSoftware_Click(object sender, EventArgs e)
        {
            GetListofSofwares();
        }

       
    }
}
