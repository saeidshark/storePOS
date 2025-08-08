//کنترلر های دیزاینر فرم با کدنویسی ساخته شده اند
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using System.Data.SqlClient;
//using System.Configuration;

//namespace StorePOS
//{
//    public partial class ProfitLossForm: Form
//    {
//        private string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

//        public ProfitLossForm()
//        {
//            InitializeComponent();
//            InitializeUI();
//        }

//        private void InitializeUI()
//        {
//            this.Text = "گزارش سود و زیان";

//            Label lblFrom = new Label() { Text = "از تاریخ:", Location = new System.Drawing.Point(20, 20) };
//            DateTimePicker dtFrom = new DateTimePicker() { Name = "dtFrom", Location = new System.Drawing.Point(90, 18), Width = 150 };
//            this.Controls.Add(lblFrom);
//            this.Controls.Add(dtFrom);

//            Label lblTo = new Label() { Text = "تا تاریخ:", Location = new System.Drawing.Point(260, 20) };
//            DateTimePicker dtTo = new DateTimePicker() { Name = "dtTo", Location = new System.Drawing.Point(330, 18), Width = 150 };
//            this.Controls.Add(lblTo);
//            this.Controls.Add(dtTo);

//            Button btnCalculate = new Button() { Text = "محاسبه", Location = new System.Drawing.Point(500, 16) };
//            btnCalculate.Click += BtnCalculate_Click_1;
//            this.Controls.Add(btnCalculate);

//            DataGridView dgv = new DataGridView()
//            {
//                Name = "dgvReport",
//                Location = new System.Drawing.Point(20, 60),
//                Size = new System.Drawing.Size(600, 250),
//                ReadOnly = true,
//                AllowUserToAddRows = false,
//                AllowUserToDeleteRows = false
//            };
//            this.Controls.Add(dgv);

//            Label lblProfit = new Label() { Name = "lblProfit", Location = new System.Drawing.Point(20, 330), AutoSize = true, Font = new System.Drawing.Font("Tahoma", 10, System.Drawing.FontStyle.Bold) };
//            this.Controls.Add(lblProfit);
//        }

//        private void BtnCalculate_Click_1(object sender, EventArgs e)
//        {
//            var dtFrom = (DateTimePicker)this.Controls.Find("dtFrom", true)[0];
//            var dtTo = (DateTimePicker)this.Controls.Find("dtTo", true)[0];
//            var dgv = (DataGridView)this.Controls.Find("dgvReport", true)[0];
//            var lblProfit = (Label)this.Controls.Find("lblProfit", true)[0];

//            DateTime from = dtFrom.Value.Date;
//            DateTime to = dtTo.Value.Date.AddDays(1).AddTicks(-1); // پایان روز

//            decimal totalSales = 0;
//            decimal totalPurchases = 0;

//            using (SqlConnection conn = new SqlConnection(connectionString))
//            {
//                conn.Open();

//                // مجموع فروش
//                SqlCommand salesCmd = new SqlCommand(@"
//                    SELECT SUM(ii.Quantity * ii.UnitPrice) 
//                    FROM InvoiceItems ii
//                    JOIN Invoices i ON ii.InvoiceId = i.Id
//                    WHERE i.InvoiceDate BETWEEN @from AND @to
//                ", conn);
//                salesCmd.Parameters.AddWithValue("@from", from);
//                salesCmd.Parameters.AddWithValue("@to", to);
//                var salesResult = salesCmd.ExecuteScalar();
//                totalSales = salesResult != DBNull.Value ? Convert.ToDecimal(salesResult) : 0;

//                // مجموع خرید
//                SqlCommand purchaseCmd = new SqlCommand(@"
//                    SELECT SUM(pi.Quantity * pi.UnitPrice) 
//                    FROM PurchaseItems pi
//                    JOIN PurchaseInvoices p ON pi.InvoiceId = p.Id
//                    WHERE p.PurchaseDate BETWEEN @from AND @to
//                ", conn);
//                purchaseCmd.Parameters.AddWithValue("@from", from);
//                purchaseCmd.Parameters.AddWithValue("@to", to);
//                var purchaseResult = purchaseCmd.ExecuteScalar();
//                totalPurchases = purchaseResult != DBNull.Value ? Convert.ToDecimal(purchaseResult) : 0;
//            }

//            decimal profit = totalSales - totalPurchases;

//            // نمایش
//            DataTable table = new DataTable();
//            table.Columns.Add("شرح");
//            table.Columns.Add("مبلغ");

