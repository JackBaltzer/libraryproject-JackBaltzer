using LibraryProject.Api.Database.Entites;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.Api.Database
{
    public class LibraryProjectContext : DbContext
    {
        public LibraryProjectContext(DbContextOptions<LibraryProjectContext> options) : base(options) { }

        public DbSet<Author> Author { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(
                new()
                {
                    Id = 1,
                    FirstName = "George",
                    LastName = "Martin",
                    MiddleName = "R.R.",
                    BirthYear = 1948
                },
                new()
                {
                    Id = 2,
                    FirstName = "Lewis",
                    LastName = "Carol",
                    MiddleName = "",
                    BirthYear = 1832,
                    YearOfDeath = 1898
                }
            );
        }
    }
}
