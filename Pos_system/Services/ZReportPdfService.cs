using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using Pos_system.Core;
using Pos_system.Models;
using AppContext = Pos_system.Core.AppContext;

namespace Pos_system.Services
{
    public class ZReportPdfService
    {
        public void GeneratePdf(ZReport report, string filepath, StoreSettings store)
        {
            if (File.Exists(filepath))
            {
                try
                {
                    File.Delete(filepath);
                }
                catch (IOException)
                {
                    throw new IOException($"Cannot write to \"{filepath}\" because it's open in another program. Close any PDF viewers and try again.");
                }
            }

            PdfWriter? writer = null;
            PdfDocument? pdf = null;
            Document? document = null;

            try
            {
                writer = new PdfWriter(filepath);
                pdf = new PdfDocument(writer);
                document = new Document(pdf);

                document.SetMargins(36, 36, 36, 36);

                var BoldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
                var RegularFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

                var headerTable = new Table(2).UseAllAvailableWidth();

                if (store != null && !string.IsNullOrWhiteSpace(store.LogoPath) && File.Exists(store.LogoPath))
                {
                    var logo = new iText.Layout.Element.Image(ImageDataFactory.Create(store.LogoPath)).ScaleToFit(70, 70);
                    headerTable.AddCell(new Cell().Add(logo).SetBorder(Border.NO_BORDER).SetVerticalAlignment(VerticalAlignment.MIDDLE));
                }
                else
                {
                    headerTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
                }

                var storeInfo = new Paragraph().SetTextAlignment(TextAlignment.RIGHT).SetFont(RegularFont).SetFontSize(10);

                storeInfo.Add(new Text(store?.Storename ?? "Store").SetFont(BoldFont).SetFontSize(14));
                storeInfo.Add("\n");
                storeInfo.Add(new Text(store?.Address ?? string.Empty).SetFont(RegularFont).SetFontSize(10));
                storeInfo.Add("\n");
                storeInfo.Add(new Text(FormatContactLine(store)).SetFont(RegularFont).SetFontSize(10));
                storeInfo.Add("\n");
                storeInfo.Add(new Text(store?.Email ?? string.Empty).SetFont(RegularFont).SetFontSize(10));
                storeInfo.Add("\n");
                storeInfo.Add(new Text(string.IsNullOrWhiteSpace(store?.BusinessPin) ? "" : $"KRA PIN: {store!.BusinessPin}").SetFont(RegularFont).SetFontSize(10));

                headerTable.AddCell(new Cell().Add(storeInfo).SetBorder(Border.NO_BORDER).SetVerticalAlignment(VerticalAlignment.MIDDLE));

                document.Add(headerTable);
                document.Add(new Paragraph("\n"));

                var reportId = $"ZR-{report.ReportDate:yyyy}-{report.Id:D4}";
                var branchName = AppContext.CurrentBranch?.BranchName ?? "—";
                var userName = SessionManager.CurrentUser?.Username ?? "—";

                var titleTable = new Table(2).UseAllAvailableWidth();

                var leftTitleBlock = new Paragraph().SetFont(BoldFont).SetFontSize(10);

                leftTitleBlock.Add(new Text("Z REPORT").SetFont(BoldFont).SetFontSize(16));
                leftTitleBlock.Add("\n");
                leftTitleBlock.Add(new Text($"Report ID: {reportId}").SetFont(BoldFont).SetFontSize(10));

                titleTable.AddCell(new Cell().Add(leftTitleBlock).SetBorder(Border.NO_BORDER).SetVerticalAlignment(VerticalAlignment.MIDDLE));

                var rightTitleBlock = new Paragraph().SetFont(RegularFont).SetFontSize(10).SetTextAlignment(TextAlignment.RIGHT);

                rightTitleBlock.Add(new Text($"Date: {report.ReportDate:dd MMM yyyy}").SetFont(RegularFont).SetFontSize(10));
                rightTitleBlock.Add("\n");
                rightTitleBlock.Add(new Text($"Branch: {branchName}").SetFont(RegularFont).SetFontSize(10));
                rightTitleBlock.Add("\n");
                rightTitleBlock.Add(new Text($"Generated: {report.GeneratedAt:dd MMM yyyy HH:mm}").SetFont(RegularFont).SetFontSize(10));

                titleTable.AddCell(new Cell()
                    .Add(rightTitleBlock)
                    .SetBorder(Border.NO_BORDER)
                    .SetVerticalAlignment(VerticalAlignment.MIDDLE));

                document.Add(new Div().Add(titleTable)
                    .SetBorder(new SolidBorder(ColorConstants.LIGHT_GRAY, 1))
                    .SetPadding(12)
                    .SetBackgroundColor(new DeviceRgb(248, 250, 252)));

                document.Add(new Paragraph("\n"));

                var salesTable = new Table(2).UseAllAvailableWidth();

                var salesHeader = new Paragraph("SALES SUMMARY").SetFont(BoldFont).SetFontSize(11);

                salesTable.AddHeaderCell(new Cell(1, 2)
                    .Add(salesHeader)
                    .SetBackgroundColor(new DeviceRgb(226, 232, 240))
                    .SetPadding(6));

                AddRow(salesTable, "Gross Sales (incl. VAT)", $"KES {report.GrossSales:N2}", false, BoldFont, RegularFont);
                AddRow(salesTable, "Refunds", $"-KES {report.RefundAmount:N2}", false, BoldFont, RegularFont);
                AddRow(salesTable, "Net Sales", $"KES {report.NetSales:N2}", true, BoldFont, RegularFont);
                AddRow(salesTable, "VAT Collected", $"KES {report.TotalVat:N2}", false, BoldFont, RegularFont);
                AddRow(salesTable, "Transactions", report.TotalTransactions.ToString(), false, BoldFont, RegularFont);

                document.Add(salesTable);
                document.Add(new Paragraph("\n"));

                var paymentTable = new Table(2).UseAllAvailableWidth();

                var paymentHeader = new Paragraph("PAYMENT BREAKDOWN").SetFont(BoldFont).SetFontSize(11);

                paymentTable.AddHeaderCell(new Cell(1, 2)
                    .Add(paymentHeader)
                    .SetBackgroundColor(new DeviceRgb(226, 232, 240))
                    .SetPadding(6));

                AddRow(paymentTable, "Cash", $"KES {report.CashTotal:N2}", false, BoldFont, RegularFont);
                AddRow(paymentTable, "M-Pesa", $"KES {report.MpesaTotal:N2}", false, BoldFont, RegularFont);
                AddRow(paymentTable, "Card", $"KES {report.CardTotal:N2}", false, BoldFont, RegularFont);

                var paymentTotal = report.CashTotal + report.MpesaTotal + report.CardTotal;
                AddRow(paymentTable, "Total", $"KES {paymentTotal:N2}", true, BoldFont, RegularFont);

                document.Add(paymentTable);
                document.Add(new Paragraph("\n"));

                var systemTable = new Table(2).UseAllAvailableWidth();

                var systemHeader = new Paragraph("SYSTEM INFO").SetFont(BoldFont).SetFontSize(11);

                systemTable.AddHeaderCell(new Cell(1, 2)
                    .Add(systemHeader)
                    .SetBackgroundColor(new DeviceRgb(226, 232, 240))
                    .SetPadding(6));

                AddRow(systemTable, "Generated By", userName, false, BoldFont, RegularFont);
                AddRow(systemTable, "Branch", branchName, false, BoldFont, RegularFont);
                AddRow(systemTable, "Generation Time", report.GeneratedAt.ToString("dd MMM yyyy HH:mm:ss"), false, BoldFont, RegularFont);

                document.Add(systemTable);
                document.Add(new Paragraph("\n"));

                var signatureTable = new Table(2).UseAllAvailableWidth();

                var preparedBy = new Paragraph()
                    .SetFont(RegularFont)
                    .SetFontSize(10);
                preparedBy.Add(new Text("Prepared By:").SetFont(RegularFont).SetFontSize(10));
                preparedBy.Add("\n\n");
                preparedBy.Add(new Text("________________________").SetFont(RegularFont).SetFontSize(10));

                var approvedBy = new Paragraph()
                    .SetFont(RegularFont)
                    .SetFontSize(10);
                approvedBy.Add(new Text("Approved By:").SetFont(RegularFont).SetFontSize(10));
                approvedBy.Add("\n\n");
                approvedBy.Add(new Text("________________________").SetFont(RegularFont).SetFontSize(10));

                signatureTable.AddCell(new Cell()
                    .Add(preparedBy)
                    .SetBorder(Border.NO_BORDER)
                    .SetPadding(6));

                signatureTable.AddCell(new Cell()
                    .Add(approvedBy)
                    .SetBorder(Border.NO_BORDER)
                    .SetPadding(6));

                document.Add(signatureTable);
                document.Add(new Paragraph("\n"));

                document.Add(new Paragraph("*** END OF REPORT ***")
                     .SetTextAlignment(TextAlignment.CENTER)
                     .SetFont(RegularFont)
                     .SetFontSize(10)
                     .SetFontColor(ColorConstants.GRAY));
            }
            finally
            {
                document?.Close();
                pdf?.Close();
                writer?.Close();
            }
        }

