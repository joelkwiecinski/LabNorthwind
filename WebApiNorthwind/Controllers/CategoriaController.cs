using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApiNorthwind.Models;
using System.Linq;

namespace WebApiNorthwind.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {

        private readonly NorthwindContext _context;

        public CategoriaController(NorthwindContext context)
        {
            _context = context;
        }


        // GET /api/categoria
        [HttpGet]
        public List<Category> Get()
        {
            var categories = (from c in _context.Categories
                              select c).ToList();
            return categories;
        }


        // GET /api/categoria/id
        [HttpGet("{id}")]
        public Category Get(int id)
        {
            Category category = (from c in _context.Categories
                                 where c.CategoryId == id
                                 select c).SingleOrDefault();
            return category;
        }
    }
}
