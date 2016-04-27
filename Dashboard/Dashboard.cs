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

        public List<MachineEntity>  ListOfMachines { get; set; }
        public List<NetworkDevices> ListOfNetworkDevices { get; set; }
        public string SelectedDomain { get; set; }
        

        public Dashboard()
        {
            InitializeComponent();
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            pgInfo.Maximum = 100;
            pgInfo.Minimum = 0;
            pgInfo.Value = 10;

            try
            {
                FillListOfMachines();
                pgInfo.Value = 100;
            }
            catch (Exception ex)
            {
                pgInfo.Value = 0;
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

        private void lstView_Click(object sender, EventArgs e)
        {
            try
            {
                GetListofSofwares();
                PopulateNetworkDevices();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void lstNetworkDevices_Click(object sender, EventArgs e)
        {
            GetListofIPAddresses();
        }
        
        private void Dashboard_Load(object sender, EventArgs e)
        {
            try
            {

                lblInfo.Text = "";
                lblIPAddresses.Text = "";
                lblNetworkDevices.Text = "";
                lblSoftware.Text = "";


                lstView.Items.Clear();
                lstView.FullRowSelect = true;
                lstView.SmallImageList = imgList;
                lslSoftware.DataSource = null;

                // Attach Subitems to the ListView
                lstView.Columns.Clear();
                lstView.Columns.Add("Machine Status", 100, HorizontalAlignment.Left);
                lstView.Columns.Add("Machine Name", 100, HorizontalAlignment.Left);
                lstView.Columns.Add("Machine IP", 80, HorizontalAlignment.Left);
                lstView.Columns.Add("Machine MAC", 80, HorizontalAlignment.Left);
                lstView.Columns.Add("Machine OS", 100, HorizontalAlignment.Left);
                lstView.Columns.Add("Machine OS Version", 150, HorizontalAlignment.Left);



                lstNetworkDevices.Items.Clear();
                lstNetworkDevices.FullRowSelect = true;


                lslSoftware.DataSource = null;

                // Attach Subitems to the ListView
                lstNetworkDevices.Columns.Clear();
                lstNetworkDevices.Columns.Add("Device ID", 60, HorizontalAlignment.Left);
                lstNetworkDevices.Columns.Add("Adapter Type", 100, HorizontalAlignment.Left);
                lstNetworkDevices.Columns.Add("Description", 200, HorizontalAlignment.Left);
                lstNetworkDevices.Columns.Add("MACAddress", 150, HorizontalAlignment.Left);
                lstNetworkDevices.Columns.Add("Manufacturer", 100, HorizontalAlignment.Left);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FillListOfMachines(string searchString = null)
        {

            lblSoftware.Text = "";
            lblInfo.Text = "";

            var domains = DomainProvider.GetInstance().EnumerateDomains();
            SelectedDomain = domains.FirstOrDefault();

            var listOfMachines = NetworkProvider.GetInstance().DomainNetworkComputers(SelectedDomain);
            ListOfMachines = listOfMachines;

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

        private void GetListofSofwares()
        {
            var machineName = lstView.SelectedItems[0].SubItems[1].Text;
            if (DomainProvider.GetInstance().IsDomainAdministrator || Dns.GetHostName() == machineName)
            {
                var listOfSoftwares = MachineProvider.GetInstance().GetListOfInstalledSoftwares(machineName);

                lslSoftware.DataSource = listOfSoftwares;
                lslSoftware.DisplayMember = "DisplayName";
                lblSoftware.Text = string.Format("{0} Software Installed on machine {1}", listOfSoftwares == null ? 0 : listOfSoftwares.Count,machineName);

            }
            else
            {
                MessageBox.Show("Please login as domain admin to see list of installed software");
            }
        }

        private void GetListofIPAddresses()
        {
            var macAddress = lstNetworkDevices.SelectedItems[0].SubItems[3].Text;
            var listOfIPAddresses = ListOfNetworkDevices.Where(x => x.MACaddress == macAddress).ToList().FirstOrDefault();
            if (DomainProvider.GetInstance().IsDomainAdministrator || Dns.GetHostName() == lstView.SelectedItems[0].SubItems[1].Text)
            {
                lstIPAddress.DataSource = listOfIPAddresses.IPAddresses;
            }
            else
            {
                MessageBox.Show("Please login as domain admin to see list of installed software");
            }

            lblIPAddresses.Text = string.Format("{0} IPAddresses found on machine {1} with MACAddress {2} ", listOfIPAddresses.IPAddresses == null ? 0 : listOfIPAddresses.IPAddresses.Length, lstView.SelectedItems[0].SubItems[1].Text,macAddress);
        }

        private void PopulateNetworkDevices()
        {
            var machineName = lstView.SelectedItems[0].SubItems[1].Text;

            var machineDetails =  ListOfMachines.Where(x => x.MachineName == machineName).ToList().FirstOrDefault();

            machineDetails = MachineProvider.GetInstance().GetMachineAdditionalInformation(machineName, SelectedDomain, machineDetails);

            
            ListOfNetworkDevices = machineDetails.ListOfNetworkDevices;

            foreach (var item in machineDetails.ListOfNetworkDevices)
            {
                ListViewItem lvi = new ListViewItem(item.DeviceID);
                lvi.SubItems.Add(item.Adaptertype);
                lvi.SubItems.Add(item.Description);
                lvi.SubItems.Add(item.MACaddress);
                lvi.SubItems.Add(item.Manufacturer);
                lstNetworkDevices.Items.Add(lvi);
            }

            lblNetworkDevices.Text = string.Format("{0} NetworkDevices Installed on machine :{1}", machineDetails.ListOfNetworkDevices == null ? 0 : machineDetails.ListOfNetworkDevices.Count, lstView.SelectedItems[0].SubItems[1].Text);
        }

       
    }
}
