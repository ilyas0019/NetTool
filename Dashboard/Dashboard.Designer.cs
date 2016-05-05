namespace Dashboard
{
    partial class Dashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dashboard));
            this.pgInfo = new System.Windows.Forms.ProgressBar();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.btnFilter = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.lblSoftware = new System.Windows.Forms.Label();
            this.lstSoftware = new System.Windows.Forms.ListBox();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.lstNetworkDevices = new System.Windows.Forms.ListView();
            this.lstIPAddress = new System.Windows.Forms.ListBox();
            this.lblNetworkDevices = new System.Windows.Forms.Label();
            this.lblIPAddresses = new System.Windows.Forms.Label();
            this.lstView = new System.Windows.Forms.ListView();
            this.chkOnline = new System.Windows.Forms.CheckBox();
            this.btnList = new System.Windows.Forms.Button();
            this.lblStorage = new System.Windows.Forms.Label();
            this.lstStorage = new System.Windows.Forms.ListView();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblDomain = new System.Windows.Forms.Label();
            this.lstDomain = new System.Windows.Forms.ListBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pgInfo
            // 
            this.pgInfo.Location = new System.Drawing.Point(12, 615);
            this.pgInfo.Name = "pgInfo";
            this.pgInfo.Size = new System.Drawing.Size(1215, 15);
            this.pgInfo.TabIndex = 8;
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(100, 15);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(102, 20);
            this.txtFilter.TabIndex = 9;
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(208, 13);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(75, 23);
            this.btnFilter.TabIndex = 10;
            this.btnFilter.Text = "Filter";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnFilter);
            this.panel1.Controls.Add(this.txtFilter);
            this.panel1.Location = new System.Drawing.Point(395, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 51);
            this.panel1.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Search Machine ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblSoftware
            // 
            this.lblSoftware.AutoSize = true;
            this.lblSoftware.Location = new System.Drawing.Point(706, 14);
            this.lblSoftware.Name = "lblSoftware";
            this.lblSoftware.Size = new System.Drawing.Size(25, 13);
            this.lblSoftware.TabIndex = 15;
            this.lblSoftware.Text = "Info";
            this.lblSoftware.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lstSoftware
            // 
            this.lstSoftware.FormattingEnabled = true;
            this.lstSoftware.Location = new System.Drawing.Point(703, 30);
            this.lstSoftware.Name = "lstSoftware";
            this.lstSoftware.Size = new System.Drawing.Size(524, 251);
            this.lstSoftware.TabIndex = 14;
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "OFF.jpg");
            this.imgList.Images.SetKeyName(1, "ON.jpg");
            // 
            // lstNetworkDevices
            // 
            this.lstNetworkDevices.Location = new System.Drawing.Point(12, 437);
            this.lstNetworkDevices.Name = "lstNetworkDevices";
            this.lstNetworkDevices.Size = new System.Drawing.Size(683, 172);
            this.lstNetworkDevices.TabIndex = 16;
            this.lstNetworkDevices.UseCompatibleStateImageBehavior = false;
            this.lstNetworkDevices.View = System.Windows.Forms.View.Details;
            this.lstNetworkDevices.Click += new System.EventHandler(this.lstNetworkDevices_Click);
            // 
            // lstIPAddress
            // 
            this.lstIPAddress.FormattingEnabled = true;
            this.lstIPAddress.Location = new System.Drawing.Point(704, 501);
            this.lstIPAddress.Name = "lstIPAddress";
            this.lstIPAddress.Size = new System.Drawing.Size(523, 108);
            this.lstIPAddress.TabIndex = 17;
            // 
            // lblNetworkDevices
            // 
            this.lblNetworkDevices.AutoSize = true;
            this.lblNetworkDevices.Location = new System.Drawing.Point(12, 421);
            this.lblNetworkDevices.Name = "lblNetworkDevices";
            this.lblNetworkDevices.Size = new System.Drawing.Size(25, 13);
            this.lblNetworkDevices.TabIndex = 18;
            this.lblNetworkDevices.Text = "Info";
            this.lblNetworkDevices.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblIPAddresses
            // 
            this.lblIPAddresses.AutoSize = true;
            this.lblIPAddresses.Location = new System.Drawing.Point(703, 485);
            this.lblIPAddresses.Name = "lblIPAddresses";
            this.lblIPAddresses.Size = new System.Drawing.Size(25, 13);
            this.lblIPAddresses.TabIndex = 19;
            this.lblIPAddresses.Text = "Info";
            this.lblIPAddresses.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lstView
            // 
            this.lstView.Location = new System.Drawing.Point(14, 112);
            this.lstView.Name = "lstView";
            this.lstView.Size = new System.Drawing.Size(683, 297);
            this.lstView.TabIndex = 24;
            this.lstView.UseCompatibleStateImageBehavior = false;
            this.lstView.View = System.Windows.Forms.View.Details;
            this.lstView.Click += new System.EventHandler(this.lstView_Click);
            // 
            // chkOnline
            // 
            this.chkOnline.AutoSize = true;
            this.chkOnline.Location = new System.Drawing.Point(395, 87);
            this.chkOnline.Name = "chkOnline";
            this.chkOnline.Size = new System.Drawing.Size(129, 17);
            this.chkOnline.TabIndex = 23;
            this.chkOnline.Text = "Online Machines Only";
            this.chkOnline.UseVisualStyleBackColor = true;
            // 
            // btnList
            // 
            this.btnList.Location = new System.Drawing.Point(554, 83);
            this.btnList.Name = "btnList";
            this.btnList.Size = new System.Drawing.Size(141, 23);
            this.btnList.TabIndex = 20;
            this.btnList.Text = "Scan Network Machines";
            this.btnList.UseVisualStyleBackColor = true;
            this.btnList.Click += new System.EventHandler(this.btnList_Click);
            // 
            // lblStorage
            // 
            this.lblStorage.AutoSize = true;
            this.lblStorage.Location = new System.Drawing.Point(702, 289);
            this.lblStorage.Name = "lblStorage";
            this.lblStorage.Size = new System.Drawing.Size(25, 13);
            this.lblStorage.TabIndex = 26;
            this.lblStorage.Text = "Info";
            this.lblStorage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lstStorage
            // 
            this.lstStorage.Location = new System.Drawing.Point(703, 310);
            this.lstStorage.Name = "lstStorage";
            this.lstStorage.Size = new System.Drawing.Size(524, 172);
            this.lstStorage.TabIndex = 25;
            this.lstStorage.UseCompatibleStateImageBehavior = false;
            this.lstStorage.View = System.Windows.Forms.View.Details;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(12, 93);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(25, 13);
            this.lblInfo.TabIndex = 27;
            this.lblInfo.Text = "Info";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblDomain
            // 
            this.lblDomain.AutoSize = true;
            this.lblDomain.Location = new System.Drawing.Point(18, 14);
            this.lblDomain.Name = "lblDomain";
            this.lblDomain.Size = new System.Drawing.Size(25, 13);
            this.lblDomain.TabIndex = 29;
            this.lblDomain.Text = "Info";
            this.lblDomain.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lstDomain
            // 
            this.lstDomain.FormattingEnabled = true;
            this.lstDomain.Location = new System.Drawing.Point(15, 30);
            this.lstDomain.Name = "lstDomain";
            this.lstDomain.Size = new System.Drawing.Size(363, 56);
            this.lstDomain.TabIndex = 28;
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1240, 657);
            this.Controls.Add(this.lblDomain);
            this.Controls.Add(this.lstDomain);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.lblStorage);
            this.Controls.Add(this.lstStorage);
            this.Controls.Add(this.lstView);
            this.Controls.Add(this.chkOnline);
            this.Controls.Add(this.btnList);
            this.Controls.Add(this.lblIPAddresses);
            this.Controls.Add(this.lblNetworkDevices);
            this.Controls.Add(this.lstIPAddress);
            this.Controls.Add(this.lstNetworkDevices);
            this.Controls.Add(this.lblSoftware);
            this.Controls.Add(this.lstSoftware);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pgInfo);
            this.DoubleBuffered = true;
            this.Name = "Dashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Network Scanner";
            this.Load += new System.EventHandler(this.Dashboard_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar pgInfo;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblSoftware;
        private System.Windows.Forms.ListBox lstSoftware;
        private System.Windows.Forms.ImageList imgList;
        private System.Windows.Forms.ListView lstNetworkDevices;
        private System.Windows.Forms.ListBox lstIPAddress;
        private System.Windows.Forms.Label lblNetworkDevices;
        private System.Windows.Forms.Label lblIPAddresses;
        private System.Windows.Forms.ListView lstView;
        private System.Windows.Forms.CheckBox chkOnline;
        private System.Windows.Forms.Button btnList;
        private System.Windows.Forms.Label lblStorage;
        private System.Windows.Forms.ListView lstStorage;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblDomain;
        private System.Windows.Forms.ListBox lstDomain;
    }
}

