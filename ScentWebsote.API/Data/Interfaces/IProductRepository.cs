using ScentWebsote.API.Models;

namespace ScentWebsote.API.Data
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
   
    }
}
