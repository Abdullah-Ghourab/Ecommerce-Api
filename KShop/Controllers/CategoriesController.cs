using KShop.Core.Models;
using KShop.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        readonly  ApplicationDbContext _context;
        public CategoriesController(ApplicationDbContext _context)
        {
            this._context = _context;
        }
        [HttpGet]
        public ActionResult GetCategories()
        {
            return Ok(_context.Categories.ToList());
        }
        [HttpGet("{id}")]

        public  ActionResult GetCategoryById(int id)
        {
                 
                var category =  _context.Categories.Find(id);

                if (category == null)
                    return NotFound();

                return Ok(category);
            }
            
        [HttpPost]
        public ActionResult PostCategories(Category c)
        {
            _context.Categories.Add(c);
            _context.SaveChanges();
            return Created("ok",c);

        }
        [HttpPut("{Id}")]
        public ActionResult PutCategories(int Id, Category c)
        {
            _context.Entry(c).State=EntityState.Modified;
            _context.SaveChanges() ;    
            return NoContent();

        }


    }
}
