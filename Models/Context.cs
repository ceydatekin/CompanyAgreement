using Microsoft.EntityFrameworkCore;

namespace CompanyAgreement.Models
{
    public class Context : DbContext
    {
        //protected void OnConfiguring(DbContextOptions optionsBuilder)
        //{

        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                "Server=DESKTOP-JLIGHCN;Database=CompanyAggrementContext;Trusted_Connection=True;MultipleActiveResultSets=true"); //for local use
            }
        }
        public DbSet<Company> Companies { get; set; }
        public DbSet<AdminLogin> AdminLogins { get; set; }
        public DbSet<CompanyAuthority> CompanyAuthorities { get; set; }
        public DbSet<CompanyDepartment> CompanyDepartments { get; set; }
        public DbSet<CompanyInformation> CompanyInformation { get; set; }
        public DbSet<CompanyLogin> CompanyLogin { get; set; }
        public DbSet<ContractInformation> ContractInformation { get; set; }
        public DbSet<ContractSituation> ContractSituation { get; set; }

        public Context(DbContextOptions<Context> options)
       : base(options) { }

        public Context()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompanyDepartment>().HasKey(sc => new { sc.DepartmentId, sc.CompanyId });
        }

    }
}
