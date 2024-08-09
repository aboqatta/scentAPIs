using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ScentWebsote.API.Data.Interfaces;
using ScentWebsote.API.DTOs;
using ScentWebsote.API.Models;

namespace ScentWebsote.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetCategories()
        {
            var categories = _repository.GetCategories();
            if (categories == null || !categories.Any())
            {
                return NotFound("No categories found.");
            }
            var categoryDtos = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            return Ok(categoryDtos);
        }
    }
}