//            table.Rows.Add("مجموع فروش", totalSales.ToString("N0"));
//            table.Rows.Add("مجموع خرید", totalPurchases.ToString("N0"));
//            table.Rows.Add("سود خالص", profit.ToString("N0"));

//            dgv.DataSource = table;
//            lblProfit.Text = $"سود خالص: {profit:N0} تومان";

//        }
//    }
//}


//--------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;
using ClosedXML.Excel;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;

namespace StorePOS
{
    public partial class ProfitLossForm : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

        public ProfitLossForm()
        {
            InitializeComponent();
            btnCalculate.Click += BtnCalculate_Click; // اتصال رویداد فقط
        }

        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            DateTime from = dtFrom.Value.Date;
            DateTime to = dtTo.Value.Date.AddDays(1).AddTicks(-1); // پایان روز

            decimal totalSales = 0;
            decimal totalPurchases = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // مجموع فروش
                SqlCommand salesCmd = new SqlCommand(@"
                    SELECT SUM(ii.Quantity * ii.UnitPrice) 
                    FROM InvoiceItems ii
                    JOIN Invoices i ON ii.InvoiceId = i.Id
                    WHERE i.InvoiceDate BETWEEN @from AND @to
                ", conn);
                salesCmd.Parameters.AddWithValue("@from", from);
                salesCmd.Parameters.AddWithValue("@to", to);
                var salesResult = salesCmd.ExecuteScalar();
                totalSales = salesResult != DBNull.Value ? Convert.ToDecimal(salesResult) : 0;

                // مجموع خرید
                SqlCommand purchaseCmd = new SqlCommand(@"
                    SELECT SUM(pi.Quantity * pi.UnitPrice) 
                    FROM PurchaseItems pi
                    JOIN PurchaseInvoices p ON pi.InvoiceId = p.Id
                    WHERE p.PurchaseDate BETWEEN @from AND @to
                ", conn);
                purchaseCmd.Parameters.AddWithValue("@from", from);
                purchaseCmd.Parameters.AddWithValue("@to", to);
                var purchaseResult = purchaseCmd.ExecuteScalar();
                totalPurchases = purchaseResult != DBNull.Value ? Convert.ToDecimal(purchaseResult) : 0;
            }

            decimal profit = totalSales - totalPurchases;

            // نمایش در گرید
            DataTable table = new DataTable();
            table.Columns.Add("شرح");
            table.Columns.Add("مبلغ");

            table.Rows.Add("مجموع فروش", totalSales.ToString("N0"));
            table.Rows.Add("مجموع خرید", totalPurchases.ToString("N0"));
            table.Rows.Add("سود خالص", profit.ToString("N0"));

            dgvReport.DataSource = table;

            // نمایش سود
            lblProfit.Text = $"سود خالص: {profit:N0} تومان";
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files|*.xlsx";
            sfd.FileName = "ProfitReport.xlsx";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    DataTable dt = dgvReport.DataSource as DataTable;
                    wb.Worksheets.Add(dt, "گزارش سود");

                    wb.SaveAs(sfd.FileName);
                    MessageBox.Show("فایل Excel با موفقیت ذخیره شد.");
                }
            }
        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PDF Files|*.pdf";
            sfd.FileName = "ProfitReport.pdf";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Document doc = new Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter.GetInstance(doc, new FileStream(sfd.FileName, FileMode.Create));
                doc.Open();

                // فونت فارسی
                string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
                BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                Font font = new Font(baseFont, 12, iTextSharp.text.Font.NORMAL);
                Font boldFont = new Font(baseFont, 14, iTextSharp.text.Font.BOLD);

                // عنوان گزارش
                Paragraph title = new Paragraph("گزارش سود و زیان", boldFont);
                title.Alignment = Element.ALIGN_CENTER;
                doc.Add(title);
                doc.Add(new Paragraph("\n", font));

                // جدول
                PdfPTable table = new PdfPTable(2);
                table.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                table.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;

                table.AddCell(new Phrase("شرح", boldFont));
                table.AddCell(new Phrase("مبلغ", boldFont));

                foreach (DataGridViewRow row in dgvReport.Rows)
                {
                    if (row.IsNewRow) continue;
                    table.AddCell(new Phrase(row.Cells[0].Value?.ToString(), font));
                    table.AddCell(new Phrase(row.Cells[1].Value?.ToString(), font));
                }

                doc.Add(table);
                doc.Add(new Paragraph("\nتاریخ گزارش: " + DateTime.Now.ToString("yyyy/MM/dd"), font));
                doc.Close();

                MessageBox.Show("فایل PDF با موفقیت ذخیره شد.");
            }
        }
    }
}



