using DocumentFormat.OpenXml.Spreadsheet;
using Banking_Payments.Models;
using Microsoft.EntityFrameworkCore;

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
                .WithMany(c => c.Beneficiaries)
                .HasForeignKey(b => b.ClientId)
                .OnDelete(DeleteBehavior.NoAction);

            // Payment -> Beneficiary relationship (RESTRICT to break cascade cycle)
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Beneficiary)
                .WithMany(b => b.Payments)
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

            // Seed Data
            modelBuilder.Entity<Admin>().HasData(
                new Admin { AdminId = 1, Code = "ADM001", Name = "Alice Johnson", Email = "alice@banking.com", Password = "Pass@123" },
                new Admin { AdminId = 2, Code = "ADM002", Name = "Bob Smith", Email = "bob@banking.com", Password = "Pass@123" },
                new Admin { AdminId = 3, Code = "ADM003", Name = "Charlie Brown", Email = "charlie@banking.com", Password = "Pass@123" }
            );

            modelBuilder.Entity<Bank>().HasData(
                new Bank { BankId = 1, Code = "B001", Name = "First National Bank", Address = "123 Finance St", PanNumber = "AAAPL1234C", RegistrationNumber = "REG001", ContactEmail = "info@fnb.com", ContactPhone = "1234567890", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), AdminId = 1 },
                new Bank { BankId = 2, Code = "B002", Name = "Global Trust Bank", Address = "456 Trust Ave", PanNumber = "BBBTY4567P", RegistrationNumber = "REG002", ContactEmail = "contact@gtb.com", ContactPhone = "9876543210", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), AdminId = 2 },
                new Bank { BankId = 3, Code = "B003", Name = "Metro Finance Bank", Address = "789 Metro Rd", PanNumber = "CCCXY7890K", RegistrationNumber = "REG003", ContactEmail = "support@mfb.com", ContactPhone = "5647382910", IsActive = true, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), AdminId = 3 }
            );

            modelBuilder.Entity<BankUser>().HasData(
                new BankUser { BankUserId = 1, Code = "BU001", Name = "Emma Green", Email = "emma@fnb.com", Password = "123456", PhoneNumber = "9876543210", BankId = 1 },
                new BankUser { BankUserId = 2, Code = "BU002", Name = "Liam Gray", Email = "liam@gtb.com", Password = "123456", PhoneNumber = "8765432109", BankId = 2 },
                new BankUser { BankUserId = 3, Code = "BU003", Name = "Olivia White", Email = "olivia@mfb.com", Password = "123456", PhoneNumber = "7654321098", BankId = 3 }
            );

            //modelBuilder.Entity<Client>().HasData(
            //    new Client { 
            //        ClientId = 1, 
            //        Code = "CL001",
            //        Name = "TechWave Ltd",
            //        Email = "hr@techwave.com",
            //        BusinessType = "IT Services",
            //        Address = "12 Silicon Ave",
            //        Password = "CL0001",
            //        RegisterationNumber = "GIABC2910C",
            //        VerificationStatus = "Verified",
            //        AccountNumber="0909015102029166",
            //        IfscCode="ICIC00192",
            //        Balance = 53203920.2,
            //        IsActive = true,
            //        BankId = 1,
            //        BankUserId = 1001
            //    }
            //    //new Client { 
            //    //    ClientId = 2, 
            //    //    Password = "CL0002", 
            //    //    Code = "C002", 
            //    //    Name = "GreenFoods Inc", 
            //    //    Email = "admin@greenfoods.com", 
            //    //    BusinessType = "Food Manufacturing", 
            //    //    Address = "45 Organic Rd", 
            //    //    VerificationStatus = "Pending", 
            //    //    IsActive = true, 
            //    //    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), 
            //    //    BankId = 2, 
            //    //    BankUserId = 2 
            //    //},
            //    //new Client { 
            //    //    ClientId = 3, 
            //    //    Password = "client123", 
            //    //    Code = "C003", 
            //    //    Name = "Urban Builders", 
            //    //    Email = "info@urbanbuilders.com", 
            //    //    BusinessType = "Construction", 
            //    //    Address = "78 Brick Lane", 
            //    //    VerificationStatus = "Verified", 
            //    //    IsActive = true, 
            //    //    CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), 
            //    //    BankId = 3, 
            //    //    BankUserId = 3 
            //    //}
            //);
            
            //modelBuilder.Entity<Beneficiary>().HasData(
            //    new Beneficiary { BeneficiaryId = 1, Name = "John Doe", AccountNumber = "1234567890", IfscCode = "FNB0001", ClientId = 1 },
            //    new Beneficiary { BeneficiaryId = 2, Name = "Mary Jane", AccountNumber = "2345678901", IfscCode = "GTB0002", ClientId = 2 },
            //    new Beneficiary { BeneficiaryId = 3, Name = "Peter Parker", AccountNumber = "3456789012", IfscCode = "MFB0003", ClientId = 3 }
            //);

            //modelBuilder.Entity<Employee>().HasData(
            //    new Employee { EmployeeId = 1, Name = "Sarah Wilson", Address = "101 Lakeview", Salary = 50000, AccountNumber = "9876543210", IfscCode = "FNB0001", ClientId = 1 },
            //    new Employee { EmployeeId = 2, Name = "David Lee", Address = "202 Hilltop", Salary = 45000, AccountNumber = "8765432109", IfscCode = "GTB0002", ClientId = 2 },
            //    new Employee { EmployeeId = 3, Name = "Sophia Brown", Address = "303 Park St", Salary = 55000, AccountNumber = "7654321098", IfscCode = "MFB0003", ClientId = 3 }
            //);

            //modelBuilder.Entity<Payment>().HasData(
            //    new Payment { PaymentId = 1, Amount = 10000, PaymentDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), status = VerificationStatus.Verified, Type = PaymentType.NEFT, BeneficiaryId = 1, ClientId = 1, BankUserId = 1 },
            //    new Payment { PaymentId = 2, Amount = 25000, PaymentDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), status = VerificationStatus.Verified, Type = PaymentType.RTGS, BeneficiaryId = 2, ClientId = 2, BankUserId = 2 },
            //    new Payment { PaymentId = 3, Amount = 5000, PaymentDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), status = VerificationStatus.Pending, Type = PaymentType.IMPS, BeneficiaryId = 3, ClientId = 3, BankUserId = 3 }
            //);

            //modelBuilder.Entity<SalaryDisbursement>().HasData(
            //    new SalaryDisbursement { SalaryDisbursementId = 1, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), Amount = 50000, EmployeeId = 1, ClientId = 1 },
            //    new SalaryDisbursement { SalaryDisbursementId = 2, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), Amount = 45000, EmployeeId = 2, ClientId = 2 },
            //    new SalaryDisbursement { SalaryDisbursementId = 3, CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc), Amount = 55000, EmployeeId = 3, ClientId = 3 }
            //);

            //modelBuilder.Entity<Document>().HasData(
            //    new Document
            //    {
            //        DocumentId = 1,
            //        Name = "PAN Proof",
            //        Url = "/docs/pan1.pdf",
            //        BankUserId = 1,
            //        ClientId = 1,
            //        UploadedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            //        DocType = "KYC"
            //    },
            //    new Document
            //    {
            //        DocumentId = 2,
            //        Name = "Registration Cert",
            //        Url = "/docs/reg2.pdf",
            //        BankUserId = 2,
            //        ClientId = 2,
            //        UploadedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            //        DocType = "Registration"
            //    },
            //    new Document
            //    {
            //        DocumentId = 3,
            //        Name = "Address Proof",
            //        Url = "/docs/address3.pdf",
            //        BankUserId = 3,
            //        ClientId = 3,
            //        UploadedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            //        DocType = "Address"
            //    }
            //);

            //modelBuilder.Entity<ContactDetails>().HasData(
            //    new ContactDetails { ContactDetailsId = 1, ContactName = "Tom Hardy", ContactNumber = "9998887771", ContactEmail = "tom@fnb.com", BankId = 1 },
            //    new ContactDetails { ContactDetailsId = 2, ContactName = "Anna Scott", ContactNumber = "9998887772", ContactEmail = "anna@gtb.com", BankId = 2 },
            //    new ContactDetails { ContactDetailsId = 3, ContactName = "Chris Evans", ContactNumber = "9998887773", ContactEmail = "chris@mfb.com", BankId = 3 }
            //);

            base.OnModelCreating(modelBuilder);
        }
    }
}
