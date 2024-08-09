using ScentWebsote.API.Models;

namespace ScentWebsote.API.Data.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategories();
    }
}
