using BookCatalog.API.Models.Domain;

namespace BookCatalog.API.Models.DTO
{
    public class AddBookRequestDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishDateUtc { get; set; }

        // relationship to Category
        public Guid CategoryId { get; set; }

    }
}
