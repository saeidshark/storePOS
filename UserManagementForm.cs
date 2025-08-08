using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace StorePOS
{
    public partial class UserManagementForm : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

        public UserManagementForm()
        {
            InitializeComponent();
        }

        private void UserManagementForm_Load(object sender, EventArgs e)
        {
            cmbRole.Items.Add("مدیر");
            cmbRole.Items.Add("صندوقدار");
            cmbRole.SelectedIndex = 0;

            LoadUsers();
        }

        private void LoadUsers()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, Username, Role FROM Users";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridUsers.DataSource = table;
            }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            string role = cmbRole.SelectedItem.ToString();

            if (username == "" || password == "")
            {
                MessageBox.Show("لطفاً تمام فیلدها را پر کنید.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Users (Username, Password, Role) VALUES (@u, @p, @r)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@u", username);
                cmd.Parameters.AddWithValue("@p", password);
                cmd.Parameters.AddWithValue("@r", role);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("کاربر با موفقیت اضافه شد.");
                    LoadUsers();
                    txtUsername.Clear();
                    txtPassword.Clear();
                    cmbRole.SelectedIndex = 0;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("خطا در افزودن کاربر: " + ex.Message);
                }
            }
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            if (dataGridUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("لطفاً یک کاربر را انتخاب کنید.");
                return;
            }

            int userId = Convert.ToInt32(dataGridUsers.SelectedRows[0].Cells["Id"].Value);
            string selectedUsername = dataGridUsers.SelectedRows[0].Cells["Username"].Value.ToString();

            if (selectedUsername == "admin")
            {
                MessageBox.Show("امکان حذف کاربر admin وجود ندارد.");
                return;
            }

            DialogResult result = MessageBox.Show("آیا مطمئن هستید که می‌خواهید این کاربر را حذف کنید؟", "حذف کاربر", MessageBoxButtons.YesNo);
            if (result == DialogResult.No) return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Users WHERE Id = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", userId);

                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("کاربر حذف شد.");
                LoadUsers();
            }
        }

        private void btnAddUser_Click_1(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            string role = cmbRole.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(role))
            {
                MessageBox.Show("لطفاً تمام فیلدها را پر کنید.");
                return;
            }

            var repo = new UserRepository();

            if (repo.UserExists(username))
            {
                MessageBox.Show("این نام کاربری قبلاً ثبت شده است.");
                return;
            }

            try
            {
                repo.AddUser(username, password, role);
                MessageBox.Show("کاربر با موفقیت اضافه شد.");
                txtUsername.Clear();
                txtPassword.Clear();
                cmbRole.SelectedIndex = 0;
                LoadUsers(); // بروزرسانی دیتاگرید
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطا در افزودن کاربر: " + ex.Message);
            }
        }

        private void btnDeleteUser_Click_1(object sender, EventArgs e)
        {
            if (dataGridUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("لطفاً یک کاربر را انتخاب کنید.");
                return;
            }

            string username = dataGridUsers.SelectedRows[0].Cells["Username"].Value.ToString();

            if (username == "admin")
            {
                MessageBox.Show("کاربر admin قابل حذف نیست.");
                return;
            }

            DialogResult result = MessageBox.Show("آیا مطمئن هستید که می‌خواهید این کاربر حذف شود؟", "تأیید حذف", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes)
                return;

            try
            {
                var repo = new UserRepository();
                repo.DeleteUser(username);
                MessageBox.Show("کاربر با موفقیت حذف شد.");
                LoadUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطا در حذف کاربر: " + ex.Message);
            }
        }
    }

}
