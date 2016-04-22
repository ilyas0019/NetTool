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
            this.label2 = new System.Windows.Forms.Label();
            this.pgInfo = new System.Windows.Forms.ProgressBar();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.btnFilter = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblSoftware = new System.Windows.Forms.Label();
            this.lslSoftware = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lstView = new System.Windows.Forms.ListBox();
            this.btnList = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(360, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Installed list of Sofware";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pgInfo
            // 
            this.pgInfo.Location = new System.Drawing.Point(13, 479);
            this.pgInfo.Name = "pgInfo";
            this.pgInfo.Size = new System.Drawing.Size(702, 23);
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
            this.panel1.Size = new System.Drawing.Size(703, 51);
            this.panel1.TabIndex = 11;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblSoftware);
            this.panel2.Controls.Add(this.lslSoftware);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.lblInfo);
            this.panel2.Controls.Add(this.lstView);
            this.panel2.Controls.Add(this.btnList);
            this.panel2.Location = new System.Drawing.Point(14, 62);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(701, 411);
            this.panel2.TabIndex = 12;
            // 
            // lblSoftware
            // 
            this.lblSoftware.AutoSize = true;
            this.lblSoftware.Location = new System.Drawing.Point(347, 391);
            this.lblSoftware.Name = "lblSoftware";
            this.lblSoftware.Size = new System.Drawing.Size(25, 13);
            this.lblSoftware.TabIndex = 12;
            this.lblSoftware.Text = "Info";
            this.lblSoftware.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lslSoftware
            // 
            this.lslSoftware.FormattingEnabled = true;
            this.lslSoftware.Location = new System.Drawing.Point(341, 33);
            this.lslSoftware.Name = "lslSoftware";
            this.lslSoftware.Size = new System.Drawing.Size(352, 355);
            this.lslSoftware.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Network Machines";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(-1, 395);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(25, 13);
            this.lblInfo.TabIndex = 9;
            this.lblInfo.Text = "Info";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lstView
            // 
            this.lstView.FormattingEnabled = true;
            this.lstView.Location = new System.Drawing.Point(9, 37);
            this.lstView.Name = "lstView";
            this.lstView.Size = new System.Drawing.Size(315, 355);
            this.lstView.TabIndex = 8;
            this.lstView.Click += new System.EventHandler(this.lslSoftware_Click);
            // 
            // btnList
            // 
            this.btnList.Location = new System.Drawing.Point(556, 4);
            this.btnList.Name = "btnList";
            this.btnList.Size = new System.Drawing.Size(140, 23);
            this.btnList.TabIndex = 7;
            this.btnList.Text = "List network machines";
            this.btnList.UseVisualStyleBackColor = true;
            this.btnList.Click += new System.EventHandler(this.btnList_Click);
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
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 513);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pgInfo);
            this.Controls.Add(this.label2);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.Name = "Dashboard";
            this.Text = "Network Scanner";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar pgInfo;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblSoftware;
        private System.Windows.Forms.ListBox lslSoftware;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.ListBox lstView;
        private System.Windows.Forms.Button btnList;
        private System.Windows.Forms.Label label3;
    }
}

