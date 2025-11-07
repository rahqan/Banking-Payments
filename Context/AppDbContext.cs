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


            modelBuilder.Entity<Admin>().HasData(
       new Admin
       {
           AdminId = 1,
           Code = "ADM001",
           Name = "Super Admin",
           Email = "admin@bankingsys.com",
           Password = "admin123"
       }
   );

            // ✅ 10 Banks
            modelBuilder.Entity<Bank>().HasData(
                Enumerable.Range(1, 10).Select(i => new Bank
                {
                    BankId = i,
                    Code = $"BNK00{i}",
                    Name = $"Global Bank {i}",
                    Address = $"Headquarters Tower {i}, Connaught Place, New Delhi",
                    PanNumber = $"PANBANK{i}XYZ",
                    RegistrationNumber = $"REG{i}BANK2025",
                    ContactEmail = $"info{i}@globalbank.com",
                    ContactPhone = $"98765000{i}",
                    IsActive = true,
                    CreatedByAdminId = 1,
                    CreatedAt = DateTime.Now
                })
            );

            // ✅ Bank Users (more realistic staff)
            modelBuilder.Entity<BankUser>().HasData(
                new BankUser { BankUserId = 1, Code = "BU001", Name = "Alice Kumar", Email = "alice.kumar@globalbank1.com", Password = "alice123", PhoneNumber = "9000010001", BankId = 1 },
                new BankUser { BankUserId = 2, Code = "BU002", Name = "Ravi Singh", Email = "ravi.singh@globalbank1.com", Password = "ravi123", PhoneNumber = "9000010002", BankId = 1 },
                new BankUser { BankUserId = 3, Code = "BU003", Name = "Meena Das", Email = "meena.das@globalbank1.com", Password = "meena123", PhoneNumber = "9000010003", BankId = 1 },
                new BankUser { BankUserId = 4, Code = "BU004", Name = "Suresh Nair", Email = "suresh.nair@globalbank1.com", Password = "suresh123", PhoneNumber = "9000010004", BankId = 1 },
                new BankUser { BankUserId = 5, Code = "BU005", Name = "Tanya Joseph", Email = "tanya.joseph@globalbank1.com", Password = "tanya123", PhoneNumber = "9000010005", BankId = 1 },
                new BankUser { BankUserId = 6, Code = "BU006", Name = "Neeraj Chauhan", Email = "neeraj.chauhan@globalbank1.com", Password = "neeraj123", PhoneNumber = "9000010006", BankId = 1 },
                new BankUser { BankUserId = 7, Code = "BU007", Name = "Rohini Sharma", Email = "rohini.sharma@globalbank1.com", Password = "rohini123", PhoneNumber = "9000010007", BankId = 1 },
                new BankUser { BankUserId = 8, Code = "BU008", Name = "Irfan Malik", Email = "irfan.malik@globalbank1.com", Password = "irfan123", PhoneNumber = "9000010008", BankId = 1 }
            );

            // ✅ Clients (diverse, realistic)
            modelBuilder.Entity<Client>().HasData(
                new Client { ClientId = 1, Code = "CL001", Name = "TechNova Pvt Ltd", Email = "info@technova.com", BusinessType = "IT Services", Address = "Gurgaon, Haryana", Password = "tech123", BankId = 1, BankUserId = 1, Balance = 480000, VerificationStatus = "Verified", IsActive = true },
                new Client { ClientId = 2, Code = "CL002", Name = "GreenFoods Ltd", Email = "contact@greenfoods.com", BusinessType = "Food Supply Chain", Address = "Noida Sector 5", Password = "green123", BankId = 1, BankUserId = 2, Balance = 210000, VerificationStatus = "Verified", IsActive = true },
                new Client { ClientId = 3, Code = "CL003", Name = "EduSmart Foundation", Email = "hello@edusmart.org", BusinessType = "Educational NGO", Address = "Kolkata, West Bengal", Password = "edu123", BankId = 1, BankUserId = 3, Balance = 75000, VerificationStatus = "Pending", IsActive = true },
                new Client { ClientId = 4, Code = "CL004", Name = "AutoWorks India", Email = "sales@autoworks.com", BusinessType = "Automotive Parts", Address = "Pune, Maharashtra", Password = "auto123", BankId = 1, BankUserId = 4, Balance = 325000, VerificationStatus = "Verified", IsActive = true },
                new Client { ClientId = 5, Code = "CL005", Name = "MedCare Diagnostics", Email = "admin@medcare.com", BusinessType = "Healthcare", Address = "Hyderabad, Telangana", Password = "med123", BankId = 1, BankUserId = 5, Balance = 455000, VerificationStatus = "Verified", IsActive = true },
                new Client { ClientId = 6, Code = "CL006", Name = "QuickMart Retail", Email = "support@quickmart.in", BusinessType = "Retail Chain", Address = "Bangalore, Karnataka", Password = "mart123", BankId = 1, BankUserId = 6, Balance = 180000, VerificationStatus = "Verified", IsActive = true },
                new Client { ClientId = 7, Code = "CL007", Name = "UrbanSpaces Realty", Email = "contact@urbanspaces.in", BusinessType = "Real Estate", Address = "Mumbai, Maharashtra", Password = "urban123", BankId = 1, BankUserId = 7, Balance = 650000, VerificationStatus = "Verified", IsActive = true },
                new Client { ClientId = 8, Code = "CL008", Name = "BrightWare Solutions", Email = "hr@brightware.com", BusinessType = "Software Services", Address = "Chennai, Tamil Nadu", Password = "bright123", BankId = 1, BankUserId = 8, Balance = 275000, VerificationStatus = "Pending", IsActive = true }
            );

            // ✅ Beneficiaries (2-3 per client)
            modelBuilder.Entity<Beneficiary>().HasData(
                // TechNova
                new Beneficiary { BeneficiaryId = 1, Name = "Rohit Sharma", AccountNumber = "1212121212", IfscCode = "GBNK0010001", BankName = "Global Bank 1", RelationShip = "Supplier", ClientId = 1 },
                new Beneficiary { BeneficiaryId = 2, Name = "Sneha Gupta", AccountNumber = "1313131313", IfscCode = "GBNK0010002", BankName = "Global Bank 1", RelationShip = "Contractor", ClientId = 1 },

                // GreenFoods
                new Beneficiary { BeneficiaryId = 3, Name = "Vikram Iyer", AccountNumber = "1414141414", IfscCode = "GBNK0010003", BankName = "Global Bank 1", RelationShip = "Farmer", ClientId = 2 },
                new Beneficiary { BeneficiaryId = 4, Name = "Megha Tiwari", AccountNumber = "1515151515", IfscCode = "GBNK0010004", BankName = "Global Bank 1", RelationShip = "Vendor", ClientId = 2 },

                // EduSmart
                new Beneficiary { BeneficiaryId = 5, Name = "Aditi Bose", AccountNumber = "1616161616", IfscCode = "GBNK0010005", BankName = "Global Bank 1", RelationShip = "Teacher", ClientId = 3 },

                // AutoWorks
                new Beneficiary { BeneficiaryId = 6, Name = "Manish Goel", AccountNumber = "1717171717", IfscCode = "GBNK0010006", BankName = "Global Bank 1", RelationShip = "Supplier", ClientId = 4 },
                new Beneficiary { BeneficiaryId = 7, Name = "Rahul Prasad", AccountNumber = "1818181818", IfscCode = "GBNK0010007", BankName = "Global Bank 1", RelationShip = "Consultant", ClientId = 4 },

                // MedCare
                new Beneficiary { BeneficiaryId = 8, Name = "Priya Nair", AccountNumber = "1919191919", IfscCode = "GBNK0010008", BankName = "Global Bank 1", RelationShip = "Doctor", ClientId = 5 },
                new Beneficiary { BeneficiaryId = 9, Name = "Dr. Arvind Rao", AccountNumber = "2020202020", IfscCode = "GBNK0010009", BankName = "Global Bank 1", RelationShip = "Supplier", ClientId = 5 }
            );

            // ✅ Payments (multiple recent transactions)
            modelBuilder.Entity<Payment>().HasData(
                new Payment { PaymentId = 1, Amount = 12500, PaymentDate = DateTime.Now.AddDays(-10), Type = PaymentType.NEFT, status = Banking_Payments.Models.Enums.VerificationStatus.Rejected, Remarks = "Monthly IT Contract", ClientId = 1, BeneficiaryId = 1, BankUserId = 1, RejectionRemark="KYC required" },
                new Payment { PaymentId = 2, Amount = 7500, PaymentDate = DateTime.Now.AddDays(-5), Type = PaymentType.IMPS, status = Banking_Payments.Models.Enums.VerificationStatus.Pending, Remarks = "Hardware Purchase", ClientId = 1, BeneficiaryId = 2, BankUserId = 1 },
                new Payment { PaymentId = 3, Amount = 34000, PaymentDate = DateTime.Now.AddDays(-7), Type = PaymentType.RTGS, status = Banking_Payments.Models.Enums.VerificationStatus.Verified, Remarks = "Raw Material Payment", ClientId = 2, BeneficiaryId = 3, BankUserId = 2 },
                new Payment { PaymentId = 4, Amount = 15000, PaymentDate = DateTime.Now.AddDays(-2), Type = PaymentType.NEFT, status = Banking_Payments.Models.Enums.VerificationStatus.Verified, Remarks = "Consultant Fees", ClientId = 4, BeneficiaryId = 7, BankUserId = 4 },
                new Payment { PaymentId = 5, Amount = 9800, PaymentDate = DateTime.Now.AddDays(-1), Type = PaymentType.IMPS, status = Banking_Payments.Models.Enums.VerificationStatus.Pending, Remarks = "Doctor Honorarium", ClientId = 5, BeneficiaryId = 9, BankUserId = 5 },
                new Payment { PaymentId = 6, Amount = 18500, PaymentDate = DateTime.Now.AddDays(-3), Type = PaymentType.RTGS, status = Banking_Payments.Models.Enums.VerificationStatus.Verified, Remarks = "Supplier Settlement", ClientId = 5, BeneficiaryId = 9, BankUserId = 5 },
                new Payment { PaymentId = 7, Amount = 5600, PaymentDate = DateTime.Now.AddDays(-4), Type = PaymentType.NEFT, status = Banking_Payments.Models.Enums.VerificationStatus.Verified, Remarks = "Teacher Stipend", ClientId = 3, BeneficiaryId = 5, BankUserId = 3 }
            );


            base.OnModelCreating(modelBuilder);
        }
    }
}
