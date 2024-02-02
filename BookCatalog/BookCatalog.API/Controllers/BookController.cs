using BookCatalog.API.Data;
using BookCatalog.API.Models.Domain;
using BookCatalog.API.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using AutoMapper;
using BookCatalog.API.Repositories;

namespace BookCatalog.API.Controllers
{
    // https://localhost:portnumber/api/Book
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookCatalogDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IBookRepository bookRepository;

        // constructor
        public BookController(
            BookCatalogDbContext dbContext, 
            IMapper mapper, 
            IBookRepository bookRepository
        )
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.bookRepository = bookRepository;
        }


        // GET: https://localhost:portnumber/api/book
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            //Get Data From Database - Domain Models
            var booksDomainModel = await bookRepository.GetAllAsync();

            //Map Domain Models to DTOs return DTOs
            return Ok(mapper.Map<List<BookDto>>(booksDomainModel));
        }

        // GET SINGLE CATEGORY BY ID
        // GET: https://localhost:portnumber/api/book/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetBookById([FromRoute] Guid id)
        {
            // Get Data from Database - Domain Models
            var bookDomain = await bookRepository.GetByIdAsync(id);

            if (bookDomain == null)
            {
                return NotFound();
            }

            // Map Domain Model to DTo and return DTOS
            return Ok(mapper.Map<BookDto>(bookDomain));
        }

        // POST To Create New Book
        // POST: https://localhost:portnumber/api/books
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddBookRequestDto addBookRequestDto)
        {
            // Map DTO to Domain Model
            var bookDomainModel = mapper.Map<Book>(addBookRequestDto);

            await bookRepository.CreateAsync(bookDomainModel);

            //Map domain model back to DTO --to show information
            return Ok(mapper.Map<BookDto>(bookDomainModel));
        }

        // Update Book by Id
        // Put  https://localhost:portnumber/api/books/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateBookRequestDto updateBookRequestDto)
        {
            // Map DTO to Domain Model
            var bookDomainModel = mapper.Map<Book>(updateBookRequestDto);

            bookDomainModel = await bookRepository.UpdateAsync(id, bookDomainModel);

            if(bookDomainModel == null)
            {
                return NotFound();
            }

            //Map domain model back to DTO --to show information
            return Ok(mapper.Map<BookDto>(bookDomainModel));
        }

        // Update Book by Id
        // Put  https://localhost:portnumber/api/books/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            // Map DTO to Domain Model
            var bookDomainModel  = await bookRepository.DeleteAsync(id);

            if (bookDomainModel == null)
            {
                return NotFound();
            }

            //Map domain model back to DTO --to show information
            return Ok(mapper.Map<BookDto>(bookDomainModel));
        }
    }
}
