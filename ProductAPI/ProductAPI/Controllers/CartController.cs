using Microsoft.AspNetCore.Mvc;
using ProductAPI.Data;
using ProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ProductAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CartController : Controller
    {
        private readonly AppDbContext _context;

        public CartController(AppDbContext context)
        {
            _context = context;
        }

        //cart value
        [HttpGet("/cart/{buyerId}")]
        public async Task<ActionResult<List<Cart>>> GetCartValue(string buyerId)
        {
            var carts = await _context.Carts
                                      .Where(c => c.buyer_id == buyerId)
                                      .Select(c => new {
                                          c.buyer_id,
                                          c.product_id,
                                          c.quantity
                                      })
                                      .Distinct()
                                      .ToListAsync();

            if (!carts.Any())
            {
                return NotFound();
            }

            return Ok(carts);
        }



        // POST: api/cart/AddToCart
        [HttpPost("AddToCart")]
        public async Task<ActionResult<Cart>> AddToCart(Cart cart)
        {
            try
            {
                var existingCartItem = await _context.Carts.FirstOrDefaultAsync(c => c.product_id == cart.product_id && c.buyer_id == cart.buyer_id);

                if (existingCartItem != null)
                {
                    // Item already exists in the cart
                    return Conflict("Item already exists in the cart.");
                }

                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetCartValue), new { buyerId = cart.buyer_id }, cart);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        [HttpDelete("DeleteFromCart")]
        public async Task<IActionResult> DeleteFromCart(string buyerId, int productId)
        {
            try
            {
                var cartItem = await _context.Carts.FirstOrDefaultAsync(a => a.buyer_id == buyerId && a.product_id == productId);

                if (cartItem != null)
                {
                    _context.Carts.Remove(cartItem);
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
                else
                {
                    return NotFound(); // Return 404 Not Found if the cart item is not found
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return StatusCode(500); // Internal Server Error
            }
        }
    }
}
