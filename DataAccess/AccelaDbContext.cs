using Accela.DataAccess.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Accela.DataAccess
{
    public class AccelaDbContext : DbContext
    {
        public AccelaDbContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;AttachDbFileName=C:\Accela\DataAccess\Database\AccelaLocalDb.mdf;Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Property Configurations
            modelBuilder.Entity<Person>()
            .HasKey(pk => pk.PersonId);

            modelBuilder.Entity<Person>()
            .Property(s => s.FirstName)
            .HasMaxLength(50)
            .IsRequired();

            modelBuilder.Entity<Person>()
            .Property(s => s.LastName)
            .HasMaxLength(50)
            .IsRequired();
        }

        //entities
        public DbSet<Person> People { get; set; }
    }
}
