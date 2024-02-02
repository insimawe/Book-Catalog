using BookCatalog.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.API.Data
{
    public class BookCatalogDbContext : DbContext
    {
        public BookCatalogDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {
            
        }

        // properties
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for Category
            var categories = new List<Category>() 
            {
                new Category()
                {
                    Id = Guid.Parse("b4b92093-03cd-4675-a7e0-1cfd76d0f321"),
                    Name = "Fantasy"
                },
                new Category()
                {
                    Id = Guid.Parse("9b37b6fb-b3b2-49ec-b359-1d913be0d822"),
                    Name = "Horror"
                },
                new Category()
                {
                    Id = Guid.Parse("36151a6b-ba6f-45d4-9985-80a579724d46"),
                    Name = "Romance"
                }
            };

            // seed categories to the database
            modelBuilder.Entity<Category>().HasData(categories);
        }
    }
}


