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
            pgInfo.Maximum = 100;
            pgInfo.Minimum = 1;
            pgInfo.Value = 10;
        
            try
            {
                FillListOfMachines();
                pgInfo.Value = 100;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

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
            try
            {
                GetListofSofwares();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {

            if (txtFilter.Text.Trim() != "")
            {
                FillListOfMachines(txtFilter.Text);
            }
            else
            {
                MessageBox.Show("Please enter machine name");
            }
        }

        private void FillListOfMachines(string searchString = null)
        {
            lstView.DataSource = null;
            lslSoftware.DataSource = null;
            lblSoftware.Text = "";
            lblInfo.Text = "";

            var domains = NetworkProvider.EnumerateDomains();
            var listOfMachines = NetworkProvider.DomainNetworkComputers(domains.FirstOrDefault());
            if (!string.IsNullOrEmpty(searchString))
            {
                listOfMachines = listOfMachines.Where(x => x.MachineName.Contains(txtFilter.Text)).ToList();
            }
            lstView.DataSource = null;
            lstView.DataSource = listOfMachines;

            lstView.DisplayMember = "MachineName";
            lblInfo.Text = string.Format("Total no of machines is {0}", listOfMachines.Count);
        }


    }
}
