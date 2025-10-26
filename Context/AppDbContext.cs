
using dummy_api.Models;
using Microsoft.EntityFrameworkCore;
using dummy_api.Models;

namespace dummy_api.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() { }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public virtual DbSet<Bank> Banks { get; set; }
        //public virtual DbSet<Beneficiary> Beneficiaries { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        //public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<SalaryDisbursement> SalaryDisbursements { get; set; }
        public virtual DbSet<BankUser> BankUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasOne(c => c.Bank)
                .WithMany(b => b.Clients)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Client)
                .WithMany(c => c.Payments)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SalaryDisbursement>()
                .HasOne(e => e.Employee)
                .WithMany(s => s.SalaryDisbursements)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
