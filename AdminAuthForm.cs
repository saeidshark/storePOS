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
    public partial class AdminAuthForm : Form
    {
        public bool IsAuthenticated { get; private set; } = false;

        public AdminAuthForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            var repo = new UserRepository();
            if (repo.ValidateUser(username, password) && repo.GetUserRole(username) == "مدیر")
            {
                IsAuthenticated = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("اطلاعات نادرست است یا دسترسی ندارید.");
            }
        }
    }
}
