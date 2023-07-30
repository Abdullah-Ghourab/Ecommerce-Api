using AutoMapper;
using KShop.Core.DTOs;
using KShop.Core.Models;
using KShop.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CartController(ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult> GetUserCarts(string userId)
        {
            var carts = await _context.Carts
                            .Include(e => e.CartProducts!)
                            .ThenInclude(x => x.Product)
                            .Where(c => c.UserId == userId)
                            .ToListAsync();
            return Ok(carts);
        }
        [HttpPost]
        public async Task<ActionResult> CreateCart(CartDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            decimal price = 0;
            List<CartProduct> cartProducts = new();
            foreach (var item in model.ProductCarts)
            {
                var productCart = new CartProduct()
                {
                    Quantity = item.Quantity,
                    ProductId = item.ProductId,
                };
                cartProducts.Add(productCart);
                var product = await _context.Products.FindAsync(item.ProductId);
                price += product!.Price * item.Quantity;
            }
            var cart = _mapper.Map<Cart>(model);
            cart.CartProducts = cartProducts;
            cart.Price = price;
            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();
            return Ok(price);
        }
        [HttpPost("AddItemToCart")]
        public async Task<ActionResult> AddItemToCart(CartProduct model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _context.CartProducts.AddAsync(model);
            await _context.SaveChangesAsync();
            return Created("", model);
        }
        [HttpDelete("RemoveItemFromCart/{id}")]
        public async Task<ActionResult> RemoveItemFromCart(int id)
        {
            var item = await _context.CartProducts.FindAsync(id);
            if (item is null)
                return NotFound();
            _context.CartProducts.Remove(item);
            _context.SaveChanges();
            return Ok("Removed Successfully");
        }
    }
}
