using ScentWebsote.API.Models;

namespace ScentWebsote.API.Data.Interfaces
{
    public interface ICartRepository
    {

        Task<IEnumerable<CartItem>> GetCartItemsAsync();
        Task<CartItem> AddToCartAsync(int productId);
        Task<CartItem> RemoveFromCartAsync(int productId);
        Task<CartItem> IncreaseQuantityAsync(int productId);
        Task<CartItem> DecreaseQuantityAsync(int productId);

    }
}
