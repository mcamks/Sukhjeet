using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace BlueSignalCore.Models
{
    public class BlueSignalContext : DbContext
    {
        public BlueSignalContext() : base("name=BluSignalsEntities")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer<BlueSignalContext>(null);
            //var type = typeof(System.Data.Entity.SqlServer.SqlProviderServices);
            //((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 300;
            //Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Users>().ToTable("Users");
            modelBuilder.Entity<MarketData>().ToTable("MarketData");
            modelBuilder.Entity<MarketCategory>().ToTable("MarketCategory");
            modelBuilder.Entity<ContactLog>().ToTable("ContactLog");
            modelBuilder.Entity<EmailTemplate>().ToTable("EmailTemplate");
            modelBuilder.Entity<ProductType>().ToTable("ProductType");
            modelBuilder.Entity<UserChartHistory>().ToTable("UserChartHistory");
            modelBuilder.Entity<GlobalCodes>().ToTable("GlobalCodes");

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<MarketData> MarketData { get; set; }
        public DbSet<MarketCategory> MarketCategory { get; set; }
        public DbSet<ContactLog> ContactLog { get; set; }
        public DbSet<EmailTemplate> EmailTemplate { get; set; }
        public DbSet<ProductType> ProductType { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<GlobalCodes> GlobalCodes { get; set; }
        public DbSet<UserChartHistory> UserChartHistory { get; set; }
    }
}
