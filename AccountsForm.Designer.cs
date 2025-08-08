namespace StorePOS
{
    partial class AccountsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.txtPhoneNumber = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.btnAddCustomer = new System.Windows.Forms.Button();
            this.dgvAccounts = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtChequeAmount = new System.Windows.Forms.TextBox();
            this.dtChequeDate = new System.Windows.Forms.DateTimePicker();
            this.cmbChequeType = new System.Windows.Forms.ComboBox();
            this.btnAddCheque = new System.Windows.Forms.Button();
            this.dgvCheques = new System.Windows.Forms.DataGridView();
            this.dtFromChequeFilter = new System.Windows.Forms.DateTimePicker();
            this.dtToChequeFilter = new System.Windows.Forms.DateTimePicker();
            this.btnFilterCheques = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnPrintCheques = new System.Windows.Forms.Button();
            this.btnEditCheque = new System.Windows.Forms.Button();
            this.btnDeleteCheque = new System.Windows.Forms.Button();
            this.btnEditCustomer = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccounts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCheques)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(255, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "costumer name :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(255, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "phone no. :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(255, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "address :";
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Location = new System.Drawing.Point(387, 26);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(100, 22);
            this.txtCustomerName.TabIndex = 3;
            // 
            // txtPhoneNumber
            // 
            this.txtPhoneNumber.Location = new System.Drawing.Point(387, 55);
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            this.txtPhoneNumber.Size = new System.Drawing.Size(100, 22);
            this.txtPhoneNumber.TabIndex = 4;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(387, 83);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(100, 22);
            this.txtAddress.TabIndex = 5;
            // 
            // btnAddCustomer
            // 
            this.btnAddCustomer.Location = new System.Drawing.Point(258, 125);
            this.btnAddCustomer.Name = "btnAddCustomer";
            this.btnAddCustomer.Size = new System.Drawing.Size(116, 44);
            this.btnAddCustomer.TabIndex = 6;
            this.btnAddCustomer.Text = "AddCustomer";
            this.btnAddCustomer.UseVisualStyleBackColor = true;
            this.btnAddCustomer.Click += new System.EventHandler(this.btnAddCustomer_Click);
            // 
            // dgvAccounts
            // 
            this.dgvAccounts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvAccounts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAccounts.Location = new System.Drawing.Point(25, 189);
            this.dgvAccounts.Name = "dgvAccounts";
            this.dgvAccounts.ReadOnly = true;
            this.dgvAccounts.RowHeadersWidth = 51;
            this.dgvAccounts.RowTemplate.Height = 24;
            this.dgvAccounts.Size = new System.Drawing.Size(678, 180);
            this.dgvAccounts.TabIndex = 7;
            this.dgvAccounts.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAccounts_CellClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(255, 393);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "cheque amount :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(255, 420);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "cheque date :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(255, 450);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 16);
            this.label6.TabIndex = 10;
            this.label6.Text = "cheque type :";
            // 
            // txtChequeAmount
            // 
            this.txtChequeAmount.Location = new System.Drawing.Point(387, 390);
            this.txtChequeAmount.Name = "txtChequeAmount";
            this.txtChequeAmount.Size = new System.Drawing.Size(100, 22);
            this.txtChequeAmount.TabIndex = 11;
            // 
            // dtChequeDate
            // 
            this.dtChequeDate.Location = new System.Drawing.Point(387, 420);
            this.dtChequeDate.Name = "dtChequeDate";
            this.dtChequeDate.Size = new System.Drawing.Size(200, 22);
            this.dtChequeDate.TabIndex = 12;
            // 
            // cmbChequeType
            // 
            this.cmbChequeType.FormattingEnabled = true;
            this.cmbChequeType.Location = new System.Drawing.Point(387, 450);
            this.cmbChequeType.Name = "cmbChequeType";
            this.cmbChequeType.Size = new System.Drawing.Size(121, 24);
            this.cmbChequeType.TabIndex = 13;
            // 
            // btnAddCheque
            // 
            this.btnAddCheque.Location = new System.Drawing.Point(336, 489);
            this.btnAddCheque.Name = "btnAddCheque";
            this.btnAddCheque.Size = new System.Drawing.Size(103, 43);
            this.btnAddCheque.TabIndex = 14;
            this.btnAddCheque.Text = "Add Cheque";
            this.btnAddCheque.UseVisualStyleBackColor = true;
            this.btnAddCheque.Click += new System.EventHandler(this.btnAddCheque_Click);
            // 
            // dgvCheques
            // 
            this.dgvCheques.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvCheques.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCheques.Location = new System.Drawing.Point(25, 553);
            this.dgvCheques.Name = "dgvCheques";
            this.dgvCheques.ReadOnly = true;
            this.dgvCheques.RowHeadersWidth = 51;
            this.dgvCheques.RowTemplate.Height = 24;
            this.dgvCheques.Size = new System.Drawing.Size(678, 186);
            this.dgvCheques.TabIndex = 15;
            this.dgvCheques.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCheques_CellClick);
            // 
            // dtFromChequeFilter
            // 
            this.dtFromChequeFilter.Location = new System.Drawing.Point(92, 760);
            this.dtFromChequeFilter.Name = "dtFromChequeFilter";
            this.dtFromChequeFilter.Size = new System.Drawing.Size(200, 22);
            this.dtFromChequeFilter.TabIndex = 16;
            // 
            // dtToChequeFilter
            // 
            this.dtToChequeFilter.Location = new System.Drawing.Point(357, 764);
            this.dtToChequeFilter.Name = "dtToChequeFilter";
            this.dtToChequeFilter.Size = new System.Drawing.Size(200, 22);
            this.dtToChequeFilter.TabIndex = 17;
            // 
            // btnFilterCheques
            // 
            this.btnFilterCheques.Location = new System.Drawing.Point(579, 760);
            this.btnFilterCheques.Name = "btnFilterCheques";
            this.btnFilterCheques.Size = new System.Drawing.Size(124, 35);
            this.btnFilterCheques.TabIndex = 18;
            this.btnFilterCheques.Text = "Filter Cheques";
            this.btnFilterCheques.UseVisualStyleBackColor = true;
            this.btnFilterCheques.Click += new System.EventHandler(this.btnFilterCheques_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 760);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 16);
            this.label7.TabIndex = 19;
            this.label7.Text = "from date :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(298, 764);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(54, 16);
            this.label8.TabIndex = 20;
            this.label8.Text = "to date :";
            // 
            // btnPrintCheques
            // 
            this.btnPrintCheques.Location = new System.Drawing.Point(194, 493);
            this.btnPrintCheques.Name = "btnPrintCheques";
            this.btnPrintCheques.Size = new System.Drawing.Size(124, 39);
            this.btnPrintCheques.TabIndex = 21;
            this.btnPrintCheques.Text = "Print Cheques";
            this.btnPrintCheques.UseVisualStyleBackColor = true;
            this.btnPrintCheques.Click += new System.EventHandler(this.btnPrintCheques_Click);
            // 
            // btnEditCheque
            // 
            this.btnEditCheque.Location = new System.Drawing.Point(455, 489);
            this.btnEditCheque.Name = "btnEditCheque";
            this.btnEditCheque.Size = new System.Drawing.Size(118, 43);
            this.btnEditCheque.TabIndex = 22;
            this.btnEditCheque.Text = "Edit Cheque";
            this.btnEditCheque.UseVisualStyleBackColor = true;
            this.btnEditCheque.Click += new System.EventHandler(this.btnEditCheque_Click);
            // 
            // btnDeleteCheque
            // 
            this.btnDeleteCheque.Location = new System.Drawing.Point(579, 489);
            this.btnDeleteCheque.Name = "btnDeleteCheque";
            this.btnDeleteCheque.Size = new System.Drawing.Size(112, 43);
            this.btnDeleteCheque.TabIndex = 23;
            this.btnDeleteCheque.Text = "Delete Cheque";
            this.btnDeleteCheque.UseVisualStyleBackColor = true;
            this.btnDeleteCheque.Click += new System.EventHandler(this.btnDeleteCheque_Click_1);
            // 
            // btnEditCustomer
            // 
            this.btnEditCustomer.Location = new System.Drawing.Point(387, 125);
            this.btnEditCustomer.Name = "btnEditCustomer";
            this.btnEditCustomer.Size = new System.Drawing.Size(116, 44);
            this.btnEditCustomer.TabIndex = 24;
            this.btnEditCustomer.Text = "Edit Customer";
            this.btnEditCustomer.UseVisualStyleBackColor = true;
            this.btnEditCustomer.Click += new System.EventHandler(this.btnEditCustomer_Click);
            // 
            // AccountsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(735, 853);
            this.Controls.Add(this.btnEditCustomer);
            this.Controls.Add(this.btnDeleteCheque);
            this.Controls.Add(this.btnEditCheque);
            this.Controls.Add(this.btnPrintCheques);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnFilterCheques);
            this.Controls.Add(this.dtToChequeFilter);
            this.Controls.Add(this.dtFromChequeFilter);
            this.Controls.Add(this.dgvCheques);
            this.Controls.Add(this.btnAddCheque);
            this.Controls.Add(this.cmbChequeType);
            this.Controls.Add(this.dtChequeDate);
            this.Controls.Add(this.txtChequeAmount);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dgvAccounts);
            this.Controls.Add(this.btnAddCustomer);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.txtPhoneNumber);
            this.Controls.Add(this.txtCustomerName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "AccountsForm";
            this.Text = "AccountsForm";
            this.Load += new System.EventHandler(this.AccountsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccounts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCheques)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.TextBox txtPhoneNumber;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Button btnAddCustomer;
        private System.Windows.Forms.DataGridView dgvAccounts;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtChequeAmount;
        private System.Windows.Forms.DateTimePicker dtChequeDate;
        private System.Windows.Forms.ComboBox cmbChequeType;
        private System.Windows.Forms.Button btnAddCheque;
        private System.Windows.Forms.DataGridView dgvCheques;
        private System.Windows.Forms.DateTimePicker dtFromChequeFilter;
        private System.Windows.Forms.DateTimePicker dtToChequeFilter;
        private System.Windows.Forms.Button btnFilterCheques;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnPrintCheques;
        private System.Windows.Forms.Button btnEditCheque;
        private System.Windows.Forms.Button btnDeleteCheque;
        private System.Windows.Forms.Button btnEditCustomer;
    }
}