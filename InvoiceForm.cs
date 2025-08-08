using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;
using System.IO.Ports;
using System.Drawing.Printing;
using System.Drawing;

namespace StorePOS
{
    public partial class InvoiceForm : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        private decimal totalAmount = 0;

        public InvoiceForm()
        {
            InitializeComponent();
        }

        private void AddProductByBarcode(string barcode)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Name, Price FROM Products WHERE Barcode = @b";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@b", barcode);

                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string name = reader["Name"].ToString();
                    decimal price = Convert.ToDecimal(reader["Price"]);

                    bool found = false;
                    foreach (DataGridViewRow row in dataGridItems.Rows)
                    {
                        if (row.Cells["Barcode"].Value?.ToString() == barcode)
                        {
                            int qty = Convert.ToInt32(row.Cells["Quantity"].Value) + 1;
                            row.Cells["Quantity"].Value = qty;
                            row.Cells["Total"].Value = qty * price;
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        dataGridItems.Rows.Add(barcode, name, price, 1, price);
                    }

                    totalAmount += price;
                    UpdateTotalLabel();
                }
                else
                {
                    MessageBox.Show("کالایی با این بارکد یافت نشد.");
                }
            }
        }

        private void UpdateTotalLabel()
        {
            lblTotalAmount.Text = $"جمع کل: {totalAmount:0.00} تومان";
        }

        private void Pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font font = new Font("Tahoma", 10);
            float y = 20;
            e.Graphics.DrawString("فاکتور فروش", new Font("Tahoma", 14, FontStyle.Bold), Brushes.Black, 200, y);
            y += 30;

            e.Graphics.DrawString($"تاریخ: {DateTime.Now:yyyy/MM/dd}", font, Brushes.Black, 50, y);
            e.Graphics.DrawString($"صندوقدار: {Global.LoggedInUsername}", font, Brushes.Black, 300, y);
            y += 30;

            e.Graphics.DrawString("-------------------------------------------------------------", font, Brushes.Black, 50, y);
            y += 20;

            foreach (DataGridViewRow row in dataGridItems.Rows)
            {
                if (row.IsNewRow) continue;
                string name = row.Cells["ProductName"].Value.ToString();
                string qty = row.Cells["Quantity"].Value.ToString();
                string total = row.Cells["Total"].Value.ToString();
                e.Graphics.DrawString($"{name} - تعداد: {qty} - مبلغ: {total}", font, Brushes.Black, 50, y);
                y += 20;
            }

            y += 20;
            e.Graphics.DrawString("-------------------------------------------------------------", font, Brushes.Black, 50, y);
            y += 20;
            e.Graphics.DrawString($"جمع کل: {totalAmount:0.00} تومان", new Font("Tahoma", 12, FontStyle.Bold), Brushes.Black, 200, y);
        }

        private void InvoiceForm_Load_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Global.LoggedInUsername))
            {
                MessageBox.Show("خطا: نام صندوقدار پیدا نشد. لطفاً دوباره وارد شوید.");
                this.Close();
                return;
            }

            lblCashierName.Text = $"صندوقدار: {Global.LoggedInUsername}";
            lblDate.Text = $"تاریخ: {DateTime.Now:yyyy/MM/dd}";
            txtBarcode.Focus();

            // تنظیم اولیه DataGridView
            dataGridItems.Columns.Add("Barcode", "بارکد");
            dataGridItems.Columns.Add("ProductName", "نام کالا");
            dataGridItems.Columns.Add("UnitPrice", "قیمت واحد");
            dataGridItems.Columns.Add("Quantity", "تعداد");
            dataGridItems.Columns.Add("Total", "جمع");
        }

        private void txtBarcode_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string barcode = txtBarcode.Text.Trim();
                AddProductByBarcode(barcode);
                txtBarcode.Clear();
            }
        }

        private void btnRegisterInvoice_Click_1(object sender, EventArgs e)
        {
            if (dataGridItems.Rows.Count == 0)
            {
                MessageBox.Show("هیچ کالایی در فاکتور وجود ندارد.");
                return;
            }

            int invoiceId;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand insertInvoice = new SqlCommand("INSERT INTO Invoices (CashierUsername, InvoiceDate, TotalAmount) OUTPUT INSERTED.Id VALUES (@user, @date, @total)", conn);
                insertInvoice.Parameters.AddWithValue("@user", Global.LoggedInUsername);
                insertInvoice.Parameters.AddWithValue("@date", DateTime.Now);
                insertInvoice.Parameters.AddWithValue("@total", totalAmount);
                invoiceId = (int)insertInvoice.ExecuteScalar();

                foreach (DataGridViewRow row in dataGridItems.Rows)
                {
                    if (row.IsNewRow) continue;

                    var barcode = row.Cells["Barcode"].Value?.ToString();
                    var name = row.Cells["ProductName"].Value?.ToString();
                    var qty = row.Cells["Quantity"].Value;
                    var price = row.Cells["UnitPrice"].Value;

                    if (string.IsNullOrWhiteSpace(barcode) || string.IsNullOrWhiteSpace(name) || qty == null || price == null)
                        continue;

                    SqlCommand insertItem = new SqlCommand("INSERT INTO InvoiceItems (InvoiceId, ProductCode, ProductName, Quantity, UnitPrice) VALUES (@invoiceId, @code, @name, @qty, @price)", conn);
                    insertItem.Parameters.AddWithValue("@invoiceId", invoiceId);
                    insertItem.Parameters.AddWithValue("@code", barcode);
                    insertItem.Parameters.AddWithValue("@name", name);
                    insertItem.Parameters.AddWithValue("@qty", qty);
                    insertItem.Parameters.AddWithValue("@price", price);
                    insertItem.ExecuteNonQuery();
                }
            }

            // 1. ارسال مبلغ به کارتخوان
            try
            {
                string portName = "COM3";
                using (SerialPort port = new SerialPort(portName, 9600, Parity.None, 8, StopBits.One))
                {
                    port.Open();
                    string message = totalAmount.ToString("0.00");
                    port.WriteLine(message);
                    port.Close();
                }

                // 2. نمایش پیام برای کشیدن کارت
                MessageBox.Show("مبلغ به دستگاه کارت‌خوان ارسال شد.\nلطفاً کارت مشتری را بکشید و پس از پایان تراکنش، روی OK کلیک کنید.", "در انتظار پرداخت", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 3. چاپ فاکتور بعد از تأیید
                PrintDocument pd = new PrintDocument();
                pd.PrintPage += Pd_PrintPage;
                PrintPreviewDialog preview = new PrintPreviewDialog
                {
                    Document = pd
                };
                //pd.Print(); // چاپ مستقیم
                preview.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطا در ارتباط با کارت‌خوان: " + ex.Message);
            }

            // 4. پاکسازی فرم
            dataGridItems.Rows.Clear();
            totalAmount = 0;
            UpdateTotalLabel();
            MessageBox.Show("فاکتور با موفقیت ثبت و چاپ شد.");
        }





    }
}
