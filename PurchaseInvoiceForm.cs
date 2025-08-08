using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace StorePOS
{
    public partial class PurchaseInvoiceForm: Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

        public PurchaseInvoiceForm()
        {
            InitializeComponent();
        }

        private void PurchaseInvoiceForm_Load(object sender, EventArgs e)
        {
            dataGridItems.Columns.Add("ProductName", "نام کالا");
            dataGridItems.Columns.Add("Quantity", "تعداد");
            dataGridItems.Columns.Add("UnitPrice", "قیمت واحد");
            dataGridItems.Columns.Add("TotalPrice", "جمع");

            txtSupplierName.Focus();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            string productName = txtProductName.Text.Trim();
            if (!int.TryParse(txtQuantity.Text.Trim(), out int quantity) ||
                !decimal.TryParse(txtUnitPrice.Text.Trim(), out decimal unitPrice))
            {
                MessageBox.Show("تعداد یا قیمت نادرست وارد شده.");
                return;
            }

            decimal total = quantity * unitPrice;
            dataGridItems.Rows.Add(productName, quantity, unitPrice, total);

            txtProductName.Clear();
            txtQuantity.Clear();
            txtUnitPrice.Clear();
            txtProductName.Focus();
        }

        private void btnRegisterInvoice_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSupplierName.Text))
            {
                MessageBox.Show("نام تأمین‌کننده را وارد کنید.");
                return;
            }

            if (dataGridItems.Rows.Count == 0)
            {
                MessageBox.Show("هیچ کالایی اضافه نشده.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // 1. ثبت فاکتور خرید
                SqlCommand insertInvoice = new SqlCommand(
                    "INSERT INTO PurchaseInvoices (SupplierName, PurchaseDate, RegisteredBy) OUTPUT INSERTED.Id VALUES (@name, @date, @user)", conn);
                insertInvoice.Parameters.AddWithValue("@name", txtSupplierName.Text.Trim());
                insertInvoice.Parameters.AddWithValue("@date", DateTime.Now);
                insertInvoice.Parameters.AddWithValue("@user", Global.LoggedInUsername ?? "Unknown");

                int invoiceId = (int)insertInvoice.ExecuteScalar();

                // 2. ثبت اقلام
                foreach (DataGridViewRow row in dataGridItems.Rows)
                {
                    if (row.IsNewRow) continue;

                    string pname = row.Cells["ProductName"].Value?.ToString();
                    int qty = Convert.ToInt32(row.Cells["Quantity"].Value);
                    decimal price = Convert.ToDecimal(row.Cells["UnitPrice"].Value);

                    SqlCommand insertItem = new SqlCommand(
                        "INSERT INTO PurchaseItems (InvoiceId, ProductName, Quantity, UnitPrice) VALUES (@invoiceId, @pname, @qty, @price)", conn);
                    insertItem.Parameters.AddWithValue("@invoiceId", invoiceId);
                    insertItem.Parameters.AddWithValue("@pname", pname);
                    insertItem.Parameters.AddWithValue("@qty", qty);
                    insertItem.Parameters.AddWithValue("@price", price);

                    insertItem.ExecuteNonQuery();
                }

                MessageBox.Show("فاکتور خرید با موفقیت ثبت شد.");
                dataGridItems.Rows.Clear();
                txtSupplierName.Clear();
            }
        }
    }
}
    