        public async Task GenerateSalesReportPdfAsync(
            string filepath,
            List<SalesReport> data,
            StoreSettings? store,
            DateTime from,
            DateTime to)
        {
            if (data == null || data.Count == 0)
                throw new InvalidOperationException("There is no data to export.");

            if (File.Exists(filepath))
            {
                try
                {
                    File.Delete(filepath);
                }
                catch (IOException)
                {
                    throw new IOException($"Cannot write to \"{filepath}\" because it's open in another program. Close any PDF viewers and try again.");
                }
            }

            await Task.Run(() =>
            {
                PdfWriter? writer = null;
                PdfDocument? pdf = null;
                Document? document = null;

                try
                {
                    writer = new PdfWriter(filepath);
                    pdf = new PdfDocument(writer);
                    document = new Document(pdf);

                    document.SetMargins(36, 36, 36, 36);

                    var BoldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
                    var RegularFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

                    var headerTable = new Table(2).UseAllAvailableWidth();

                    if (store != null && !string.IsNullOrWhiteSpace(store.LogoPath) && File.Exists(store.LogoPath))
                    {
                        var logo = new iText.Layout.Element.Image(ImageDataFactory.Create(store.LogoPath)).ScaleToFit(70, 70);
                        headerTable.AddCell(new Cell().Add(logo).SetBorder(Border.NO_BORDER).SetVerticalAlignment(VerticalAlignment.MIDDLE));
                    }
                    else
                    {
                        headerTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
                    }

                    var storeInfo = new Paragraph().SetTextAlignment(TextAlignment.RIGHT).SetFont(RegularFont).SetFontSize(10);

                    storeInfo.Add(new Text(store?.Storename ?? "Store").SetFont(BoldFont).SetFontSize(14));
                    storeInfo.Add("\n");
                    storeInfo.Add(new Text(store?.Address ?? string.Empty).SetFont(RegularFont).SetFontSize(10));
                    storeInfo.Add("\n");
                    storeInfo.Add(new Text(FormatContactLine(store)).SetFont(RegularFont).SetFontSize(10));
                    storeInfo.Add("\n");
                    storeInfo.Add(new Text(store?.Email ?? string.Empty).SetFont(RegularFont).SetFontSize(10));
                    storeInfo.Add("\n");
                    storeInfo.Add(new Text(string.IsNullOrWhiteSpace(store?.BusinessPin) ? "" : $"KRA PIN: {store!.BusinessPin}").SetFont(RegularFont).SetFontSize(10));

                    headerTable.AddCell(new Cell().Add(storeInfo).SetBorder(Border.NO_BORDER).SetVerticalAlignment(VerticalAlignment.MIDDLE));

                    document.Add(headerTable);
                    document.Add(new Paragraph("\n"));

                    document.Add(new Paragraph("Sales Report")
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFont(BoldFont)
                        .SetFontSize(16));

                    document.Add(new Paragraph($"Period: {from:dd MMM yyyy} to {to:dd MMM yyyy}")
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFont(RegularFont)
                        .SetFontSize(10));

                    document.Add(new Paragraph($"Generated: {DateTime.Now:dd MMM yyyy HH:mm}")
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFont(RegularFont)
                        .SetFontSize(10));

                    document.Add(new Paragraph("\n"));

                    var table = new Table(5).UseAllAvailableWidth();

                    table.AddHeaderCell(new Cell().Add(new Paragraph("Date").SetFont(BoldFont).SetFontSize(10))
                        .SetBackgroundColor(new DeviceRgb(226, 232, 240)).SetPadding(6));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Sales (KES)").SetFont(BoldFont).SetFontSize(10))
                        .SetBackgroundColor(new DeviceRgb(226, 232, 240)).SetPadding(6));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Profit (KES)").SetFont(BoldFont).SetFontSize(10))
                        .SetBackgroundColor(new DeviceRgb(226, 232, 240)).SetPadding(6));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("VAT (KES)").SetFont(BoldFont).SetFontSize(10))
                        .SetBackgroundColor(new DeviceRgb(226, 232, 240)).SetPadding(6));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Txns").SetFont(BoldFont).SetFontSize(10))
                        .SetBackgroundColor(new DeviceRgb(226, 232, 240)).SetPadding(6));

