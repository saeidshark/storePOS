using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace StorePOS
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            ShowDateAndTime();
        }

        private void ShowDateAndTime()
        {
            lblDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss");

            timer1.Interval = 1000;
            timer1.Tick += Timer1_Tick;
            timer1.Start();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            var login = new LoginForm();
            var result = login.ShowDialog();

            if (result == DialogResult.OK)
            {
                var invoiceForm = new InvoiceForm();
                invoiceForm.ShowDialog();
            }
        }

        private void btnUserManagement_Click(object sender, EventArgs e)
        {
            var authForm = new AdminAuthForm();
            authForm.ShowDialog();

            if (authForm.IsAuthenticated)
            {
                var userForm = new UserManagementForm();
                userForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("شما مجاز به دسترسی به این بخش نیستید.");
            }
        }

        private void report_Click(object sender, EventArgs e)
        {
            var ReportForm = new ReportForm();
            ReportForm.ShowDialog();
        }

        private void productsfom_Click(object sender, EventArgs e)
        {
            var productsform = new ProductsForm();
            productsform.ShowDialog();
        }

        private void purchase_Click(object sender, EventArgs e)
        {
            // ابتدا فرم لاگین را نمایش بده
            var loginForm = new LoginForm();
            var result = loginForm.ShowDialog();

            // اگر لاگین موفق بود، فرم فاکتور خرید را باز کن
            if (result == DialogResult.OK)
            {
                var purchaseForm = new PurchaseInvoiceForm();
                purchaseForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("ورود ناموفق بود یا لغو شد.");
            }
        }

        private void btnProfitLoss_Click(object sender, EventArgs e)
        {
            var authForm = new AdminAuthForm();
            authForm.ShowDialog();

            if (authForm.IsAuthenticated)
            {
                var profitForm = new ProfitLossForm(); // فرمی که می‌سازیم
                profitForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("شما مجاز به دسترسی به این بخش نیستید.");
            }
        }

        private void accountcheques_Click(object sender, EventArgs e)
        {
            var AccountsForm = new AccountsForm();
            AccountsForm.ShowDialog();
        }

        private void perfectreport_Click(object sender, EventArgs e)
        {
            var PerfectReport = new perfectReportsForm();
            PerfectReport.ShowDialog();
        }
    }

}
