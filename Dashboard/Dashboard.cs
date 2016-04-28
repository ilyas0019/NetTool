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

        public List<MachineEntity> ListOfMachines { get; set; }
        public List<NetworkDevices> ListOfNetworkDevices { get; set; }
        public string SelectedDomain { get; set; }
        public string SelectedMachineName { get; set; }


        public Dashboard()
        {
            InitializeComponent();
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            ResetPager(0);
            try
            {
                FillListOfMachines();

            }
            catch (Exception ex)
            {
                ResetPager(0);
                MessageBox.Show(ex.Message);
            }

        }

        private void ClearForm()
        {
            lblInfo.Text = "";
            lblIPAddresses.Text = "";
            lblNetworkDevices.Text = "";
            lblStorage.Text = "";
            lblSoftware.Text = "";

            lstView.Items.Clear();
            lstNetworkDevices.Items.Clear();
            lstStorage.Items.Clear();
            lstIPAddress.DataSource = null;
            lstSoftware.DataSource = null;

        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            ResetPager(0);
            if (txtFilter.Text.ToUpper().Trim() != "")
            {
                FillListOfMachines(txtFilter.Text.ToUpper());
            }
            else
            {
                ResetPager(0);
                MessageBox.Show("Please enter machine name");
            }
        }

        private void lstView_Click(object sender, EventArgs e)
        {
            try
            {

                lstNetworkDevices.Items.Clear();
                lstIPAddress.DataSource = null;
                lstStorage.Items.Clear();
                SelectedMachineName = lstView.SelectedItems[0].SubItems[1].Text;
                if (DomainProvider.GetInstance().IsDomainAdministrator || Dns.GetHostName() == SelectedMachineName)
                {
                    GetListofSofwares();
                    PopulateNetworkDevices();
                    PopulateStorage();
                }
                else
                {
                    MessageBox.Show("Please login as domain admin to see other system details");
                }



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

        private void PopulateStorage()
        {
            var objMachine = ListOfMachines.Where(x => x.MachineName == SelectedMachineName).ToList().FirstOrDefault();
            objMachine = MachineProvider.GetInstance().GetStorageInfoOfMachine(SelectedMachineName, SelectedDomain, objMachine);
            lstStorage.Items.Clear();
            lstStorage.FullRowSelect = true;
            ListViewItem lvi;
            foreach (var item in objMachine.ListOfStoragekDevices)
            {
                lvi = new ListViewItem(item.Name);
                lvi.SubItems.Add(item.SerialNumber);
                lvi.SubItems.Add(item.FreeSpace);

                lstStorage.Items.Add(lvi);
            }



            lblStorage.Text = string.Format("{0} Storage Devices found on machine {1} ", objMachine.ListOfStoragekDevices == null ? 0 : objMachine.ListOfStoragekDevices.Count, SelectedMachineName);
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            try
            {

                ClearForm();


                lstView.Items.Clear();
                lstView.FullRowSelect = true;
                lstView.SmallImageList = imgList;
                lstSoftware.DataSource = null;

                // Attach Subitems to the ListView
                lstView.Columns.Clear();
                lstView.Columns.Add("Machine Status", 100, HorizontalAlignment.Left);
                lstView.Columns.Add("Machine Name", 100, HorizontalAlignment.Left);
                lstView.Columns.Add("Machine IP", 80, HorizontalAlignment.Left);
                lstView.Columns.Add("Machine MAC", 80, HorizontalAlignment.Left);
                lstView.Columns.Add("Machine OS", 100, HorizontalAlignment.Left);
                lstView.Columns.Add("Machine OS Version", 150, HorizontalAlignment.Left);
                lstView.Columns.Add("Loggedin User", 150, HorizontalAlignment.Left);



                lstNetworkDevices.Items.Clear();
                lstNetworkDevices.FullRowSelect = true;


                lstSoftware.DataSource = null;

                // Attach Subitems to the ListView
                lstNetworkDevices.Columns.Clear();
                lstNetworkDevices.Columns.Add("Device ID", 60, HorizontalAlignment.Left);
                lstNetworkDevices.Columns.Add("Adapter Type", 100, HorizontalAlignment.Left);
                lstNetworkDevices.Columns.Add("Description", 200, HorizontalAlignment.Left);
                lstNetworkDevices.Columns.Add("MACAddress", 150, HorizontalAlignment.Left);
                lstNetworkDevices.Columns.Add("Manufacturer", 100, HorizontalAlignment.Left);

                lstStorage.Columns.Clear();
                lstStorage.Columns.Add("Name", 50, HorizontalAlignment.Left);
                lstStorage.Columns.Add("Serial Number", 100, HorizontalAlignment.Left);
                lstStorage.Columns.Add("Free Space(GB)", 150, HorizontalAlignment.Left);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FillListOfMachines(string searchString = null)
        {
            ClearForm();
            lblSoftware.Text = "";
            lblInfo.Text = "";

            var domains = DomainProvider.GetInstance().EnumerateDomains();
            SelectedDomain = domains.FirstOrDefault();

            var listOfMachines = NetworkProvider.GetInstance().DomainNetworkComputers(SelectedDomain);
            ListOfMachines = listOfMachines;

            if (!string.IsNullOrEmpty(searchString))
            {
                listOfMachines = listOfMachines.Where(x => x.MachineName.Contains(searchString)).ToList();
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
            lstSoftware.DataSource = null;
            int online = 0;
            MachineEntity objMachine = new MachineEntity();

            ResetPager(listOfMachines.Count);

            foreach (var item in listOfMachines)
            {

                objMachine = item;


                if (DomainProvider.GetInstance().IsDomainAdministrator || Dns.GetHostName() == item.MachineName)
                {
                    objMachine = MachineProvider.GetInstance().GetMachineAdditionalInformation(item.MachineName, item.DomainName, objMachine);
                }

                ListViewItem lvi = new ListViewItem(Enum.GetName(typeof(MachineStatus), item.MachineStatus));
                lvi.SubItems.Add(objMachine.MachineName);
                lvi.SubItems.Add(objMachine.IPAddress);
                lvi.SubItems.Add(objMachine.MachineMACAddress);
                lvi.SubItems.Add(objMachine.OpratingSystem);
                lvi.SubItems.Add(objMachine.OpratingSystemVersion);
                lvi.SubItems.Add(objMachine.LoggedInUser);
                if (objMachine.MachineStatus == MachineStatus.Online)
                {
                    lvi.ImageIndex = 1;
                    online++;
                }
                else
                {
                    lvi.ImageIndex = 0;
                }

                lstView.Items.Add(lvi);
                pgInfo.Value++;
            }

            lblInfo.Text = string.Format("Total no of machines is '{0}' currently online '{1}'", listOfMachines.Count, online);
        }

        private void ResetPager(int maxValue)
        {
            pgInfo.Minimum = 0;
            pgInfo.Value = 0;
            pgInfo.Maximum = maxValue;
        }

        private void GetListofSofwares()
        {

            SelectedMachineName = lstView.SelectedItems[0].SubItems[1].Text;

            var listOfSoftwares = MachineProvider.GetInstance().GetListOfInstalledSoftwares(SelectedMachineName);

            lstSoftware.DataSource = listOfSoftwares;
            lstSoftware.DisplayMember = "DisplayName";
            lblSoftware.Text = string.Format("{0} Software Installed on machine {1}", listOfSoftwares == null ? 0 : listOfSoftwares.Count, SelectedMachineName);

        }

        private void GetListofIPAddresses()
        {
            var macAddress = lstNetworkDevices.SelectedItems[0].SubItems[3].Text;
            var listOfIPAddresses = ListOfNetworkDevices.Where(x => x.MACaddress == macAddress).ToList().FirstOrDefault();

            lstIPAddress.DataSource = listOfIPAddresses.IPAddresses;

            lblIPAddresses.Text = string.Format("{0} IPAddresses found on machine {1} with MACAddress {2} ", listOfIPAddresses.IPAddresses == null ? 0 : listOfIPAddresses.IPAddresses.Length, lstView.SelectedItems[0].SubItems[1].Text, macAddress);
        }

        private void PopulateNetworkDevices()
        {
            var machineName = lstView.SelectedItems[0].SubItems[1].Text;

            var machineDetails = ListOfMachines.Where(x => x.MachineName == machineName).ToList().FirstOrDefault();


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
