using AutoMapper;
using KShop.Core.DTOs;
using KShop.Core.Models;
using KShop.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ProductsController(ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _context.Products.Where(p => !p.IsDeleted).ToListAsync();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if(product == null)
                return NotFound();  
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var product = _mapper.Map<Product>(model);
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return Created("",product);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id,ProductDto model)
        {
            if(!ModelState.IsValid || id != model.Id)
                return BadRequest(ModelState);
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();
            product = _mapper.Map<Product>(model);
            await _context.SaveChangesAsync();
            return Ok(product);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();
            product.IsDeleted = true;
            await _context.SaveChangesAsync();
            return Ok();
        }


    }
}
