using AutoMapper;
using KShop.Core.DTOs;
using KShop.Core.Models;
using KShop.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        public async Task<ActionResult> CreateCart(CartDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            decimal price = 0;
            var cart = _mapper.Map<Cart>(model);
            cart.CartProducts = new List<CartProduct>();
            foreach(var item in model.ProductCarts)
            {
                var productCart = new CartProduct()
                {
                    Quantity = item.Quantity,
                    ProductId = item.ProductId,
                };
                cart.CartProducts.Add(productCart);
                var product = await _context.Products.FindAsync(item.ProductId);
                price += product!.Price * item.Quantity;
            }
            await _context.SaveChangesAsync();
            return Ok(price);
        }
    }
}
