using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScentWebsote.API.Data;
using ScentWebsote.API.Data.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace ScentWebsote.API.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        private readonly AppDbContext _context;

        public CartController(ICartRepository cartRepository, AppDbContext context)
        {
            _cartRepository = cartRepository;
            _context = context;
        }

        [HttpPost("addtocart")]
        public async Task<IActionResult> AddToCart([FromQuery] int productId)
        {
            var cartItem = await _cartRepository.AddToCartAsync(productId);
            if (cartItem == null)
            {
                return NotFound("Product not found.");
            }

            return Ok(cartItem);
        }

        [HttpPost("increasequantity")]
        public async Task<IActionResult> IncreaseQuantityOfProduct([FromQuery] int productId)
        {
            var cartItem = await _cartRepository.IncreaseQuantityAsync(productId);
            if (cartItem == null)
            {
                return NotFound("Product not found in cart.");
            }

            return Ok(cartItem);
        }

        [HttpPost("decreasequantity")]
        public async Task<IActionResult> DecreaseQuantityOfProduct([FromQuery] int productId)
        {
            var cartItem = await _cartRepository.DecreaseQuantityAsync(productId);
            if (cartItem == null)
            {
                return BadRequest("Quantity cannot be less than 1 or product not found.");
            }

            return Ok(cartItem);
        }

        [HttpDelete("removefromcart")]
        public async Task<IActionResult> RemoveFromCart([FromQuery] int productId)
        {
            var cartItem = await _cartRepository.RemoveFromCartAsync(productId);
            if (cartItem == null)
            {
                return NotFound("Product not found in cart.");
            }

            return Ok("Product deleted from cart.");
        }

        [HttpGet("cart/cartitems")]
        public async Task<IActionResult> GetCartItems()
        {
            var cartItems = await _cartRepository.GetCartItemsAsync();

            if (cartItems == null || !cartItems.Any())
            {
                return Ok("Cart is empty.");
            }

            var productIds = cartItems.Select(ci => ci.ProductId).ToList();
            var products = await _context.Products
                .Where(p => productIds.Contains(p.Id))
                .ToListAsync();

            var result = cartItems.Join(products,
                cartItem => cartItem.ProductId,
                product => product.Id,
                (cartItem, product) => new
                {
                    product.Id,
                    product.Name,
                    product.Price,
                    product.Image,
                    cartItem.Quantity,
                    TotalPrice = cartItem.Quantity * product.Price
                }).ToList();

            return Ok(result);
        }
    }
}
