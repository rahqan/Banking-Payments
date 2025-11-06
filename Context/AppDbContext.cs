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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Salary)
                .HasPrecision(18, 2);

            modelBuilder.Entity<SalaryDisbursement>()
                .Property(s => s.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Client>()
                .HasOne(c => c.Bank)
                .WithMany(b => b.Clients)
                .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Client)
                .WithMany(c => c.Payments)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Beneficiary>()
                .HasOne(b => b.Client)
                .WithMany(c => c.Beneficiaries)
                .HasForeignKey(b => b.ClientId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Beneficiary)
                .WithMany(b => b.Payments)
                .HasForeignKey(p => p.BeneficiaryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SalaryDisbursement>()
                .HasOne(e => e.Employee)
                .WithMany(s => s.SalaryDisbursements)
                .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<Document>()
                .HasOne(c => c.Client)
                .WithMany(d => d.Documents)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Client>()
                .HasOne(c => c.BankUser)
                .WithMany(bu => bu.Clients)
                .HasForeignKey(c => c.BankUserId)
                .OnDelete(DeleteBehavior.NoAction);


            base.OnModelCreating(modelBuilder);
        }
    }
}
