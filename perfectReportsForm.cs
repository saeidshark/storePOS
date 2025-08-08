using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StorePOS
{
    public partial class perfectReportsForm: Form
    {
        public perfectReportsForm()
        {
            InitializeComponent();
            statusTimer.Tick += statusTimer_Tick;
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                lblStatus.Text = "در حال تولید گزارش...";
                Application.DoEvents(); // نمایش سریع‌تر وضعیت

                // عملیات گزارش‌گیری...

                lblStatus.Text = "✅ گزارش با موفقیت ذخیره شد.";
            }
            catch (Exception ex)
            {
                lblStatus.Text = "❌ خطا در تولید گزارش: " + ex.Message;
            }


            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // 1. داده‌های فروش
                SqlDataAdapter daSales = new SqlDataAdapter(@"
        SELECT i.Id AS InvoiceID, i.InvoiceDate, i.CashierUsername, 
               ii.ProductName, ii.Quantity, ii.UnitPrice, ii.TotalPrice
        FROM Invoices i
        JOIN InvoiceItems ii ON i.Id = ii.InvoiceId
    ", conn);
                DataTable salesTable = new DataTable();
                daSales.Fill(salesTable);

                // 2. داده‌های خرید
                SqlDataAdapter daPurchases = new SqlDataAdapter(@"
        SELECT pi.Id AS PurchaseID, p.PurchaseDate, p.SupplierName, 
               pi.ProductName, pi.Quantity, pi.UnitPrice, pi.TotalPrice
        FROM PurchaseInvoices p
        JOIN PurchaseItems pi ON p.Id = pi.InvoiceId
    ", conn);
                DataTable purchaseTable = new DataTable();
                daPurchases.Fill(purchaseTable);

                // 3. موجودی کالا
                SqlDataAdapter daProducts = new SqlDataAdapter("SELECT * FROM Products", conn);
                DataTable productsTable = new DataTable();
                daProducts.Fill(productsTable);

                // 4. محاسبه سود و زیان کلی
                decimal totalSales = salesTable.AsEnumerable().Sum(r => r.Field<decimal>("TotalPrice"));
                decimal totalPurchases = purchaseTable.AsEnumerable().Sum(r => r.Field<decimal>("TotalPrice"));
                decimal profit = totalSales - totalPurchases;

                // ساخت فایل اکسل
                SaveFileDialog sfd = new SaveFileDialog { Filter = "Excel Workbook|*.xlsx", FileName = "گزارش مالی فروشگاه.xlsx" };
                if (sfd.ShowDialog() != DialogResult.OK) return;

                using (var workbook = new XLWorkbook())
                {
                    workbook.Worksheets.Add(salesTable, "گزارش فروش");
                    workbook.Worksheets.Add(purchaseTable, "گزارش خرید");
                    workbook.Worksheets.Add(productsTable, "موجودی کالا");

                    var summarySheet = workbook.Worksheets.Add("سود و زیان");
                    summarySheet.Cell("A1").Value = "جمع فروش";
                    summarySheet.Cell("B1").Value = totalSales;
                    summarySheet.Cell("A2").Value = "جمع خرید";
                    summarySheet.Cell("B2").Value = totalPurchases;
                    summarySheet.Cell("A3").Value = "سود یا زیان";
                    summarySheet.Cell("B3").Value = profit;

                    workbook.SaveAs(sfd.FileName);
                }

                MessageBox.Show("فایل گزارش با موفقیت ذخیره شد.");
            }
        }
        private void ShowStatusMessage(string message, Color color)
        {
            lblStatus.Text = message;
            lblStatus.ForeColor = color;

            // شروع تایمر برای پاک کردن پیام بعد از مدت زمان مشخص
            statusTimer.Stop();
            statusTimer.Start();
        }

        private void statusTimer_Tick(object sender, EventArgs e)
        {
            lblStatus.Text = "";
            statusTimer.Stop();
        }
    }
}
