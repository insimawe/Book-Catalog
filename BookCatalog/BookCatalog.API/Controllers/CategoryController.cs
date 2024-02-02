using BookCatalog.API.Data;
using BookCatalog.API.Models.Domain;
using BookCatalog.API.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly BookCatalogDbContext dbContext;
        // constructor
        public CategoryController(BookCatalogDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            
            //Get Data From Database - Domain Models
            var categories = dbContext.Categories.ToList();

            //Map Domain Models to DTOs
            var categoriesDto = new List<CategoryDto>();
            foreach (var category in categories)
            {
                categoriesDto.Add(new CategoryDto()
                {
                    Id = category.Id,
                    Name = category.Name
                });
            }
            // return DTOs
            return Ok(categoriesDto);
        }

        // GET SINGLE CATEGORY BY ID
        // GET: https://localhost:portnumber/api/category/{id}
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetCategoryById([FromRoute] Guid id)
        {
            var category = dbContext.Categories.FirstOrDefault(x => x.Id == id);
            
            if (category == null) 
            {
                return NotFound();
            }

            // Map Domain Model to DTo
            var categorysDto = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };

            return Ok(category);
        }

        // POST To Create New Book
        // POST: https://localhost:portnumber/api/books
        [HttpPost]
        public IActionResult Create([FromBody] AddCategoryRequestDto addCategoryRequestDto)
        {
            // Map DTO to Domain Model
            var categoryDomainModel = new Category
            {
                Name = addCategoryRequestDto.Name
            };

            // Use Domain Model to create Book
            dbContext.Categories.Add(categoryDomainModel);
            dbContext.SaveChanges();

            //Map domain model back to DTO --to show information
            var categoryDto = new CategoryDto
            {
                Id = categoryDomainModel.Id,
                Name = categoryDomainModel.Name
            };

            return CreatedAtAction(nameof(GetCategoryById), new { id = categoryDomainModel.Id }, categoryDto);
        }

        // Put To Update New Book
        // Put: https://localhost:portnumber/api/books/{id}
        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateCategoryRequestDto updateCategoryRequestDto)
        {
            // Map DTO to Domain Model
            var categoryDomainModel = new Category
            {
                Id = id,
                Name = updateCategoryRequestDto.Name
            };

            // Use Domain Model to update Book
            var existingCategory = dbContext.Categories.Where(x => x.Id == id).FirstOrDefault();

            if (existingCategory == null)
            {
                return NotFound();
            }

            existingCategory.Name = categoryDomainModel.Name;
            dbContext.SaveChanges();

            //Map domain model back to DTO --to show information
            var categoryDto = new CategoryDto
            {
                Id = categoryDomainModel.Id,
                Name = categoryDomainModel.Name
            };

            return CreatedAtAction(nameof(GetCategoryById), new { id = categoryDomainModel.Id }, categoryDto);
        }

        // Delete To Update New Book
        // Delete: https://localhost:portnumber/api/books/{id}
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delet([FromRoute] Guid id)
        {
            // Map DTO to Domain Model
            var categoryDomainModel = new Category
            {
                Id = id
            };

            // Use Domain Model to update Book
            var existingCategory = dbContext.Categories.Where(x => x.Id == id).FirstOrDefault();

            if (existingCategory == null)
            {
                return NotFound();
            }

            dbContext.Categories.Remove(existingCategory);
            dbContext.SaveChanges();

            //Map domain model back to DTO --to show information
            var categoryDto = new CategoryDto
            {
                Id = categoryDomainModel.Id
            };

            return CreatedAtAction(nameof(GetCategoryById), new { id = categoryDomainModel.Id }, categoryDto);
        }
    }
}
