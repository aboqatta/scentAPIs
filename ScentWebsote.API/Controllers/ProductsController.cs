using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ScentWebsote.API.Data;
using ScentWebsote.API.Data.Interfaces;
using ScentWebsote.API.DTOs;
using ScentWebsote.API.Models;

namespace ScentWebsote.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            var products = _repository.GetProducts();
            if (products == null || !products.Any())
            {
                return NotFound("No products found.");
            }
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Ok(productDtos);
        }

        [HttpGet("category/{categoryId}")]
        public ActionResult<IEnumerable<Product>> GetProductsByCategory(int categoryId)
        {
            var products = _repository.GetProducts().Where(p => p.CategoryId == categoryId).ToList();
            if (products == null || !products.Any())
            {
                return NotFound($"No products found for category ID {categoryId}.");
            }
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Ok(productDtos);
        }

       
    }

}
