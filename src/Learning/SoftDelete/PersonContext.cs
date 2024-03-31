using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SoftDelete
{
    public class PersonContext:DbContext
    {
        public DbSet<Person> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SoftDelete;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure soft delete filter for Student
            modelBuilder.Entity<Person>().HasQueryFilter(p => !p.IsDeleted);
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<Person>())
            {
                if (entry.State == EntityState.Deleted)
                {
                    // Override removal to set soft delete properties
                    entry.State = EntityState.Modified;
                    entry.Property(nameof(Person.IsDeleted)).CurrentValue = true;
                }
            }
            return base.SaveChanges();
        }
    }
}
