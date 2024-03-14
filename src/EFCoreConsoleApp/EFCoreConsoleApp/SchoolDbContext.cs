using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EFCoreConsoleApp
{
    public class SchoolDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        IConfiguration appConfig;
        public SchoolDbContext(IConfiguration config)
        {
            appConfig = config;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(appConfig.GetConnectionString("SqlServer"));
        }
    }
}
