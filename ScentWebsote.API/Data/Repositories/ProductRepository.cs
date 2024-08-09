using Microsoft.EntityFrameworkCore;
using ScentWebsote.API.Data.Interfaces;
using ScentWebsote.API.Models;

namespace ScentWebsote.API.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetProducts()
        {
            return _context.Products.Include(p => p.Category).ToList();
        }
    }
}
