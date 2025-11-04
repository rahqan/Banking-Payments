using DocumentFormat.OpenXml.Spreadsheet;
using Banking_Payments.Models;
using Microsoft.EntityFrameworkCore;
using Banking_Payments.Models.Enums;

namespace Banking_Payments.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() { }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<BankUser> BankUsers { get; set; }
        public virtual DbSet<Beneficiary> Beneficiaries { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<SalaryDisbursement> SalaryDisbursements { get; set; }

        // ADDED: This will suppress the warning temporarily
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure decimal precision for monetary values
            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Salary)
                .HasPrecision(18, 2);

            modelBuilder.Entity<SalaryDisbursement>()
                .Property(s => s.Amount)
                .HasPrecision(18, 2);

            // Configure all relationships with proper delete behavior to avoid cycles

            // Client -> Bank relationship
            modelBuilder.Entity<Client>()
                .HasOne(c => c.Bank)
                .WithMany(b => b.Clients)
                .OnDelete(DeleteBehavior.NoAction);

            // Payment -> Client relationship (NO ACTION to break cascade cycle)
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Client)
                .WithMany(c => c.Payments)
                .OnDelete(DeleteBehavior.NoAction);

            // Beneficiary -> Client relationship (NO ACTION to break cascade cycle)
            modelBuilder.Entity<Beneficiary>()
                .HasOne(b => b.Client)
                .WithMany()
                .HasForeignKey(b => b.ClientId)
                .OnDelete(DeleteBehavior.NoAction);

            // Payment -> Beneficiary relationship (RESTRICT to break cascade cycle)
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Beneficiary)
                .WithMany()
                .HasForeignKey(p => p.BeneficiaryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Employee -> Client relationship
            modelBuilder.Entity<SalaryDisbursement>()
                .HasOne(e => e.Employee)
                .WithMany(s => s.SalaryDisbursements)
                .OnDelete(DeleteBehavior.NoAction);

            // Document -> Client relationship
            modelBuilder.Entity<Document>()
                .HasOne(c => c.Client)
                .WithMany(d => d.Documents)
                .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<Payment>()
            //    .HasOne()

            // // Seed Data
            // //modelBuilder.Entity<Admin>().HasData(
            // //    new Admin { AdminId = 1, Code = "ADM001", Name = "Alice Johnson", Email = "alice@banking.com", Password = "Pass@123" },
            // //    new Admin { AdminId = 2, Code = "ADM002", Name = "Bob Smith", Email = "bob@banking.com", Password = "Pass@123" },
            // //    new Admin { AdminId = 3, Code = "ADM003", Name = "Charlie Brown", Email = "charlie@banking.com", Password = "Pass@123" }
            // //);

            // //modelBuilder.Entity<Bank>().HasData(
            // //    new Bank { BankId = 1, Code = "B001", Name = "First National Bank", Address = "123 Finance St", PanNumber = "AAAPL1234C", RegistrationNumber = "REG001", ContactEmail = "info@fnb.com", ContactPhone = "1234567890", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), AdminId = 1 },
            // //    new Bank { BankId = 2, Code = "B002", Name = "Global Trust Bank", Address = "456 Trust Ave", PanNumber = "BBBTY4567P", RegistrationNumber = "REG002", ContactEmail = "contact@gtb.com", ContactPhone = "9876543210", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), AdminId = 2 },
            // //    new Bank { BankId = 3, Code = "B003", Name = "Metro Finance Bank", Address = "789 Metro Rd", PanNumber = "CCCXY7890K", RegistrationNumber = "REG003", ContactEmail = "support@mfb.com", ContactPhone = "5647382910", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), AdminId = 3 }
            // //);

            // //modelBuilder.Entity<BankUser>().HasData(
            // //    new BankUser { BankUserId = 1, Code = "BU001", Name = "Emma Green", Email = "emma@fnb.com", Password = "123456", PhoneNumber = "9876543210", BankId = 1 },
            // //    new BankUser { BankUserId = 2, Code = "BU002", Name = "Liam Gray", Email = "liam@gtb.com", Password = "123456", PhoneNumber = "8765432109", BankId = 2 },
            // //    new BankUser { BankUserId = 3, Code = "BU003", Name = "Olivia White", Email = "olivia@mfb.com", Password = "123456", PhoneNumber = "7654321098", BankId = 3 }
            // //);

            // //modelBuilder.Entity<Client>().HasData(
            // //    new Client { ClientId = 1, Password = "client123", Code = "C001", Name = "TechWave Ltd", Email = "hr@techwave.com", BusinessType = "IT Services", Address = "12 Silicon Ave", VerificationStatus = "Verified", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), BankId = 1, BankUserId = 1 },
            // //    new Client { ClientId = 2, Password = "client123", Code = "C002", Name = "GreenFoods Inc", Email = "admin@greenfoods.com", BusinessType = "Food Manufacturing", Address = "45 Organic Rd", VerificationStatus = "Pending", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), BankId = 2, BankUserId = 2 },
            // //    new Client { ClientId = 3, Password = "client123", Code = "C003", Name = "Urban Builders", Email = "info@urbanbuilders.com", BusinessType = "Construction", Address = "78 Brick Lane", VerificationStatus = "Verified", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), BankId = 3, BankUserId = 3 }
            // //);

            // //modelBuilder.Entity<Beneficiary>().HasData(
            // //    new Beneficiary { BeneficiaryId = 1, Name = "John Doe", AccountNumber = "1234567890", IfscCode = "FNB0001", ClientId = 1 },
            // //    new Beneficiary { BeneficiaryId = 2, Name = "Mary Jane", AccountNumber = "2345678901", IfscCode = "GTB0002", ClientId = 2 },
            // //    new Beneficiary { BeneficiaryId = 3, Name = "Peter Parker", AccountNumber = "3456789012", IfscCode = "MFB0003", ClientId = 3 }
            // //);



            // //modelBuilder.Entity<Payment>().HasData(
            // //    new Payment { PaymentId = 1, Amount = 10000, PaymentDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), status = VerificationStatus.Verified, Type = PaymentType.NEFT, BeneficiaryId = 1, ClientId = 1, BankUserId = 1 },
            // //    new Payment { PaymentId = 2, Amount = 25000, PaymentDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), status = VerificationStatus.Verified, Type = PaymentType.RTGS, BeneficiaryId = 2, ClientId = 2, BankUserId = 2 },
            // //    new Payment { PaymentId = 3, Amount = 5000, PaymentDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), status = VerificationStatus.Pending, Type = PaymentType.IMPS, BeneficiaryId = 3, ClientId = 3, BankUserId = 3 }
            // //);

            // //modelBuilder.Entity<SalaryDisbursement>().HasData(
            // //    new SalaryDisbursement { SalaryDisbursementId = 1, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), Amount = 50000, EmployeeId = 1, ClientId = 1 },
            // //    new SalaryDisbursement { SalaryDisbursementId = 2, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), Amount = 45000, EmployeeId = 2, ClientId = 2 },
            // //    new SalaryDisbursement { SalaryDisbursementId = 3, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), Amount = 55000, EmployeeId = 3, ClientId = 3 }
            // //);

            // //modelBuilder.Entity<Document>().HasData(
            // //    new Document
            // //    {
            // //        DocumentId = 1,
            // //        Name = "PAN Proof",
            // //        Url = "/docs/pan1.pdf",
            // //        BankUserId = 1,
            // //        ClientId = 1,
            // //        UploadedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            // //        DocType = "KYC"
            // //    },
            // //    new Document
            // //    {
            // //        DocumentId = 2,
            // //        Name = "Registration Cert",
            // //        Url = "/docs/reg2.pdf",
            // //        BankUserId = 2,
            // //        ClientId = 2,
            // //        UploadedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            // //        DocType = "Registration"
            // //    },
            // //    new Document
            // //    {
            // //        DocumentId = 3,
            // //        Name = "Address Proof",
            // //        Url = "/docs/address3.pdf",
            // //        BankUserId = 3,
            // //        ClientId = 3,
            // //        UploadedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            // //        DocType = "Address"
            // //    }
            // //);


            // modelBuilder.Entity<Admin>().HasData(
            //    new Admin { AdminId = 1, Code = "ADM001", Name = "Alice Johnson", Email = "alice@banking.com", Password = "Pass@123" },
            //    new Admin { AdminId = 2, Code = "ADM002", Name = "Bob Smith", Email = "bob@banking.com", Password = "Pass@123" },
            //    new Admin { AdminId = 3, Code = "ADM003", Name = "Charlie Brown", Email = "charlie@banking.com", Password = "Pass@123" }
            //);

            // modelBuilder.Entity<Bank>().HasData(
            //     new Bank { BankId = 1, Code = "B001", Name = "First National Bank", Address = "123 Finance St", PanNumber = "AAAPL1234C", RegistrationNumber = "REG001", ContactEmail = "info@fnb.com", ContactPhone = "1234567890", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), AdminId = 1 },
            //     new Bank { BankId = 2, Code = "B002", Name = "Global Trust Bank", Address = "456 Trust Ave", PanNumber = "BBBTY4567P", RegistrationNumber = "REG002", ContactEmail = "contact@gtb.com", ContactPhone = "9876543210", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), AdminId = 2 },
            //     new Bank { BankId = 3, Code = "B003", Name = "Metro Finance Bank", Address = "789 Metro Rd", PanNumber = "CCCXY7890K", RegistrationNumber = "REG003", ContactEmail = "support@mfb.com", ContactPhone = "5647382910", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), AdminId = 3 }
            // );

            // modelBuilder.Entity<BankUser>().HasData(
            //     new BankUser { BankUserId = 1, Code = "BU001", Name = "Emma Green", Email = "emma@fnb.com", Password = "123456", PhoneNumber = "9876543210", BankId = 1 },
            //     new BankUser { BankUserId = 2, Code = "BU002", Name = "Liam Gray", Email = "liam@gtb.com", Password = "123456", PhoneNumber = "8765432109", BankId = 2 },
            //     new BankUser { BankUserId = 3, Code = "BU003", Name = "Olivia White", Email = "olivia@mfb.com", Password = "123456", PhoneNumber = "7654321098", BankId = 3 }
            // );

            // // 👉 Fixed: CreatedBy = null (instead of 0)
            // modelBuilder.Entity<Client>().HasData(
            //     new Client { ClientId = 1, Password = "client123", Code = "C001", Name = "TechWave Ltd", Email = "hr@techwave.com", BusinessType = "IT Services", Address = "12 Silicon Ave", VerificationStatus = "Verified", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), BankId = 1, BankUserId = 1, ApprovedBy = null },
            //     new Client { ClientId = 2, Password = "client123", Code = "C002", Name = "GreenFoods Inc", Email = "admin@greenfoods.com", BusinessType = "Food Manufacturing", Address = "45 Organic Rd", VerificationStatus = "Pending", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), BankId = 2, BankUserId = 2,  ApprovedBy = null },
            //     new Client { ClientId = 3, Password = "client123", Code = "C003", Name = "Urban Builders", Email = "info@urbanbuilders.com", BusinessType = "Construction", Address = "78 Brick Lane", VerificationStatus = "Verified", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), BankId = 3, BankUserId = 3, ApprovedBy = null }
            // );

            // modelBuilder.Entity<Beneficiary>().HasData(
            //     new Beneficiary { BeneficiaryId = 1, Name = "John Doe", AccountNumber = "1234567890", IfscCode = "FNB0001", ClientId = 1 },
            //     new Beneficiary { BeneficiaryId = 2, Name = "Mary Jane", AccountNumber = "2345678901", IfscCode = "GTB0002", ClientId = 2 },
            //     new Beneficiary { BeneficiaryId = 3, Name = "Peter Parker", AccountNumber = "3456789012", IfscCode = "MFB0003", ClientId = 3 }
            // );

            // modelBuilder.Entity<Payment>().HasData(
            //     new Payment { PaymentId = 1, Amount = 10000, PaymentDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), status = VerificationStatus.Verified, Type = PaymentType.NEFT, BeneficiaryId = 1, ClientId = 1, BankUserId = 1 },
            //     new Payment { PaymentId = 2, Amount = 25000, PaymentDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), status = VerificationStatus.Verified, Type = PaymentType.RTGS, BeneficiaryId = 2, ClientId = 2, BankUserId = 2 },
            //     new Payment { PaymentId = 3, Amount = 5000, PaymentDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), status = VerificationStatus.Pending, Type = PaymentType.IMPS, BeneficiaryId = 3, ClientId = 3, BankUserId = 3 }
            // );

            // modelBuilder.Entity<Document>().HasData(
            //     new Document { DocumentId = 1, Name = "PAN Proof", Url = "/docs/pan1.pdf", BankUserId = 1, ClientId = 1, UploadedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), DocType = "KYC" },
            //     new Document { DocumentId = 2, Name = "Registration Cert", Url = "/docs/reg2.pdf", BankUserId = 2, ClientId = 2, UploadedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), DocType = "Registration" },
            //     new Document { DocumentId = 3, Name = "Address Proof", Url = "/docs/address3.pdf", BankUserId = 3, ClientId = 3, UploadedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), DocType = "Address" }
            // );


            // ------------------------- SEED DATA -----------------------------

            // ADMINS
            modelBuilder.Entity<Admin>().HasData(
                new Admin { AdminId = 1, Code = "ADM001", Name = "Alice Johnson", Email = "alice@banking.com", Password = "Pass@123" },
                new Admin { AdminId = 2, Code = "ADM002", Name = "Bob Smith", Email = "bob@banking.com", Password = "Pass@123" },
                new Admin { AdminId = 3, Code = "ADM003", Name = "Charlie Brown", Email = "charlie@banking.com", Password = "Pass@123" },
                new Admin { AdminId = 4, Code = "ADM004", Name = "Diana Prince", Email = "diana@banking.com", Password = "Pass@123" },
                new Admin { AdminId = 5, Code = "ADM005", Name = "Ethan Hunt", Email = "ethan@banking.com", Password = "Pass@123" },
                new Admin { AdminId = 6, Code = "ADM006", Name = "Fiona Carter", Email = "fiona@banking.com", Password = "Pass@123" },
                new Admin { AdminId = 7, Code = "ADM007", Name = "George Miller", Email = "george@banking.com", Password = "Pass@123" },
                new Admin { AdminId = 8, Code = "ADM008", Name = "Hannah Wells", Email = "hannah@banking.com", Password = "Pass@123" },
                new Admin { AdminId = 9, Code = "ADM009", Name = "Ian Holmes", Email = "ian@banking.com", Password = "Pass@123" },
                new Admin { AdminId = 10, Code = "ADM010", Name = "Julia Roberts", Email = "julia@banking.com", Password = "Pass@123" }
            );

            // BANKS
            modelBuilder.Entity<Bank>().HasData(
                new Bank { BankId = 1, Code = "B001", Name = "First National Bank", Address = "123 Finance St", PanNumber = "AAAPL1234C", RegistrationNumber = "REG001", ContactEmail = "info@fnb.com", ContactPhone = "1234567890", IsActive = true, CreatedAt = DateTime.UtcNow, AdminId = 1 },
                new Bank { BankId = 2, Code = "B002", Name = "Global Trust Bank", Address = "456 Trust Ave", PanNumber = "BBBTY4567P", RegistrationNumber = "REG002", ContactEmail = "contact@gtb.com", ContactPhone = "9876543210", IsActive = true, CreatedAt = DateTime.UtcNow, AdminId = 2 },
                new Bank { BankId = 3, Code = "B003", Name = "Metro Finance Bank", Address = "789 Metro Rd", PanNumber = "CCCXY7890K", RegistrationNumber = "REG003", ContactEmail = "support@mfb.com", ContactPhone = "5647382910", IsActive = true, CreatedAt = DateTime.UtcNow, AdminId = 3 },
                new Bank { BankId = 4, Code = "B004", Name = "Unity Bank", Address = "88 Liberty Ln", PanNumber = "DDDPL2234J", RegistrationNumber = "REG004", ContactEmail = "help@unity.com", ContactPhone = "9898989898", IsActive = true, CreatedAt = DateTime.UtcNow, AdminId = 4 },
                new Bank { BankId = 5, Code = "B005", Name = "Central Credit Bank", Address = "22 Market Rd", PanNumber = "EEERW5567L", RegistrationNumber = "REG005", ContactEmail = "info@ccb.com", ContactPhone = "9000090000", IsActive = true, CreatedAt = DateTime.UtcNow, AdminId = 5 },
                new Bank { BankId = 6, Code = "B006", Name = "Prime Finance Corp", Address = "47 Prime Blvd", PanNumber = "FFFTR2233N", RegistrationNumber = "REG006", ContactEmail = "contact@prime.com", ContactPhone = "8765432190", IsActive = true, CreatedAt = DateTime.UtcNow, AdminId = 6 },
                new Bank { BankId = 7, Code = "B007", Name = "Eco Savings Bank", Address = "12 Green St", PanNumber = "GGGTY8907M", RegistrationNumber = "REG007", ContactEmail = "eco@esb.com", ContactPhone = "9123456789", IsActive = true, CreatedAt = DateTime.UtcNow, AdminId = 7 },
                new Bank { BankId = 8, Code = "B008", Name = "Liberty Bank", Address = "7 Freedom Way", PanNumber = "HHHPL5678V", RegistrationNumber = "REG008", ContactEmail = "info@liberty.com", ContactPhone = "9234567890", IsActive = true, CreatedAt = DateTime.UtcNow, AdminId = 8 },
                new Bank { BankId = 9, Code = "B009", Name = "Heritage Finance", Address = "4 Heritage Square", PanNumber = "IIIWR3456C", RegistrationNumber = "REG009", ContactEmail = "support@heritage.com", ContactPhone = "9345678901", IsActive = true, CreatedAt = DateTime.UtcNow, AdminId = 9 },
                new Bank { BankId = 10, Code = "B010", Name = "Crescent Capital Bank", Address = "55 Moonlight Rd", PanNumber = "JJJER5647R", RegistrationNumber = "REG010", ContactEmail = "hello@crescent.com", ContactPhone = "9456789012", IsActive = true, CreatedAt = DateTime.UtcNow, AdminId = 10 }
            );

            // BANK USERS
            modelBuilder.Entity<BankUser>().HasData(
                new BankUser { BankUserId = 1, Code = "BU001", Name = "Emma Green", Email = "emma@fnb.com", Password = "123456", PhoneNumber = "9876543210", BankId = 1 },
                new BankUser { BankUserId = 2, Code = "BU002", Name = "Liam Gray", Email = "liam@gtb.com", Password = "123456", PhoneNumber = "8765432109", BankId = 2 },
                new BankUser { BankUserId = 3, Code = "BU003", Name = "Olivia White", Email = "olivia@mfb.com", Password = "123456", PhoneNumber = "7654321098", BankId = 3 },
                new BankUser { BankUserId = 4, Code = "BU004", Name = "Noah Black", Email = "noah@unity.com", Password = "123456", PhoneNumber = "6543210987", BankId = 4 },
                new BankUser { BankUserId = 5, Code = "BU005", Name = "Ava Brown", Email = "ava@ccb.com", Password = "123456", PhoneNumber = "5432109876", BankId = 5 },
                new BankUser { BankUserId = 6, Code = "BU006", Name = "William Scott", Email = "will@prime.com", Password = "123456", PhoneNumber = "4321098765", BankId = 6 },
                new BankUser { BankUserId = 7, Code = "BU007", Name = "Sophia Adams", Email = "sophia@eco.com", Password = "123456", PhoneNumber = "3210987654", BankId = 7 },
                new BankUser { BankUserId = 8, Code = "BU008", Name = "James Walker", Email = "james@liberty.com", Password = "123456", PhoneNumber = "2109876543", BankId = 8 },
                new BankUser { BankUserId = 9, Code = "BU009", Name = "Mia Turner", Email = "mia@heritage.com", Password = "123456", PhoneNumber = "1098765432", BankId = 9 },
                new BankUser { BankUserId = 10, Code = "BU010", Name = "Lucas Reed", Email = "lucas@crescent.com", Password = "123456", PhoneNumber = "9988776655", BankId = 10 }
            );

            // CLIENTS
            modelBuilder.Entity<Client>().HasData(
                new Client { ClientId = 1, Password = "client123", Code = "C001", Name = "TechWave Ltd", Email = "hr@techwave.com", BusinessType = "IT Services", Address = "12 Silicon Ave", VerificationStatus = "Verified", IsActive = true, CreatedAt = DateTime.UtcNow, BankId = 1, BankUserId = 1 },
                new Client { ClientId = 2, Password = "client123", Code = "C002", Name = "GreenFoods Inc", Email = "admin@greenfoods.com", BusinessType = "Food Manufacturing", Address = "45 Organic Rd", VerificationStatus = "Pending", IsActive = true, CreatedAt = DateTime.UtcNow, BankId = 2, BankUserId = 2 },
                new Client { ClientId = 3, Password = "client123", Code = "C003", Name = "Urban Builders", Email = "info@urbanbuilders.com", BusinessType = "Construction", Address = "78 Brick Lane", VerificationStatus = "Verified", IsActive = true, CreatedAt = DateTime.UtcNow, BankId = 3, BankUserId = 3 },
                new Client { ClientId = 4, Password = "client123", Code = "C004", Name = "BlueOcean Travels", Email = "info@blueocean.com", BusinessType = "Travel Agency", Address = "22 Sea View", VerificationStatus = "Verified", IsActive = true, CreatedAt = DateTime.UtcNow, BankId = 4, BankUserId = 4 },
                new Client { ClientId = 5, Password = "client123", Code = "C005", Name = "HealthPlus Pharma", Email = "contact@healthplus.com", BusinessType = "Pharmaceuticals", Address = "88 Medic Way", VerificationStatus = "Pending", IsActive = true, CreatedAt = DateTime.UtcNow, BankId = 5, BankUserId = 5 },
                new Client { ClientId = 6, Password = "client123", Code = "C006", Name = "BrightFuture EdTech", Email = "hello@brightfuture.com", BusinessType = "Education", Address = "91 Knowledge Park", VerificationStatus = "Verified", IsActive = true, CreatedAt = DateTime.UtcNow, BankId = 6, BankUserId = 6 },
                new Client { ClientId = 7, Password = "client123", Code = "C007", Name = "EcoFarm Pvt Ltd", Email = "info@ecofarm.com", BusinessType = "Agriculture", Address = "32 Green Fields", VerificationStatus = "Verified", IsActive = true, CreatedAt = DateTime.UtcNow, BankId = 7, BankUserId = 7 },
                new Client { ClientId = 8, Password = "client123", Code = "C008", Name = "Skyline Interiors", Email = "contact@skyline.com", BusinessType = "Interior Design", Address = "56 City Heights", VerificationStatus = "Pending", IsActive = true, CreatedAt = DateTime.UtcNow, BankId = 8, BankUserId = 8 },
                new Client { ClientId = 9, Password = "client123", Code = "C009", Name = "FinCore Analytics", Email = "admin@fincore.com", BusinessType = "Data Analytics", Address = "77 Data Dr", VerificationStatus = "Verified", IsActive = true, CreatedAt = DateTime.UtcNow, BankId = 9, BankUserId = 9 },
                new Client { ClientId = 10, Password = "client123", Code = "C010", Name = "Nova Retail", Email = "info@novaretail.com", BusinessType = "Retail Chain", Address = "19 Market Lane", VerificationStatus = "Pending", IsActive = true, CreatedAt = DateTime.UtcNow, BankId = 10, BankUserId = 10 }
            );

            // BENEFICIARIES
            modelBuilder.Entity<Beneficiary>().HasData(
                new Beneficiary { BeneficiaryId = 1, Name = "John Doe", AccountNumber = "1234567890", IfscCode = "FNB0001", ClientId = 1 },
                new Beneficiary { BeneficiaryId = 2, Name = "Mary Jane", AccountNumber = "2345678901", IfscCode = "GTB0002", ClientId = 2 },
                new Beneficiary { BeneficiaryId = 3, Name = "Peter Parker", AccountNumber = "3456789012", IfscCode = "MFB0003", ClientId = 3 },
                new Beneficiary { BeneficiaryId = 4, Name = "Tony Stark", AccountNumber = "4567890123", IfscCode = "UNITY0004", ClientId = 4 },
                new Beneficiary { BeneficiaryId = 5, Name = "Bruce Wayne", AccountNumber = "5678901234", IfscCode = "CCB0005", ClientId = 5 },
                new Beneficiary { BeneficiaryId = 6, Name = "Clark Kent", AccountNumber = "6789012345", IfscCode = "PRIME0006", ClientId = 6 },
                new Beneficiary { BeneficiaryId = 7, Name = "Diana Prince", AccountNumber = "7890123456", IfscCode = "ECO0007", ClientId = 7 },
                new Beneficiary { BeneficiaryId = 8, Name = "Barry Allen", AccountNumber = "8901234567", IfscCode = "LIB0008", ClientId = 8 },
                new Beneficiary { BeneficiaryId = 9, Name = "Hal Jordan", AccountNumber = "9012345678", IfscCode = "HER0009", ClientId = 9 },
                new Beneficiary { BeneficiaryId = 10, Name = "Arthur Curry", AccountNumber = "0123456789", IfscCode = "CRES00010", ClientId = 10 }
            );

            // PAYMENTS
            modelBuilder.Entity<Payment>().HasData(
                new Payment { PaymentId = 1, Amount = 10000, PaymentDate = DateTime.UtcNow, status = VerificationStatus.Verified, Type = PaymentType.NEFT, BeneficiaryId = 1, ClientId = 1, BankUserId = 1 },
                new Payment { PaymentId = 2, Amount = 25000, PaymentDate = DateTime.UtcNow, status = VerificationStatus.Verified, Type = PaymentType.RTGS, BeneficiaryId = 2, ClientId = 2, BankUserId = 2 },
                new Payment { PaymentId = 3, Amount = 5000, PaymentDate = DateTime.UtcNow, status = VerificationStatus.Pending, Type = PaymentType.IMPS, BeneficiaryId = 3, ClientId = 3, BankUserId = 3 },
                new Payment { PaymentId = 4, Amount = 40000, PaymentDate = DateTime.UtcNow, status = VerificationStatus.Verified, Type = PaymentType.RTGS, BeneficiaryId = 4, ClientId = 4, BankUserId = 4 },
                new Payment { PaymentId = 5, Amount = 12000, PaymentDate = DateTime.UtcNow, status = VerificationStatus.Rejected, Type = PaymentType.NEFT, BeneficiaryId = 5, ClientId = 5, BankUserId = 5 },
                new Payment { PaymentId = 6, Amount = 8700, PaymentDate = DateTime.UtcNow, status = VerificationStatus.Pending, Type = PaymentType.IMPS, BeneficiaryId = 6, ClientId = 6, BankUserId = 6 },
                new Payment { PaymentId = 7, Amount = 65500, PaymentDate = DateTime.UtcNow, status = VerificationStatus.Verified, Type = PaymentType.RTGS, BeneficiaryId = 7, ClientId = 7, BankUserId = 7 },
                new Payment { PaymentId = 8, Amount = 9200, PaymentDate = DateTime.UtcNow, status = VerificationStatus.Verified, Type = PaymentType.NEFT, BeneficiaryId = 8, ClientId = 8, BankUserId = 8 },
                new Payment { PaymentId = 9, Amount = 11100, PaymentDate = DateTime.UtcNow, status = VerificationStatus.Pending, Type = PaymentType.IMPS, BeneficiaryId = 9, ClientId = 9, BankUserId = 9 },
                new Payment { PaymentId = 10, Amount = 25000, PaymentDate = DateTime.UtcNow, status = VerificationStatus.Verified, Type = PaymentType.RTGS, BeneficiaryId = 10, ClientId = 10, BankUserId = 10 }
            );

            // DOCUMENTS
            modelBuilder.Entity<Document>().HasData(
                new Document { DocumentId = 1, Name = "PAN Proof", Url = "/docs/pan1.pdf", BankUserId = 1, ClientId = 1, UploadedAt = DateTime.UtcNow, DocType = "KYC" },
                new Document { DocumentId = 2, Name = "Registration Cert", Url = "/docs/reg2.pdf", BankUserId = 2, ClientId = 2, UploadedAt = DateTime.UtcNow, DocType = "Registration" },
                new Document { DocumentId = 3, Name = "Address Proof", Url = "/docs/address3.pdf", BankUserId = 3, ClientId = 3, UploadedAt = DateTime.UtcNow, DocType = "Address" },
                new Document { DocumentId = 4, Name = "License", Url = "/docs/license4.pdf", BankUserId = 4, ClientId = 4, UploadedAt = DateTime.UtcNow, DocType = "Business License" },
                new Document { DocumentId = 5, Name = "GST Certificate", Url = "/docs/gst5.pdf", BankUserId = 5, ClientId = 5, UploadedAt = DateTime.UtcNow, DocType = "Tax" },
                new Document { DocumentId = 6, Name = "Insurance Proof", Url = "/docs/ins6.pdf", BankUserId = 6, ClientId = 6, UploadedAt = DateTime.UtcNow, DocType = "Insurance" },
                new Document { DocumentId = 7, Name = "Office Lease", Url = "/docs/lease7.pdf", BankUserId = 7, ClientId = 7, UploadedAt = DateTime.UtcNow, DocType = "Property" },
                new Document { DocumentId = 8, Name = "PAN Proof", Url = "/docs/pan8.pdf", BankUserId = 8, ClientId = 8, UploadedAt = DateTime.UtcNow, DocType = "KYC" },
                new Document { DocumentId = 9, Name = "Audit Report", Url = "/docs/audit9.pdf", BankUserId = 9, ClientId = 9, UploadedAt = DateTime.UtcNow, DocType = "Financial" },
                new Document { DocumentId = 10, Name = "Tax Return", Url = "/docs/tax10.pdf", BankUserId = 10, ClientId = 10, UploadedAt = DateTime.UtcNow, DocType = "Tax" }
            );


            modelBuilder.Entity<Payment>().HasData(
    new Payment { PaymentId = 11, Amount = 8500, PaymentDate = new DateTime(2025, 2, 10, 10, 30, 0, DateTimeKind.Utc), status = VerificationStatus.Verified, Type = PaymentType.IMPS, BeneficiaryId = 1, ClientId = 1, BankUserId = 1 },
    new Payment { PaymentId = 12, Amount = 15200, PaymentDate = new DateTime(2025, 2, 11, 11, 15, 0, DateTimeKind.Utc), status = VerificationStatus.Verified, Type = PaymentType.NEFT, BeneficiaryId = 1, ClientId = 1, BankUserId = 11},
    new Payment { PaymentId = 13, Amount = 23000, PaymentDate = new DateTime(2025, 2, 12, 9, 0, 0, DateTimeKind.Utc), status = VerificationStatus.Pending, Type = PaymentType.RTGS, BeneficiaryId = 1, ClientId = 1, BankUserId = 1 },
    new Payment { PaymentId = 14, Amount = 4200, PaymentDate = new DateTime(2025, 2, 12, 13, 45, 0, DateTimeKind.Utc), status = VerificationStatus.Rejected, Type = PaymentType.IMPS, BeneficiaryId = 1, ClientId = 1, BankUserId = 1 },
    new Payment { PaymentId = 15, Amount = 12500, PaymentDate = new DateTime(2025, 2, 13, 8, 20, 0, DateTimeKind.Utc), status = VerificationStatus.Verified, Type = PaymentType.NEFT, BeneficiaryId = 1, ClientId = 1, BankUserId = 1 },
    new Payment { PaymentId = 16, Amount = 9900, PaymentDate = new DateTime(2025, 2, 13, 15, 10, 0, DateTimeKind.Utc), status = VerificationStatus.Pending, Type = PaymentType.IMPS, BeneficiaryId = 1, ClientId = 1, BankUserId = 11 },
    new Payment { PaymentId = 17, Amount = 30500, PaymentDate = new DateTime(2025, 2, 14, 10, 5, 0, DateTimeKind.Utc), status = VerificationStatus.Verified, Type = PaymentType.RTGS, BeneficiaryId = 1, ClientId = 1, BankUserId = 1 },
    new Payment { PaymentId = 18, Amount = 17800, PaymentDate = new DateTime(2025, 2, 15, 9, 40, 0, DateTimeKind.Utc), status = VerificationStatus.Verified, Type = PaymentType.NEFT, BeneficiaryId = 1, ClientId = 1, BankUserId = 1 },
    new Payment { PaymentId = 19, Amount = 6200, PaymentDate = new DateTime(2025, 2, 15, 17, 30, 0, DateTimeKind.Utc), status = VerificationStatus.Pending, Type = PaymentType.IMPS, BeneficiaryId = 1, ClientId = 1, BankUserId = 1 },
    new Payment { PaymentId = 20, Amount = 28500, PaymentDate = new DateTime(2025, 2, 16, 12, 0, 0, DateTimeKind.Utc), status = VerificationStatus.Verified, Type = PaymentType.RTGS, BeneficiaryId = 1, ClientId = 1, BankUserId = 2 }
);


            // BankUser -> Client relationship
            // Explicitly define Client ↔ BankUser relationships


            // ApprovedByBankUser relationship
            modelBuilder.Entity<Client>()
                .HasOne(c => c.ApprovedByBankUser)
                .WithMany()
                .HasForeignKey(c => c.ApprovedBy)
                .OnDelete(DeleteBehavior.NoAction);

            // Legacy BankUser relationship
            modelBuilder.Entity<Client>()
                .HasOne(c => c.BankUser)
                .WithMany(bu => bu.Clients)
                .HasForeignKey(c => c.BankUserId)
                .OnDelete(DeleteBehavior.NoAction);


            base.OnModelCreating(modelBuilder);
        }
    }
}
