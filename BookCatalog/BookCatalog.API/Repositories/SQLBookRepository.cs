using BookCatalog.API.Data;
using BookCatalog.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.API.Repositories
{
    public class SQLBookRepository : IBookRepository
    {
        private readonly BookCatalogDbContext dbContext;
        public SQLBookRepository(BookCatalogDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await dbContext.Books.Include("Category").ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(Guid id)
        {
            return await dbContext.Books
                .Include("Category").
                FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Book> CreateAsync(Book bk)
        {
            await dbContext.Books.AddAsync(bk);
            await dbContext.SaveChangesAsync();
            return bk;
        }

        public async Task<Book?> UpdateAsync(Guid id, Book bk)
        {
            var existingBook = await dbContext.Books.FirstOrDefaultAsync(x => x.Id == id);

            if(existingBook == null)
            {
                return null;
            }

            existingBook.Title = bk.Title;
            existingBook.Description = bk.Description;
            existingBook.PublishDateUtc = bk.PublishDateUtc;
            existingBook.CategoryId = bk.CategoryId;
            await dbContext.SaveChangesAsync();

            return existingBook;
        }
        public async Task<Book?> DeleteAsync(Guid id)
        {
            var existingBook = await dbContext.Books.FirstOrDefaultAsync(x => x.Id == id);

            if(existingBook == null)
            {
                return null;
            }

            dbContext.Books.Remove(existingBook);
            await dbContext.SaveChangesAsync();
            return existingBook;
        }
    }
}
