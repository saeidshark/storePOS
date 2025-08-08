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
    public partial class LoginForm: Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        
        private string targetForm;

        public LoginForm(string targetForm = "")
        {
            InitializeComponent();
            this.targetForm = targetForm;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            UserRepository repo = new UserRepository();

            if (repo.ValidateUser(username, password))
            {
                Session.LoggedInUsername = username;
                Session.LoggedInRole = repo.GetUserRole(username);
                Global.LoggedInUsername = username;

                MessageBox.Show($"ورود موفق! خوش آمدید {username} - نقش: {Session.LoggedInRole}");

                this.DialogResult = DialogResult.OK;
                this.Close(); // فقط فرم لاگین بسته میشه
            }
            else
            {
                MessageBox.Show("نام کاربری یا رمز عبور اشتباه است.");
            }
        }
    }
}
