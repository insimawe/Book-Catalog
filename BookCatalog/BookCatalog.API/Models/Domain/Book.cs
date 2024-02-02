namespace BookCatalog.API.Models.Domain
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishDateUtc { get; set; }

        // relationship to Category
        public Guid CategoryId { get; set; }

        // Navigation properties
        public Category Category { get; set; }
    }
}