                    foreach (var row in data)
                    {
                        table.AddCell(new Cell().Add(new Paragraph(row.Date.ToString("dd MMM yyyy")).SetFont(RegularFont).SetFontSize(10)).SetPadding(5));
                        table.AddCell(new Cell().Add(new Paragraph(row.TotalSales.ToString("N2")).SetFont(RegularFont).SetFontSize(10).SetTextAlignment(TextAlignment.RIGHT)).SetPadding(5));
                        table.AddCell(new Cell().Add(new Paragraph(row.TotalProfit.ToString("N2")).SetFont(RegularFont).SetFontSize(10).SetTextAlignment(TextAlignment.RIGHT)).SetPadding(5));
                        table.AddCell(new Cell().Add(new Paragraph(row.TotalVat.ToString("N2")).SetFont(RegularFont).SetFontSize(10).SetTextAlignment(TextAlignment.RIGHT)).SetPadding(5));
                        table.AddCell(new Cell().Add(new Paragraph(row.TotalTransactions.ToString()).SetFont(RegularFont).SetFontSize(10).SetTextAlignment(TextAlignment.RIGHT)).SetPadding(5));
                    }

                    var totalSales = data.Sum(x => x.TotalSales);
                    var totalProfit = data.Sum(x => x.TotalProfit);
                    var totalVat = data.Sum(x => x.TotalVat);
                    var totalTxns = data.Sum(x => x.TotalTransactions);

