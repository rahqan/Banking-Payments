using Banking_Payments.Models.DTO.Reports;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.ComponentModel;

namespace Banking_Payments.Services
{
    public class PdfExportService : IPdfExportService
    {
        private readonly ILogger<PdfExportService> _logger;

        public PdfExportService(ILogger<PdfExportService> logger)
        {
            _logger = logger;
            // Required for QuestPDF
            QuestPDF.Settings.License = LicenseType.Community;
        }

        // ==================== SUPER ADMIN REPORTS ====================

        public byte[] GenerateSystemOverviewPdf(SystemOverviewReportDTO report)
        {
            try
            {
                var document = Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(2, Unit.Centimetre);
                        page.DefaultTextStyle(x => x.FontSize(10));

                        page.Header().Element(ComposeHeader("System Overview Report"));

                        page.Content().Column(column =>
                        {
                            column.Spacing(10);

                            // Summary Section
                            column.Item().Text("System Summary").FontSize(14).Bold();
                            column.Item().PaddingVertical(5).LineHorizontal(1);

                            column.Item().Row(row =>
                            {
                                row.RelativeItem().Column(col =>
                                {
                                    AddInfoRow(col, "Total Banks", report.TotalBanks.ToString());
                                    AddInfoRow(col, "Active Banks", report.ActiveBanks.ToString());
                                    AddInfoRow(col, "Inactive Banks", report.InactiveBanks.ToString());
                                    AddInfoRow(col, "Total Clients", report.TotalClients.ToString());
                                    AddInfoRow(col, "Total Bank Users", report.TotalBankUsers.ToString());
                                });

                                row.RelativeItem().Column(col =>
                                {
                                    AddInfoRow(col, "Total Employees", report.TotalEmployees.ToString());
                                    AddInfoRow(col, "Total Beneficiaries", report.TotalBeneficiaries.ToString());
                                    AddInfoRow(col, "Pending Verifications", report.PendingVerifications.ToString());
                                    AddInfoRow(col, "Verified Clients", report.VerifiedClients.ToString());
                                    AddInfoRow(col, "Rejected Clients", report.RejectedClients.ToString());
                                });
                            });

                            column.Item().PaddingTop(15).Text("Payment Statistics").FontSize(14).Bold();
                            column.Item().PaddingVertical(5).LineHorizontal(1);

                            column.Item().Row(row =>
                            {
                                row.RelativeItem().Column(col =>
                                {
                                    AddInfoRow(col, "Total Payments", report.TotalPayments.ToString());
                                    AddInfoRow(col, "Pending Payments", report.PendingPayments.ToString());
                                    AddInfoRow(col, "Approved Payments", report.ApprovedPayments.ToString());
                                    AddInfoRow(col, "Rejected Payments", report.RejectedPayments.ToString());
                                });

                                row.RelativeItem().Column(col =>
                                {
                                    AddInfoRow(col, "Total Payment Value", $"₹{report.TotalPaymentValue:N2}");
                                    AddInfoRow(col, "Total Salary Disbursements", report.TotalSalaryDisbursements.ToString());
                                    AddInfoRow(col, "Total Salary Value", $"₹{report.TotalSalaryValue:N2}");
                                });
                            });
                        });

                        page.Footer().Element(ComposeFooter);
                    });
                });

                return document.GeneratePdf();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate System Overview PDF");
                throw;
            }
        }

        public byte[] GenerateBankPerformancePdf(IEnumerable<BankPerformanceReportDTO> report, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var banks = report.ToList();

                var document = Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4.Landscape());
                        page.Margin(1.5f, Unit.Centimetre);
                        page.DefaultTextStyle(x => x.FontSize(9));

                        // ✅ FIXED HEADER
                        page.Header().Column(header =>
                        {
                            header.Item().Element(ComposeHeader("Bank Performance Report"));

                            if (startDate.HasValue && endDate.HasValue)
                            {
                                header.Item().PaddingTop(5)
                                    .Text($"Period: {startDate.Value:dd/MM/yyyy} to {endDate.Value:dd/MM/yyyy}")
                                    .FontSize(10).Italic();
                            }
                        });

                        // ✅ TABLE CONTENT
                        page.Content().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(120); // Bank Name
                                columns.RelativeColumn(1);   // Clients
                                columns.RelativeColumn(1);   // Payments
                                columns.RelativeColumn(1.5f); // Payment Value
                                columns.RelativeColumn(1);   // Verifications
                            });

                            // Table Header
                            table.Header(header =>
                            {
                                header.Cell().Element(CellStyle).Text("Bank Name").Bold();
                                header.Cell().Element(CellStyle).Text("Clients").Bold();
                                header.Cell().Element(CellStyle).Text("Payments").Bold();
                                header.Cell().Element(CellStyle).Text("Payment Value").Bold();
                                header.Cell().Element(CellStyle).Text("Pending Verif.").Bold();
                            });

                            // Table Rows
                            foreach (var bank in banks)
                            {
                                table.Cell().Element(CellStyle).Text(bank.BankName);
                                table.Cell().Element(CellStyle).Text($"{bank.TotalClients} ({bank.ActiveClients} active)");
                                table.Cell().Element(CellStyle).Text($"{bank.TotalPayments} ({bank.ApprovedPayments} approved)");
                                table.Cell().Element(CellStyle).Text($"₹{bank.TotalPaymentValue:N2}");
                                table.Cell().Element(CellStyle).Text(bank.PendingVerifications.ToString());
                            }
                        });

                        // ✅ FOOTER
                        page.Footer().Element(ComposeFooter);
                    });
                });

                return document.GeneratePdf();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate Bank Performance PDF");
                throw;
            }
        }


        public byte[] GenerateTransactionVolumePdf(TransactionVolumeReportDTO report)
        {
            try
            {
                var document = Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(2, Unit.Centimetre);
                        page.DefaultTextStyle(x => x.FontSize(10));

                        // ✅ FIXED HEADER
                        page.Header().Column(header =>
                        {
                            header.Item().Element(ComposeHeader("Transaction Volume Report"));

                            header.Item().PaddingTop(5)
                                .Text($"Period: {report.StartDate:dd/MM/yyyy} to {report.EndDate:dd/MM/yyyy}")
                                .FontSize(10).Italic();
                        });

                        page.Content().Column(column =>
                        {
                            column.Spacing(10);

                            // Summary
                            column.Item().Text("Summary").FontSize(14).Bold();
                            column.Item().LineHorizontal(1);
                            column.Item().Row(row =>
                            {
                                row.RelativeItem().Column(col =>
                                {
                                    AddInfoRow(col, "Total Payments", report.TotalPayments.ToString());
                                    AddInfoRow(col, "Total Amount", $"₹{report.TotalPaymentAmount:N2}");
                                });
                                row.RelativeItem().Column(col =>
                                {
                                    AddInfoRow(col, "Salary Disbursements", report.TotalSalaryDisbursements.ToString());
                                    AddInfoRow(col, "Salary Amount", $"₹{report.TotalSalaryAmount:N2}");
                                });
                            });

                            // Payment Type Breakdown
                            column.Item().PaddingTop(10).Text("Payment Type Breakdown").FontSize(14).Bold();
                            column.Item().LineHorizontal(1);
                            column.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                table.Header(header =>
                                {
                                    header.Cell().Element(CellStyle).Text("Type").Bold();
                                    header.Cell().Element(CellStyle).Text("Count").Bold();
                                    header.Cell().Element(CellStyle).Text("Amount").Bold();
                                });

                                table.Cell().Element(CellStyle).Text("RTGS");
                                table.Cell().Element(CellStyle).Text(report.PaymentTypeBreakdown.RTGSCount.ToString());
                                table.Cell().Element(CellStyle).Text($"₹{report.PaymentTypeBreakdown.RTGSAmount:N2}");

                                table.Cell().Element(CellStyle).Text("IMPS");
                                table.Cell().Element(CellStyle).Text(report.PaymentTypeBreakdown.IMPSCount.ToString());
                                table.Cell().Element(CellStyle).Text($"₹{report.PaymentTypeBreakdown.IMPSAmount:N2}");

                                table.Cell().Element(CellStyle).Text("NEFT");
                                table.Cell().Element(CellStyle).Text(report.PaymentTypeBreakdown.NEFTCount.ToString());
                                table.Cell().Element(CellStyle).Text($"₹{report.PaymentTypeBreakdown.NEFTAmount:N2}");
                            });

                            // Status Breakdown
                            column.Item().PaddingTop(10).Text("Payment Status Breakdown").FontSize(14).Bold();
                            column.Item().LineHorizontal(1);
                            column.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                table.Header(header =>
                                {
                                    header.Cell().Element(CellStyle).Text("Status").Bold();
                                    header.Cell().Element(CellStyle).Text("Count").Bold();
                                    header.Cell().Element(CellStyle).Text("Amount").Bold();
                                });

                                table.Cell().Element(CellStyle).Text("Pending");
                                table.Cell().Element(CellStyle).Text(report.PaymentStatusBreakdown.PendingCount.ToString());
                                table.Cell().Element(CellStyle).Text($"₹{report.PaymentStatusBreakdown.PendingAmount:N2}");

                                table.Cell().Element(CellStyle).Text("Approved");
                                table.Cell().Element(CellStyle).Text(report.PaymentStatusBreakdown.ApprovedCount.ToString());
                                table.Cell().Element(CellStyle).Text($"₹{report.PaymentStatusBreakdown.ApprovedAmount:N2}");

                                table.Cell().Element(CellStyle).Text("Rejected");
                                table.Cell().Element(CellStyle).Text(report.PaymentStatusBreakdown.RejectedCount.ToString());
                                table.Cell().Element(CellStyle).Text($"₹{report.PaymentStatusBreakdown.RejectedAmount:N2}");
                            });
                        });

                        page.Footer().Element(ComposeFooter);
                    });
                });

                return document.GeneratePdf();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate Transaction Volume PDF");
                throw;
            }
        }


        public byte[] GenerateFinancialSummaryPdf(FinancialSummaryReportDTO report)
        {
            try
            {
                var document = Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(2, Unit.Centimetre);
                        page.DefaultTextStyle(x => x.FontSize(10));

                        // ✅ FIXED HEADER
                        page.Header().Column(header =>
                        {
                            header.Item().Element(ComposeHeader("Financial Summary Report"));

                            header.Item().PaddingTop(5)
                                .Text($"Period: {report.StartDate:dd/MM/yyyy} to {report.EndDate:dd/MM/yyyy}")
                                .FontSize(10).Italic();
                        });

                        page.Content().Column(column =>
                        {
                            column.Spacing(10);

                            // Financial Overview
                            column.Item().Text("Financial Overview").FontSize(14).Bold();
                            column.Item().LineHorizontal(1);
                            column.Item().Row(row =>
                            {
                                row.RelativeItem().Column(col =>
                                {
                                    AddInfoRow(col, "Total Money Flow", $"₹{report.TotalMoneyFlow:N2}");
                                    AddInfoRow(col, "Total Payments", $"₹{report.TotalPaymentsValue:N2}");
                                });
                                row.RelativeItem().Column(col =>
                                {
                                    AddInfoRow(col, "Total Salaries", $"₹{report.TotalSalariesValue:N2}");
                                    AddInfoRow(col, "Client Balance", $"₹{report.TotalClientBalance:N2}");
                                });
                            });

                            // Top Clients
                            column.Item().PaddingTop(10).Text("Top 10 Clients by Transaction Volume").FontSize(14).Bold();
                            column.Item().LineHorizontal(1);
                            column.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(2);
                                    columns.RelativeColumn();
                                    columns.RelativeColumn(1.5f);
                                });

                                table.Header(header =>
                                {
                                    header.Cell().Element(CellStyle).Text("Client Name").Bold();
                                    header.Cell().Element(CellStyle).Text("Transactions").Bold();
                                    header.Cell().Element(CellStyle).Text("Total Value").Bold();
                                });

                                foreach (var client in report.TopClientsByVolume.Take(10))
                                {
                                    table.Cell().Element(CellStyle).Text(client.ClientName);
                                    table.Cell().Element(CellStyle).Text(client.TransactionCount.ToString());
                                    table.Cell().Element(CellStyle).Text($"₹{client.TotalTransactionValue:N2}");
                                }
                            });
                        });

                        page.Footer().Element(ComposeFooter);
                    });
                });

                return document.GeneratePdf();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate Financial Summary PDF");
                throw;
            }
        }


        // ==================== BANK USER REPORTS ====================

        public byte[] GenerateTransactionReportPdf(
     IEnumerable<TransactionReportDTO> transactions,
     string clientName,
     DateTime? startDate,
     DateTime? endDate)
        {
            try
            {
                var transactionList = transactions.ToList();

                var document = Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4.Landscape());
                        page.Margin(1.5f, Unit.Centimetre);
                        page.DefaultTextStyle(x => x.FontSize(8));

                        // ✅ FIXED HEADER — use Column instead of Element
                        page.Header().Column(header =>
                        {
                            header.Item().Element(ComposeHeader($"Transaction Report - {clientName}"));

                            if (startDate.HasValue && endDate.HasValue)
                            {
                                header.Item().PaddingTop(5)
                                    .Text($"Period: {startDate.Value:dd/MM/yyyy} to {endDate.Value:dd/MM/yyyy}")
                                    .FontSize(10).Italic();
                            }
                        });

                        // ✅ CONTENT
                        page.Content().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(60);  // Date
                                columns.ConstantColumn(50);  // Type
                                columns.RelativeColumn();    // Beneficiary
                                columns.ConstantColumn(80);  // Amount
                                columns.ConstantColumn(60);  // Status
                            });

                            // Table header
                            table.Header(header =>
                            {
                                header.Cell().Element(CellStyle).Text("Date").Bold();
                                header.Cell().Element(CellStyle).Text("Type").Bold();
                                header.Cell().Element(CellStyle).Text("Beneficiary").Bold();
                                header.Cell().Element(CellStyle).Text("Amount").Bold();
                                header.Cell().Element(CellStyle).Text("Status").Bold();
                            });

                            // Rows
                            foreach (var txn in transactionList)
                            {
                                table.Cell().Element(CellStyle).Text(txn.PaymentDate.ToString("dd/MM/yyyy"));
                                table.Cell().Element(CellStyle).Text(txn.PaymentType);
                                table.Cell().Element(CellStyle).Text(txn.BeneficiaryName);
                                table.Cell().Element(CellStyle).Text($"₹{txn.Amount:N2}");
                                table.Cell().Element(CellStyle).Text(txn.Status);
                            }
                        });

                        // ✅ FOOTER
                        page.Footer().Element(ComposeFooter);
                    });
                });

                return document.GeneratePdf();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate Transaction Report PDF");
                throw;
            }
        }



        public byte[] GenerateCustomerOnboardingPdf(CustomerOnboardingReportDTO report)
        {
            try
            {
                var document = Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4.Landscape());
                        page.Margin(1.5f, Unit.Centimetre);
                        page.DefaultTextStyle(x => x.FontSize(9));

                        // ✅ FIXED HEADER
                        page.Header().Column(header =>
                        {
                            header.Item().Element(ComposeHeader("Customer Onboarding Report"));
                            // Optional future flexibility: you can easily add filters or date info later here
                        });

                        // ✅ CONTENT
                        page.Content().Column(column =>
                        {
                            column.Spacing(10);

                            // Summary
                            column.Item().Row(row =>
                            {
                                row.RelativeItem().Column(col =>
                                {
                                    AddInfoRow(col, "Total Onboarded", report.TotalOnboarded.ToString());
                                    AddInfoRow(col, "Pending", report.PendingVerifications.ToString());
                                });
                                row.RelativeItem().Column(col =>
                                {
                                    AddInfoRow(col, "Approved", report.ApprovedClients.ToString());
                                    AddInfoRow(col, "Rejected", report.RejectedClients.ToString());
                                });
                                row.RelativeItem().Column(col =>
                                {
                                    AddInfoRow(col, "Avg Days to Verify", report.AverageOnboardingDays.ToString("N1"));
                                    AddInfoRow(col, "Documents Uploaded", report.DocumentsUploaded.ToString());
                                });
                            });

                            column.Item().LineHorizontal(1);

                            // Client Details Table
                            column.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(1.5f);
                                    columns.RelativeColumn();
                                    columns.ConstantColumn(80);
                                    columns.ConstantColumn(60);
                                    columns.ConstantColumn(50);
                                });

                                table.Header(header =>
                                {
                                    header.Cell().Element(CellStyle).Text("Client Name").Bold();
                                    header.Cell().Element(CellStyle).Text("Email").Bold();
                                    header.Cell().Element(CellStyle).Text("Business Type").Bold();
                                    header.Cell().Element(CellStyle).Text("Status").Bold();
                                    header.Cell().Element(CellStyle).Text("Docs").Bold();
                                });

                                foreach (var client in report.ClientDetails)
                                {
                                    table.Cell().Element(CellStyle).Text(client.ClientName);
                                    table.Cell().Element(CellStyle).Text(client.Email);
                                    table.Cell().Element(CellStyle).Text(client.BusinessType);
                                    table.Cell().Element(CellStyle).Text(client.VerificationStatus);
                                    table.Cell().Element(CellStyle).Text(client.DocumentCount.ToString());
                                }
                            });
                        });

                        // ✅ FOOTER
                        page.Footer().Element(ComposeFooter);
                    });
                });

                return document.GeneratePdf();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate Customer Onboarding PDF");
                throw;
            }
        }


        public byte[] GeneratePaymentApprovalPdf(PaymentApprovalReportDTO report)
        {
            try
            {
                var document = Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(2, Unit.Centimetre);
                        page.DefaultTextStyle(x => x.FontSize(10));

                        // ✅ FIXED HEADER — use a Column so multiple elements are allowed safely
                        page.Header().Column(header =>
                        {
                            header.Item().Element(ComposeHeader("Payment Approval Report"));
                            // Optional: add date filters, user info, etc. later here
                        });

                        // ✅ CONTENT
                        page.Content().Column(column =>
                        {
                            column.Spacing(10);

                            // Summary Section
                            column.Item().Text("Summary").FontSize(14).Bold();
                            column.Item().LineHorizontal(1);
                            column.Item().Row(row =>
                            {
                                row.RelativeItem().Column(col =>
                                {
                                    AddInfoRow(col, "Total Payments", report.TotalPayments.ToString());
                                    AddInfoRow(col, "Pending", report.PendingApprovals.ToString());
                                    AddInfoRow(col, "Approved", report.ApprovedPayments.ToString());
                                });
                                row.RelativeItem().Column(col =>
                                {
                                    AddInfoRow(col, "Rejected", report.RejectedPayments.ToString());
                                    AddInfoRow(col, "Approved Amount", $"₹{report.TotalApprovedAmount:N2}");
                                    AddInfoRow(col, "Rejected Amount", $"₹{report.TotalRejectedAmount:N2}");
                                });
                            });

                            // Bank User Performance Section
                            column.Item().PaddingTop(10).Text("Bank User Performance").FontSize(14).Bold();
                            column.Item().LineHorizontal(1);
                            column.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(2);   // Bank User
                                    columns.RelativeColumn();    // Approved
                                    columns.RelativeColumn();    // Rejected
                                    columns.RelativeColumn(1.5f); // Total Amount
                                });

                                // Table Header
                                table.Header(header =>
                                {
                                    header.Cell().Element(CellStyle).Text("Bank User").Bold();
                                    header.Cell().Element(CellStyle).Text("Approved").Bold();
                                    header.Cell().Element(CellStyle).Text("Rejected").Bold();
                                    header.Cell().Element(CellStyle).Text("Total Amount").Bold();
                                });

                                // Table Rows
                                foreach (var user in report.BankUserPerformance)
                                {
                                    table.Cell().Element(CellStyle).Text(user.BankUserName);
                                    table.Cell().Element(CellStyle).Text(user.TotalApproved.ToString());
                                    table.Cell().Element(CellStyle).Text(user.TotalRejected.ToString());
                                    table.Cell().Element(CellStyle).Text($"₹{user.TotalAmountApproved:N2}");
                                }
                            });
                        });

                        // ✅ FOOTER
                        page.Footer().Element(ComposeFooter);
                    });
                });

                return document.GeneratePdf();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate Payment Approval PDF");
                throw;
            }
        }


        public byte[] GenerateClientActivityPdf(IEnumerable<ClientActivityReportDTO> report)
        {
            try
            {
                var clients = report.ToList();

                var document = Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4.Landscape());
                        page.Margin(1.5f, Unit.Centimetre);
                        page.DefaultTextStyle(x => x.FontSize(8));

                        // ✅ FIXED HEADER: Use Column instead of Element()
                        page.Header().Column(header =>
                        {
                            header.Item().Element(ComposeHeader("Client Activity Report"));
                            // Optionally, you can safely add another line here (e.g. date, filters, etc.)
                        });

                        // ✅ CONTENT
                        page.Content().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(1.5f);  // Client Name
                                columns.ConstantColumn(60);    // Status
                                columns.ConstantColumn(50);    // Payments
                                columns.ConstantColumn(90);    // Payment Value
                                columns.ConstantColumn(50);    // Employees
                                columns.ConstantColumn(50);    // Beneficiaries
                            });

                            // Table Header
                            table.Header(header =>
                            {
                                header.Cell().Element(CellStyle).Text("Client Name").Bold();
                                header.Cell().Element(CellStyle).Text("Status").Bold();
                                header.Cell().Element(CellStyle).Text("Payments").Bold();
                                header.Cell().Element(CellStyle).Text("Payment Value").Bold();
                                header.Cell().Element(CellStyle).Text("Employees").Bold();
                                header.Cell().Element(CellStyle).Text("Beneficiaries").Bold();
                            });

                            // Table Rows
                            foreach (var client in clients)
                            {
                                table.Cell().Element(CellStyle).Text(client.ClientName);
                                table.Cell().Element(CellStyle).Text(client.IsActive ? "Active" : "Inactive");
                                table.Cell().Element(CellStyle).Text(client.TotalPayments.ToString());
                                table.Cell().Element(CellStyle).Text($"₹{client.TotalPaymentValue:N2}");
                                table.Cell().Element(CellStyle).Text(client.TotalEmployees.ToString());
                                table.Cell().Element(CellStyle).Text(client.TotalBeneficiaries.ToString());
                            }
                        });

                        // ✅ FOOTER
                        page.Footer().Element(ComposeFooter);
                    });
                });

                return document.GeneratePdf();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate Client Activity PDF");
                throw;
            }
        }


        // ==================== HELPER METHODS ====================

        private static Action<QuestPDF.Infrastructure.IContainer> ComposeHeader(string title)
        {
            return container =>
            {
                container.Column(column =>
                {
                    column.Item().Text(title).FontSize(18).Bold().FontColor(QuestPDF.Helpers.Colors.Blue.Darken2);
                    column.Item().Text($"Generated: {DateTime.UtcNow:dd/MM/yyyy HH:mm} UTC").FontSize(9).Italic();
                    column.Item().PaddingTop(5).LineHorizontal(2).LineColor(QuestPDF.Helpers.Colors.Blue.Darken2);
                });
            };
        }

        private static void ComposeFooter(QuestPDF.Infrastructure.IContainer container)
        {
            container.AlignCenter().Column(column =>
            {
                column.Item().LineHorizontal(1);
                column.Item().PaddingTop(5).DefaultTextStyle(x => x.FontSize(9)).Text(text =>
                {
                    text.Span("Page ");
                    text.CurrentPageNumber();
                    text.Span(" of ");
                    text.TotalPages();
                    text.Span(" | Generated by Banking & Payments System");
                });
            });
        }

        private static QuestPDF.Infrastructure.IContainer CellStyle(QuestPDF.Infrastructure.IContainer container)
        {
            return container
                .Border(1)
                .BorderColor(QuestPDF.Helpers.Colors.Grey.Lighten2)
                .Padding(5)
                .AlignMiddle();
        }

        private static void AddInfoRow(ColumnDescriptor column, string label, string value)
        {
            column.Item().Row(row =>
            {
                row.ConstantItem(150).Text(label).Bold();
                row.RelativeItem().Text(value);
            });
        }
    }
}