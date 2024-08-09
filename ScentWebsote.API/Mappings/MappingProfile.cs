using AutoMapper;
using ScentWebsote.API.DTOs;
using ScentWebsote.API.Models;

namespace ScentWebsote.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<Category,CategoryDto>();
        }
    }
}
