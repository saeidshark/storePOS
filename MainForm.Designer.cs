namespace StorePOS
{
    partial class MainForm
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
            this.lblDate = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnManageUsers = new System.Windows.Forms.Button();
            this.report = new System.Windows.Forms.Button();
            this.productsfom = new System.Windows.Forms.Button();
            this.purchase = new System.Windows.Forms.Button();
            this.btnProfitLoss = new System.Windows.Forms.Button();
            this.accountcheques = new System.Windows.Forms.Button();
            this.perfectreport = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(12, 9);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(34, 16);
            this.lblDate.TabIndex = 0;
            this.lblDate.Text = "date";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(12, 34);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(32, 16);
            this.lblTime.TabIndex = 1;
            this.lblTime.Text = "time";
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(24, 88);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(111, 40);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "INVOICE";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click_1);
            // 
            // btnManageUsers
            // 
            this.btnManageUsers.Location = new System.Drawing.Point(24, 134);
            this.btnManageUsers.Name = "btnManageUsers";
            this.btnManageUsers.Size = new System.Drawing.Size(111, 40);
            this.btnManageUsers.TabIndex = 3;
            this.btnManageUsers.Text = "user management";
            this.btnManageUsers.UseVisualStyleBackColor = true;
            this.btnManageUsers.Click += new System.EventHandler(this.btnUserManagement_Click);
            // 
            // report
            // 
            this.report.Location = new System.Drawing.Point(24, 180);
            this.report.Name = "report";
            this.report.Size = new System.Drawing.Size(111, 40);
            this.report.TabIndex = 4;
            this.report.Text = "sales report";
            this.report.UseVisualStyleBackColor = true;
            this.report.Click += new System.EventHandler(this.report_Click);
            // 
            // productsfom
            // 
            this.productsfom.Location = new System.Drawing.Point(141, 88);
            this.productsfom.Name = "productsfom";
            this.productsfom.Size = new System.Drawing.Size(111, 40);
            this.productsfom.TabIndex = 5;
            this.productsfom.Text = "products";
            this.productsfom.UseVisualStyleBackColor = true;
            this.productsfom.Click += new System.EventHandler(this.productsfom_Click);
            // 
            // purchase
            // 
            this.purchase.Location = new System.Drawing.Point(141, 134);
            this.purchase.Name = "purchase";
            this.purchase.Size = new System.Drawing.Size(111, 40);
            this.purchase.TabIndex = 6;
            this.purchase.Text = "Purchase";
            this.purchase.UseVisualStyleBackColor = true;
            this.purchase.Click += new System.EventHandler(this.purchase_Click);
            // 
            // btnProfitLoss
            // 
            this.btnProfitLoss.Location = new System.Drawing.Point(141, 180);
            this.btnProfitLoss.Name = "btnProfitLoss";
            this.btnProfitLoss.Size = new System.Drawing.Size(111, 40);
            this.btnProfitLoss.TabIndex = 7;
            this.btnProfitLoss.Text = "ProfitLoss";
            this.btnProfitLoss.UseVisualStyleBackColor = true;
            this.btnProfitLoss.Click += new System.EventHandler(this.btnProfitLoss_Click);
            // 
            // accountcheques
            // 
            this.accountcheques.Location = new System.Drawing.Point(24, 226);
            this.accountcheques.Name = "accountcheques";
            this.accountcheques.Size = new System.Drawing.Size(111, 46);
            this.accountcheques.TabIndex = 8;
            this.accountcheques.Text = "accounts and cheques";
            this.accountcheques.UseVisualStyleBackColor = true;
            this.accountcheques.Click += new System.EventHandler(this.accountcheques_Click);
            // 
            // perfectreport
            // 
            this.perfectreport.Location = new System.Drawing.Point(141, 226);
            this.perfectreport.Name = "perfectreport";
            this.perfectreport.Size = new System.Drawing.Size(111, 46);
            this.perfectreport.TabIndex = 9;
            this.perfectreport.Text = "perfect report";
            this.perfectreport.UseVisualStyleBackColor = true;
            this.perfectreport.Click += new System.EventHandler(this.perfectreport_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 335);
            this.Controls.Add(this.perfectreport);
            this.Controls.Add(this.accountcheques);
            this.Controls.Add(this.btnProfitLoss);
            this.Controls.Add(this.purchase);
            this.Controls.Add(this.productsfom);
            this.Controls.Add(this.report);
            this.Controls.Add(this.btnManageUsers);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lblDate);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnManageUsers;
        private System.Windows.Forms.Button report;
        private System.Windows.Forms.Button productsfom;
        private System.Windows.Forms.Button purchase;
        private System.Windows.Forms.Button btnProfitLoss;
        private System.Windows.Forms.Button accountcheques;
        private System.Windows.Forms.Button perfectreport;
    }
}

