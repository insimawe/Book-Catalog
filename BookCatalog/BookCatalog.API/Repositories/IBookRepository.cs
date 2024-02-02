
using BookCatalog.API.Models.Domain;

namespace BookCatalog.API.Repositories
{
    public interface IBookRepository
    {
        Task<Book> CreateAsync(Book bk);
        Task<List<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(Guid id);
        Task<Book?> UpdateAsync(Guid id, Book bk);
        Task<Book?> DeleteAsync(Guid id);
    }
}
