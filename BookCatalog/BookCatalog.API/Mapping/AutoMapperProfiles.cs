using AutoMapper;
using BookCatalog.API.Models.Domain;
using BookCatalog.API.Models.DTO;

namespace BookCatalog.API.Mapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            //Create
            CreateMap<AddBookRequestDto, Book>().ReverseMap();
            //List
            CreateMap<Book, BookDto>().ReverseMap();
            //Update
            CreateMap<UpdateBookRequestDto, Book>().ReverseMap();
        } 
    }
}
