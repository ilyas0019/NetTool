using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Net;
using System.DirectoryServices;
using System.Runtime.InteropServices;


namespace HostNIPAddr
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		internal System.Windows.Forms.Button ButClose;
		internal System.Windows.Forms.Button ButDisplay;
		internal System.Windows.Forms.ListView ListHostIP;
		internal System.Windows.Forms.ColumnHeader HostName;
		internal System.Windows.Forms.ColumnHeader IpAddr;
		internal System.Windows.Forms.StatusBar Status;
		private System.Windows.Forms.TextBox TxtWorkGroup;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ColumnHeader PhyAdd;

		[DllImport("iphlpapi.dll", ExactSpelling=true)]
		public static extern int SendARP( int DestIP, int SrcIP, [Out] byte[] pMacAddr, ref int PhyAddrLen );


		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Form1));
			this.ButClose = new System.Windows.Forms.Button();
			this.ButDisplay = new System.Windows.Forms.Button();
			this.ListHostIP = new System.Windows.Forms.ListView();
			this.HostName = new System.Windows.Forms.ColumnHeader();
			this.IpAddr = new System.Windows.Forms.ColumnHeader();
			this.PhyAdd = new System.Windows.Forms.ColumnHeader();
			this.Status = new System.Windows.Forms.StatusBar();
			this.TxtWorkGroup = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// ButClose
			// 
			this.ButClose.Location = new System.Drawing.Point(380, 272);
			this.ButClose.Name = "ButClose";
			this.ButClose.Size = new System.Drawing.Size(84, 20);
			this.ButClose.TabIndex = 7;
			this.ButClose.Text = "Close";
			this.ButClose.Click += new System.EventHandler(this.ButClose_Click);
			// 
			// ButDisplay
			// 
			this.ButDisplay.Location = new System.Drawing.Point(380, 252);
			this.ButDisplay.Name = "ButDisplay";
			this.ButDisplay.Size = new System.Drawing.Size(84, 20);
			this.ButDisplay.TabIndex = 6;
			this.ButDisplay.Text = "Display";
			this.ButDisplay.Click += new System.EventHandler(this.ButDisplay_Click);
			// 
			// ListHostIP
			// 
			this.ListHostIP.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						 this.HostName,
																						 this.IpAddr,
																						 this.PhyAdd});
			this.ListHostIP.Location = new System.Drawing.Point(40, 16);
			this.ListHostIP.Name = "ListHostIP";
			this.ListHostIP.Size = new System.Drawing.Size(304, 272);
			this.ListHostIP.TabIndex = 5;
			this.ListHostIP.View = System.Windows.Forms.View.Details;
			// 
			// HostName
			// 
			this.HostName.Text = "Host Name";
			this.HostName.Width = 100;
			// 
			// IpAddr
			// 
			this.IpAddr.Text = "IP Address";
			this.IpAddr.Width = 100;
			// 
			// PhyAdd
			// 
			this.PhyAdd.Text = "Physical Address";
			this.PhyAdd.Width = 125;
			// 
			// Status
			// 
			this.Status.Location = new System.Drawing.Point(0, 303);
			this.Status.Name = "Status";
			this.Status.Size = new System.Drawing.Size(472, 22);
			this.Status.TabIndex = 4;
			// 
			// TxtWorkGroup
			// 
			this.TxtWorkGroup.Location = new System.Drawing.Point(352, 80);
			this.TxtWorkGroup.Name = "TxtWorkGroup";
			this.TxtWorkGroup.Size = new System.Drawing.Size(80, 20);
			this.TxtWorkGroup.TabIndex = 8;
			this.TxtWorkGroup.Text = "Baba";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(356, 56);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(98, 16);
			this.label1.TabIndex = 9;
			this.label1.Text = "Work Group Name";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(472, 325);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.TxtWorkGroup);
			this.Controls.Add(this.ButClose);
			this.Controls.Add(this.ButDisplay);
			this.Controls.Add(this.ListHostIP);
			this.Controls.Add(this.Status);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(480, 352);
			this.MinimumSize = new System.Drawing.Size(480, 352);
			this.Name = "Form1";
			this.Text = "System Info";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void ButDisplay_Click(object sender, System.EventArgs e)
		{
			try
			{

				this.Status.Text = "Collecting Information...";

				if(this.TxtWorkGroup.Text.Trim() == "")
				{
					MessageBox.Show("The Work Group name Should Not be Empty");
					return;
				}
			
					
				// Use Your work Group WinNT://&&&&(Work Group Name)
				DirectoryEntry DomainEntry = new DirectoryEntry("WinNT://" + this.TxtWorkGroup.Text.Trim());
				DomainEntry.Children.SchemaFilter.Add("computer");
			

				// To Get all the System names And Display with the Ip Address
				foreach(DirectoryEntry machine in DomainEntry.Children)
				{
					string[] Ipaddr = new string[3];
					Ipaddr[0] = machine.Name;

					System.Net.IPHostEntry Tempaddr = null;

					try
					{
						Tempaddr = (System.Net.IPHostEntry)Dns.GetHostByName(machine.Name);
					}
					catch(Exception ex)
					{
						MessageBox.Show("Unable to connect woth the system :" + machine.Name );
						continue;
					}
					System.Net.IPAddress[] TempAd = Tempaddr.AddressList;
					foreach(IPAddress TempA in TempAd)
					{
						Ipaddr[1] = TempA.ToString();

						byte[] ab = new byte[6];
						int len = ab.Length;

						// This Function Used to Get The Physical Address
						int r = SendARP( (int) TempA.Address, 0, ab, ref len );
						string mac = BitConverter.ToString( ab, 0, 6 );

						Ipaddr[2] = mac;
					}			

					System.Windows.Forms.ListViewItem TempItem = new ListViewItem(Ipaddr);

					this.ListHostIP.Items.Add(TempItem);
				}

				this.Status.Text = "Displayed";
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message,"Error",System.Windows.Forms.MessageBoxButtons.OK  );
				Application.Exit();
			}
		
		}

		private void ButClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
