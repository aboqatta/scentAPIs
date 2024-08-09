using Microsoft.EntityFrameworkCore;
using ScentWebsote.API.Data.Interfaces;
using ScentWebsote.API.Models;

namespace ScentWebsote.API.Data
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;

        public CartRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CartItem> AddToCartAsync(int productId)
    {
        var cartItem = await _context.CartItems
            .FirstOrDefaultAsync(ci => ci.ProductId == productId);

        if (cartItem != null)
        {
            // Product already in cart, increase quantity
            cartItem.Quantity++;
        }
        else
        {
            // Product not in cart, add new item
            cartItem = new CartItem
            {
                ProductId = productId,
                Quantity = 1
            };

            await _context.CartItems.AddAsync(cartItem);
        }

        await _context.SaveChangesAsync();
        return cartItem;
    }

        public async Task<CartItem> RemoveFromCartAsync(int productId)
        {
            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(c => c.ProductId == productId);

            if (cartItem == null)
            {
                return null; // Product not found in cart
            }

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
            return cartItem;
        }

        public async Task<CartItem> IncreaseQuantityAsync(int productId)
        {
            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(c => c.ProductId == productId);

            if (cartItem == null)
            {
                return null; // Product not found in cart
            }

            cartItem.Quantity++;
            await _context.SaveChangesAsync();
            return cartItem;
        }

        public async Task<CartItem> DecreaseQuantityAsync(int productId)
        {
            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(c => c.ProductId == productId);

            if (cartItem == null)
            {
                return null; // Product not found in cart
            }

            if (cartItem.Quantity > 1)
            {
                cartItem.Quantity--;
                await _context.SaveChangesAsync();
                return cartItem;
            }

            return null; // Quantity cannot be less than 1
        }

        public async Task<IEnumerable<CartItem>> GetCartItemsAsync()
        {
            return await _context.CartItems.ToListAsync();
        }
    }
}
