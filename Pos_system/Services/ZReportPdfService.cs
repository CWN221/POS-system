using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using Pos_system.Models;

namespace Pos_system.Services
{
    /*
     * ZReportPdfService.cs
     * 
     * This service is responsible for generating a professional Z Report PDF.
     * The report includes:
     * - Store information (logo, name, address & contact)
     * - Report metadata (date & generation time)
     * - Sales summary (Total Sales, VAT, Transactions)
     * - Payment breakdown (Cash, M-Pesa, Card)
     * - System information (user)
     * 
     */

    public class ZReportPdfService
    {
        public void GeneratePdf(ZReport report, string filepath, StoreSettings store)
        {
            using var writer = new PdfWriter(filepath);
            using var pdf = new PdfDocument(writer);
            using var document = new Document(pdf);

            /*                  
             * Header (Logo + Store Info)
             * 
             */

            var headerTable = new Table(2).UseAllAvailableWidth();

            // Check if Logo exists
            if (!string.IsNullOrEmpty(store.LogoPath) && File.Exists(store.LogoPath))
            {
                var logo = new iText.Layout.Element.Image(ImageDataFactory.Create(store.LogoPath)).ScaleToFit(60, 60);

                headerTable.AddCell(new Cell().Add(logo).SetBorder(Border.NO_BORDER));
            }
            else
            {
                headerTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
            }

            // Store details (Name, Address, Contacts)
            var storeInfo = new Paragraph()
                .Add(store.Storename + "\n")
                .Add(store.Address + "\n")
                .Add(store.Contact + "\n")
                .SetTextAlignment(TextAlignment.RIGHT)
                .SetFontSize(10);

            headerTable.AddCell(new Cell().Add(storeInfo).SetBorder(Border.NO_BORDER));

            document.Add(new Div().Add(headerTable).SetBorder(new SolidBorder(1)).SetPadding(10));

            document.Add(new Paragraph("\n"));

            /*         
             * Title (Report Info)
             */

            var titleTable = new Table(2).UseAllAvailableWidth();

            titleTable.AddCell(new Cell().Add(new Paragraph("Z REPORT")).SetBorder(Border.NO_BORDER));

            titleTable.AddCell(new Cell()
                .Add(new Paragraph($"Date: {report.ReportDate:yyyy-MM-dd}\nGenerated: {DateTime.Now:dd-MM-yyyy HH:mm}"))
                .SetTextAlignment(TextAlignment.RIGHT)
                .SetBorder(Border.NO_BORDER));

            document.Add(new Div().Add(titleTable).SetBorder(new SolidBorder(1)).SetPadding(10));

            document.Add(new Paragraph("\n"));

            /*
             *
             * Sales summary
             * 
             */

            var salesTable = new Table(2).UseAllAvailableWidth();

            salesTable.AddHeaderCell(new Cell(1, 2)
                .Add(new Paragraph("SALES SUMMARY"))
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY));

            salesTable.AddCell("Total Sales");
            salesTable.AddCell(new Paragraph($"KES {report.TotalSales:N2}").SetTextAlignment(TextAlignment.RIGHT));

            salesTable.AddCell("Total VAT");
            salesTable.AddCell(new Paragraph($"KES {report.TotalVat:N2}").SetTextAlignment(TextAlignment.RIGHT));

            salesTable.AddCell("Transactions");
            salesTable.AddCell(new Paragraph(report.TotalTransactions.ToString()).SetTextAlignment(TextAlignment.RIGHT));

            document.Add(new Div().Add(salesTable).SetBorder(new SolidBorder(1)).SetPadding(10));

            document.Add(new Paragraph("\n"));

            /*
             * 
             * Payment
             * 
             * Shows how customers paid (Cash / M-Pesa / Card)
             * NOTE: You must supply these values from DB/service
             */

            var paymentTable = new Table(2).UseAllAvailableWidth();

            paymentTable.AddHeaderCell(new Cell(1, 2)
                .Add(new Paragraph("PAYMENT BREAKDOWN"))
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY));

            paymentTable.AddCell("Cash");
            paymentTable.AddCell(new Paragraph($"KES {report.CashTotal:N2}").SetTextAlignment(TextAlignment.RIGHT));

            paymentTable.AddCell("M-Pesa");
            paymentTable.AddCell(new Paragraph($"KES {report.MpesaTotal:N2}").SetTextAlignment(TextAlignment.RIGHT));

            paymentTable.AddCell("Card");
            paymentTable.AddCell(new Paragraph($"KES {report.CardTotal:N2}").SetTextAlignment(TextAlignment.RIGHT));

            document.Add(new Div().Add(paymentTable).SetBorder(new SolidBorder(1)).SetPadding(10));

            document.Add(new Paragraph("\n"));

            /*
             * 
             * Sys Info
             * 
             */

            var systemTable = new Table(2).UseAllAvailableWidth();

            systemTable.AddHeaderCell(new Cell(1, 2)
                .Add(new Paragraph("SYSTEM INFO"))
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY));
           
            systemTable.AddCell("User");
            systemTable.AddCell("Admin");

            document.Add(new Div()
                .Add(systemTable)
                .SetBorder(new SolidBorder(1))
                .SetPadding(10));

            document.Add(new Paragraph("\n"));

            /*
             * 
             * Footer
             * 
             */

            document.Add(new Paragraph("*** END OF REPORT ***").SetTextAlignment(TextAlignment.CENTER));
        }
    }
}