                    table.AddCell(new Cell().Add(new Paragraph("TOTAL").SetFont(BoldFont).SetFontSize(10))
                        .SetBackgroundColor(new DeviceRgb(248, 250, 252)).SetPadding(6));
                    table.AddCell(new Cell().Add(new Paragraph(totalSales.ToString("N2")).SetFont(BoldFont).SetFontSize(10).SetTextAlignment(TextAlignment.RIGHT))
                        .SetBackgroundColor(new DeviceRgb(248, 250, 252)).SetPadding(6));
                    table.AddCell(new Cell().Add(new Paragraph(totalProfit.ToString("N2")).SetFont(BoldFont).SetFontSize(10).SetTextAlignment(TextAlignment.RIGHT))
                        .SetBackgroundColor(new DeviceRgb(248, 250, 252)).SetPadding(6));
                    table.AddCell(new Cell().Add(new Paragraph(totalVat.ToString("N2")).SetFont(BoldFont).SetFontSize(10).SetTextAlignment(TextAlignment.RIGHT))
                        .SetBackgroundColor(new DeviceRgb(248, 250, 252)).SetPadding(6));
                    table.AddCell(new Cell().Add(new Paragraph(totalTxns.ToString()).SetFont(BoldFont).SetFontSize(10).SetTextAlignment(TextAlignment.RIGHT))
                        .SetBackgroundColor(new DeviceRgb(248, 250, 252)).SetPadding(6));

                    document.Add(table);

                    document.Add(new Paragraph("\n"));
                    document.Add(new Paragraph("*** END OF REPORT ***")
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFont(RegularFont)
                        .SetFontSize(10)
                        .SetFontColor(ColorConstants.GRAY));
                }
                finally
                {
                    document?.Close();
                    pdf?.Close();
                    writer?.Close();
                }
            });
        }

        private static string FormatContactLine(StoreSettings? store)
        {
            if (store == null) return string.Empty;
            var parts = new List<string>();
            if (!string.IsNullOrWhiteSpace(store.Contact)) parts.Add(store.Contact);
            if (!string.IsNullOrWhiteSpace(store.Contact2)) parts.Add(store.Contact2);
            return string.Join(" | ", parts);
        }

        private static void AddRow(Table table, string label, string value, bool bold, PdfFont boldFont, PdfFont regularFont)
        {
            var labelPara = new Paragraph(label).SetFont(bold ? boldFont : regularFont).SetFontSize(10);

            var valuePara = new Paragraph(value)
                .SetFont(bold ? boldFont : regularFont)
                .SetFontSize(10)
                .SetTextAlignment(TextAlignment.RIGHT);

            table.AddCell(new Cell().Add(labelPara).SetPadding(6));
            table.AddCell(new Cell().Add(valuePara).SetPadding(6));
        }
    }
}