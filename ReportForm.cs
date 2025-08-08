using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace StorePOS
{
    public partial class ReportForm : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

        public ReportForm()
        {
            InitializeComponent();
        }

        private void btnLoadReport_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = dateTimePicker1.Value.Date;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT Id, InvoiceDate, TotalAmount 
                                 FROM Invoices 
                                 WHERE CAST(InvoiceDate AS DATE) = @date 
                                 AND CashierUsername = @user";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@date", selectedDate);
                cmd.Parameters.AddWithValue("@user", Global.LoggedInUsername);

                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);

                dataGridView1.DataSource = dt;

                // جمع فروش:
                decimal sum = 0;
                foreach (DataRow row in dt.Rows)
                {
                    sum += Convert.ToDecimal(row["TotalAmount"]);
                }

                lblTotal.Text = $"جمع فروش امروز: {sum:0.00} تومان";
            }
        }
    }
}
