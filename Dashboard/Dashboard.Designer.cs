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
            this.panel2 = new System.Windows.Forms.Panel();
            this.lstView = new System.Windows.Forms.ListView();
            this.chkOnline = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.btnList = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSoftware = new System.Windows.Forms.Label();
            this.lslSoftware = new System.Windows.Forms.ListBox();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.lstNetworkDevices = new System.Windows.Forms.ListView();
            this.lstIPAddress = new System.Windows.Forms.ListBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pgInfo
            // 
            this.pgInfo.Location = new System.Drawing.Point(12, 715);
            this.pgInfo.Name = "pgInfo";
            this.pgInfo.Size = new System.Drawing.Size(1215, 23);
            this.pgInfo.TabIndex = 8;
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(100, 15);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(240, 20);
            this.txtFilter.TabIndex = 9;
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(350, 15);
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
            this.panel1.Location = new System.Drawing.Point(14, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(456, 51);
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
            // panel2
            // 
            this.panel2.Controls.Add(this.lstView);
            this.panel2.Controls.Add(this.chkOnline);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.lblInfo);
            this.panel2.Controls.Add(this.btnList);
            this.panel2.Location = new System.Drawing.Point(14, 61);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(703, 411);
            this.panel2.TabIndex = 12;
            // 
            // lstView
            // 
            this.lstView.Location = new System.Drawing.Point(9, 36);
            this.lstView.Name = "lstView";
            this.lstView.Size = new System.Drawing.Size(683, 355);
            this.lstView.TabIndex = 15;
            this.lstView.UseCompatibleStateImageBehavior = false;
            this.lstView.View = System.Windows.Forms.View.Details;
            this.lstView.Click += new System.EventHandler(this.lstView_Click);
            // 
            // chkOnline
            // 
            this.chkOnline.AutoSize = true;
            this.chkOnline.Location = new System.Drawing.Point(404, 12);
            this.chkOnline.Name = "chkOnline";
            this.chkOnline.Size = new System.Drawing.Size(129, 17);
            this.chkOnline.TabIndex = 14;
            this.chkOnline.Text = "Online Machines Only";
            this.chkOnline.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Network Machines";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(9, 394);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(25, 13);
            this.lblInfo.TabIndex = 9;
            this.lblInfo.Text = "Info";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnList
            // 
            this.btnList.Location = new System.Drawing.Point(549, 8);
            this.btnList.Name = "btnList";
            this.btnList.Size = new System.Drawing.Size(141, 23);
            this.btnList.TabIndex = 7;
            this.btnList.Text = "Scan Network Machines";
            this.btnList.UseVisualStyleBackColor = true;
            this.btnList.Click += new System.EventHandler(this.btnList_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(720, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Installed list of Sofware";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblSoftware
            // 
            this.lblSoftware.AutoSize = true;
            this.lblSoftware.Location = new System.Drawing.Point(722, 453);
            this.lblSoftware.Name = "lblSoftware";
            this.lblSoftware.Size = new System.Drawing.Size(25, 13);
            this.lblSoftware.TabIndex = 15;
            this.lblSoftware.Text = "Info";
            this.lblSoftware.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lslSoftware
            // 
            this.lslSoftware.FormattingEnabled = true;
            this.lslSoftware.Location = new System.Drawing.Point(723, 30);
            this.lslSoftware.Name = "lslSoftware";
            this.lslSoftware.Size = new System.Drawing.Size(505, 420);
            this.lslSoftware.TabIndex = 14;
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
            this.lstNetworkDevices.Location = new System.Drawing.Point(23, 509);
            this.lstNetworkDevices.Name = "lstNetworkDevices";
            this.lstNetworkDevices.Size = new System.Drawing.Size(681, 172);
            this.lstNetworkDevices.TabIndex = 16;
            this.lstNetworkDevices.UseCompatibleStateImageBehavior = false;
            this.lstNetworkDevices.View = System.Windows.Forms.View.Details;
            this.lstNetworkDevices.Click += new System.EventHandler(this.lstNetworkDevices_Click);
            // 
            // lstIPAddress
            // 
            this.lstIPAddress.FormattingEnabled = true;
            this.lstIPAddress.Location = new System.Drawing.Point(725, 509);
            this.lstIPAddress.Name = "lstIPAddress";
            this.lstIPAddress.Size = new System.Drawing.Size(502, 173);
            this.lstIPAddress.TabIndex = 17;
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1243, 750);
            this.Controls.Add(this.lstIPAddress);
            this.Controls.Add(this.lstNetworkDevices);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblSoftware);
            this.Controls.Add(this.lslSoftware);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pgInfo);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.Name = "Dashboard";
            this.Text = "Network Scanner";
            this.Load += new System.EventHandler(this.Dashboard_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar pgInfo;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Button btnList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkOnline;
        private System.Windows.Forms.ListView lstView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSoftware;
        private System.Windows.Forms.ListBox lslSoftware;
        private System.Windows.Forms.ImageList imgList;
        private System.Windows.Forms.ListView lstNetworkDevices;
        private System.Windows.Forms.ListBox lstIPAddress;
    }
}

