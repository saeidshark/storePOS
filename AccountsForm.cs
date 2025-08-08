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
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;

namespace StorePOS
{
    public partial class AccountsForm : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

        public AccountsForm()
        {
            InitializeComponent();
            this.Load += AccountsForm_Load;
            dgvAccounts.CellClick += dgvAccounts_CellClick;
        }


        private void AccountsForm_Load(object sender, EventArgs e)
        {
            cmbChequeType.Items.Add("دریافتی");
            cmbChequeType.Items.Add("پرداختی");
            cmbChequeType.SelectedIndex = 0;

            LoadCustomers();
            LoadCheques();
        }

        private void LoadCustomers()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT Id, Name AS 'نام مشتری', PhoneNumber AS 'شماره تماس', Balance AS 'مانده حساب' FROM Customers", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvAccounts.DataSource = dt;
            }
        }

        private void LoadCheques()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(@"
                    SELECT c.Id, cu.Name AS 'مشتری', c.Amount AS 'مبلغ', c.DueDate AS 'تاریخ سررسید', c.Type AS 'نوع چک'
                    FROM Cheques c
                    JOIN Customers cu ON c.CustomerId = cu.Id
                    ORDER BY c.DueDate DESC", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvCheques.DataSource = dt;
            }
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            string name = txtCustomerName.Text.Trim();
            string phone = txtPhoneNumber.Text.Trim();
            string address = txtAddress.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("لطفاً نام مشتری را وارد کنید.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Customers (Name, PhoneNumber, Address) VALUES (@n, @p, @a)", conn);
                cmd.Parameters.AddWithValue("@n", name);
                cmd.Parameters.AddWithValue("@p", phone);
                cmd.Parameters.AddWithValue("@a", address);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("مشتری با موفقیت ثبت شد.");
            ClearCustomerFields();
            LoadCustomers();
        }

        private void ClearCustomerFields()
        {
            txtCustomerName.Clear();
            txtPhoneNumber.Clear();
            txtAddress.Clear();
        }

        private void btnAddCheque_Click(object sender, EventArgs e)
        {
            if (dgvAccounts.SelectedRows.Count == 0)
            {
                MessageBox.Show("لطفاً یک مشتری را از جدول انتخاب کنید.");
                return;
            }

            int customerId = Convert.ToInt32(dgvAccounts.SelectedRows[0].Cells["Id"].Value);
            if (!decimal.TryParse(txtChequeAmount.Text.Trim(), out decimal amount) || amount <= 0)
            {
                MessageBox.Show("مبلغ چک معتبر نیست.");
                return;
            }

            DateTime dueDate = dtChequeDate.Value.Date;
            string type = cmbChequeType.SelectedItem.ToString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    SqlCommand cmdCheque = new SqlCommand(@"
                        INSERT INTO Cheques (CustomerId, Amount, DueDate, Type)
                        VALUES (@cid, @amount, @date, @type)", conn, tran);
                    cmdCheque.Parameters.AddWithValue("@cid", customerId);
                    cmdCheque.Parameters.AddWithValue("@amount", amount);
                    cmdCheque.Parameters.AddWithValue("@date", dueDate);
                    cmdCheque.Parameters.AddWithValue("@type", type);
                    cmdCheque.ExecuteNonQuery();

                    SqlCommand cmdBalance = new SqlCommand(@"
                        UPDATE Customers
                        SET Balance = Balance + @delta
                        WHERE Id = @id", conn, tran);
                    decimal delta = (type == "دریافتی") ? amount : -amount;
                    cmdBalance.Parameters.AddWithValue("@delta", delta);
                    cmdBalance.Parameters.AddWithValue("@id", customerId);
                    cmdBalance.ExecuteNonQuery();

                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    MessageBox.Show("خطا در ثبت چک. عملیات انجام نشد.");
                    return;
                }
            }

            MessageBox.Show("چک با موفقیت ثبت شد.");
            txtChequeAmount.Clear();
            LoadCustomers();
            LoadCheques();
        }

        private void btnFilterCheques_Click(object sender, EventArgs e)
        {
            DateTime from = dtFromChequeFilter.Value.Date;
            DateTime to = dtToChequeFilter.Value.Date.AddDays(1).AddTicks(-1);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(@"
                    SELECT c.Id, cu.Name AS 'مشتری', c.Amount AS 'مبلغ', 
                           c.DueDate AS 'تاریخ سررسید', c.Type AS 'نوع چک'
                    FROM Cheques c
                    JOIN Customers cu ON c.CustomerId = cu.Id
                    WHERE c.DueDate BETWEEN @from AND @to
                    ORDER BY c.DueDate DESC", conn);
                da.SelectCommand.Parameters.AddWithValue("@from", from);
                da.SelectCommand.Parameters.AddWithValue("@to", to);

                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvCheques.DataSource = dt;
            }
        }

        private void btnPrintCheques_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "PDF Files|*.pdf",
                FileName = "ChequesReport.pdf"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Document doc = new Document(PageSize.A4.Rotate(), 25, 25, 30, 30);
                PdfWriter.GetInstance(doc, new FileStream(sfd.FileName, FileMode.Create));
                doc.Open();

                string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
                BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, 12);
                iTextSharp.text.Font bold = new iTextSharp.text.Font(baseFont, 14, iTextSharp.text.Font.BOLD);

                Paragraph title = new Paragraph("گزارش چک‌ها", bold)
                {
                    Alignment = Element.ALIGN_CENTER
                };
                doc.Add(title);
                doc.Add(new Paragraph("\n", font));

                PdfPTable table = new PdfPTable(dgvCheques.Columns.Count)
                {
                    RunDirection = PdfWriter.RUN_DIRECTION_RTL
                };

                foreach (DataGridViewColumn col in dgvCheques.Columns)
                    table.AddCell(new Phrase(col.HeaderText, bold));

                foreach (DataGridViewRow row in dgvCheques.Rows)
                {
                    if (row.IsNewRow) continue;
                    foreach (DataGridViewCell cell in row.Cells)
                        table.AddCell(new Phrase(cell.Value?.ToString(), font));
                }

                doc.Add(table);
                doc.Close();
                MessageBox.Show("گزارش چک‌ها با موفقیت ذخیره شد.");
            }
        }

       

        private void btnDeleteCheque_Click_1(object sender, EventArgs e)
        {
            if (dgvCheques.SelectedRows.Count == 0)
            {
                MessageBox.Show("لطفاً یک چک را انتخاب کنید.");
                return;
            }

            int chequeId = Convert.ToInt32(dgvCheques.SelectedRows[0].Cells["Id"].Value);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Cheques WHERE Id = @id", conn);
                cmd.Parameters.AddWithValue("@id", chequeId);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("چک حذف شد.");
            LoadCheques();
            LoadCustomers();
        }

        private void btnEditCheque_Click(object sender, EventArgs e)
        {
            if (dgvCheques.SelectedRows.Count == 0)
            {
                MessageBox.Show("لطفاً یک چک را انتخاب کنید.");
                return;
            }

            int chequeId = Convert.ToInt32(dgvCheques.SelectedRows[0].Cells["Id"].Value);
            string selectedCustomerName = dgvCheques.SelectedRows[0].Cells["مشتری"].Value.ToString();

            if (!decimal.TryParse(txtChequeAmount.Text.Trim(), out decimal newAmount) || newAmount <= 0)
            {
                MessageBox.Show("مبلغ جدید معتبر نیست.");
                return;
            }

            string newType = cmbChequeType.SelectedItem.ToString();
            DateTime newDueDate = dtChequeDate.Value.Date;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();

                try
                {
                    // 1. اطلاعات قبلی چک
                    SqlCommand cmdOld = new SqlCommand("SELECT Amount, Type, CustomerId FROM Cheques WHERE Id = @id", conn, tran);
                    cmdOld.Parameters.AddWithValue("@id", chequeId);

                    decimal oldAmount = 0;
                    string oldType = "";
                    int customerId = 0;

                    using (SqlDataReader reader = cmdOld.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            oldAmount = reader.GetDecimal(0);
                            oldType = reader.GetString(1);
                            customerId = reader.GetInt32(2);
                        }
                        else
                        {
                            reader.Close();
                            tran.Rollback();
                            MessageBox.Show("چک یافت نشد.");
                            return;
                        }
                    }

                    // 2. بروزرسانی چک
                    SqlCommand cmdUpdateCheque = new SqlCommand(@"
                UPDATE Cheques
                SET Amount = @amt, DueDate = @date, Type = @type
                WHERE Id = @id", conn, tran);
                    cmdUpdateCheque.Parameters.AddWithValue("@amt", newAmount);
                    cmdUpdateCheque.Parameters.AddWithValue("@date", newDueDate);
                    cmdUpdateCheque.Parameters.AddWithValue("@type", newType);
                    cmdUpdateCheque.Parameters.AddWithValue("@id", chequeId);
                    cmdUpdateCheque.ExecuteNonQuery();

                    // 3. محاسبه‌ی اختلاف مانده حساب
                    decimal oldDelta = (oldType == "دریافتی") ? oldAmount : -oldAmount;
                    decimal newDelta = (newType == "دریافتی") ? newAmount : -newAmount;
                    decimal diff = newDelta - oldDelta;

                    SqlCommand cmdUpdateBalance = new SqlCommand(@"
                UPDATE Customers
                SET Balance = Balance + @diff
                WHERE Id = @id", conn, tran);
                    cmdUpdateBalance.Parameters.AddWithValue("@diff", diff);
                    cmdUpdateBalance.Parameters.AddWithValue("@id", customerId);
                    cmdUpdateBalance.ExecuteNonQuery();

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    MessageBox.Show("خطا در ویرایش چک:\n" + ex.Message);
                    return;
                }
            }

            MessageBox.Show("چک با موفقیت ویرایش شد.");
            txtChequeAmount.Clear();
            LoadCheques();
            LoadCustomers();
        }

        private void dgvCheques_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvCheques.Rows[e.RowIndex];
                txtChequeAmount.Text = row.Cells["مبلغ"].Value?.ToString();
                cmbChequeType.SelectedItem = row.Cells["نوع چک"].Value?.ToString();
                dtChequeDate.Value = Convert.ToDateTime(row.Cells["تاریخ سررسید"].Value);
            }
        }

        private void dgvAccounts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvAccounts.Rows[e.RowIndex];
                txtCustomerName.Text = row.Cells["نام مشتری"].Value?.ToString();
                txtPhoneNumber.Text = row.Cells["شماره تماس"].Value?.ToString();

                // خواندن آدرس از دیتابیس (اگر ستون "آدرس" در جدول نمایش داده نمی‌شود)
                int customerId = Convert.ToInt32(row.Cells["Id"].Value);
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT Address FROM Customers WHERE Id = @id", conn);
                    cmd.Parameters.AddWithValue("@id", customerId);
                    var result = cmd.ExecuteScalar();
                    txtAddress.Text = result?.ToString() ?? "";
                }
            }
        }

        private void btnEditCustomer_Click(object sender, EventArgs e)
        {

            if (dgvAccounts.SelectedRows.Count == 0)
            {
                MessageBox.Show("لطفاً یک مشتری را از جدول انتخاب کنید.");
                return;
            }

            
            int customerId = Convert.ToInt32(dgvAccounts.SelectedRows[0].Cells["Id"].Value);
            string name = txtCustomerName.Text.Trim();
            string phone = txtPhoneNumber.Text.Trim();
            string address = txtAddress.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("نام مشتری نمی‌تواند خالی باشد.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"
              UPDATE Customers
              SET Name = @n, PhoneNumber = @p, Address = @a
              WHERE Id = @id", conn);
                cmd.Parameters.AddWithValue("@n", name);
                cmd.Parameters.AddWithValue("@p", phone);
                cmd.Parameters.AddWithValue("@a", address);
                cmd.Parameters.AddWithValue("@id", customerId);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("اطلاعات مشتری با موفقیت ویرایش شد.");
            LoadCustomers();
            ClearCustomerFields();
        }
    }
}
