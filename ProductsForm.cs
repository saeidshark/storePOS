using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;

namespace StorePOS
{
    public partial class ProductsForm : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        private DataTable productTable;

        public ProductsForm()
        {
            InitializeComponent();
        }       

        private void ProductsForm_Load_1(object sender, EventArgs e)
        {
            dataGridProducts.AutoGenerateColumns = true;
            dataGridProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridProducts.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
            
            txtSearch.TextChanged += txtSearch_TextChanged;
            DataTable productTable = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT Barcode, Name, Category, Price FROM Products", conn);
                adapter.Fill(productTable);
            }

            dataGridProducts.DataSource = productTable;  

            LoadProducts();
        }
        private void LoadProducts()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, Barcode, Category, Name, Price FROM Products";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                productTable = new DataTable();
                adapter.Fill(productTable);
                dataGridProducts.DataSource = productTable;
            }
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBarcode.Text) || string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                MessageBox.Show("لطفاً تمام فیلدهای الزامی را پر کنید.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Products (Barcode, Category, Name, Price) VALUES (@b, @c, @n, @p)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@b", txtBarcode.Text.Trim());
                cmd.Parameters.AddWithValue("@c", txtCategory.Text.Trim());
                cmd.Parameters.AddWithValue("@n", txtName.Text.Trim());
                cmd.Parameters.AddWithValue("@p", Convert.ToDecimal(txtPrice.Text.Trim()));

                conn.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("کالا با موفقیت اضافه شد.");
                    ClearInputs();
                    LoadProducts();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("خطا در افزودن: " + ex.Message);
                }
            }
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            if (dataGridProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("لطفاً یک ردیف برای حذف انتخاب کنید.");
                return;
            }

            int productId = Convert.ToInt32(dataGridProducts.SelectedRows[0].Cells["Id"].Value);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Products WHERE Id = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", productId);
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("کالا حذف شد.");
                LoadProducts();
            }
        }

        private void btnSaveChanges_Click_1(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, Barcode, Category, Name, Price FROM Products";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

                conn.Open();
                adapter.Update(productTable);
                MessageBox.Show("تغییرات ذخیره شد.");
                LoadProducts();
            }
        }

        private void ClearInputs()
        {
            txtBarcode.Clear();
            txtCategory.Clear();
            txtName.Clear();
            txtPrice.Clear();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string filterColumn = "Name"; // پیش‌فرض

            if (rbBarcode.Checked)
                filterColumn = "Barcode";
            else if (rbCategory.Checked)
                filterColumn = "Category";

            (dataGridProducts.DataSource as DataTable).DefaultView.RowFilter =
                string.Format("[{0}] LIKE '%{1}%'", filterColumn, txtSearch.Text.Replace("'", "''"));
        }
    }
}
