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
using NTTool.Core.Models;
using System.Net;

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
            var machineName = lstView.SelectedItems[0].SubItems[1].Text;
            if (NetworkProvider.GetInstance().IsDomainAdministrator || Dns.GetHostName() == machineName)
            {
                var listOfSoftwares = MachineProvider.GetInstance().GetListOfInstalledSoftwares(machineName);

                lslSoftware.DataSource = listOfSoftwares;
                lslSoftware.DisplayMember = "DisplayName";
                lblSoftware.Text = string.Format("Total no of installed software is {0}", listOfSoftwares == null ? 0 : listOfSoftwares.Count);
            }
            else
            {
                MessageBox.Show("Please login as domain admin to see list of installed software");
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

            lblSoftware.Text = "";
            lblInfo.Text = "";

            var domains = NetworkProvider.GetInstance().EnumerateDomains();

            var listOfMachines = NetworkProvider.GetInstance().DomainNetworkComputers(domains.FirstOrDefault());

            if (!string.IsNullOrEmpty(searchString))
            {
                listOfMachines = listOfMachines.Where(x => x.MachineName.Contains(txtFilter.Text)).ToList();
            }

            if (chkOnline.Checked)
            {
                listOfMachines = listOfMachines.Where(x => x.MachineStatus == MachineStatus.Online).ToList();
            }

            PopulateListView(listOfMachines);

        }


        public void PopulateListView(List<MachineEntity> listOfMachines)
        {
            lstView.Items.Clear();
            lstView.FullRowSelect = true;
            lslSoftware.DataSource = null;
            int online = 0;

            foreach (var item in listOfMachines)
            {
                ListViewItem lvi = new ListViewItem(Enum.GetName(typeof(MachineStatus), item.MachineStatus));
                lvi.SubItems.Add(item.MachineName);
                lvi.SubItems.Add(item.IPAddress);
                lvi.SubItems.Add(item.MachineMACAddress);
                lvi.SubItems.Add(item.OpratingSystem);
                lvi.SubItems.Add(item.OpratingSystemVersion);
                if (item.MachineStatus == MachineStatus.Online)
                {
                    lvi.ImageIndex = 1;
                    online++;
                }
                else
                {
                    lvi.ImageIndex = 0;
                }

                lstView.Items.Add(lvi);
            }

            lblInfo.Text = string.Format("Total no of machines is '{0}' currently online '{1}'", listOfMachines.Count, online);
        }

        private void lstView_Click(object sender, EventArgs e)
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

        private void Dashboard_Load(object sender, EventArgs e)
        {
            lstView.Items.Clear();
            lstView.FullRowSelect = true;
            lstView.SmallImageList = imgList;
            lslSoftware.DataSource = null;

            // Attach Subitems to the ListView
            lstView.Columns.Clear();
            lstView.Columns.Add("Machine Status", 100, HorizontalAlignment.Left);
            lstView.Columns.Add("Machine Name", 200, HorizontalAlignment.Left);
            lstView.Columns.Add("Machine IP", 80, HorizontalAlignment.Left);
            lstView.Columns.Add("Machine MAC", 80, HorizontalAlignment.Left);
            lstView.Columns.Add("Machine OS", 200, HorizontalAlignment.Left);
            lstView.Columns.Add("Machine OS Version", 200, HorizontalAlignment.Left);
        }
    }
}
