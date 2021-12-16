using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using applicationChallenge.Data;
using applicationChallenge.Models;
using Microsoft.AspNetCore.Authorization;

namespace applicationChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ShopContext _context;

        public ProductsController(ShopContext context)
        {
            _context = context;
        }

        //pagnur : 2
        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery] ProductsParameter productsParameter)
        {
            return await _context.Products
                .Include(c => c.Category)
                .Include(ol => ol.OrderLines)
                .ThenInclude(o => o.Order)
                .Skip((productsParameter.PageNumber -1) * productsParameter.PageSize)
                .Take(productsParameter.PageSize)
                .ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products
                .Include(c => c.Category)
                .Include(ol => ol.OrderLines)
                .ThenInclude(o => o.Order)
                .SingleOrDefaultAsync(p => p.ProductID == id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        //Search products
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Product>>> Search([FromQuery] ProductsParameter productsParameter, string name, string sortby)
        {
            try
            {
                if (!string.IsNullOrEmpty(name))
                {
                    if (sortby == "Price")
                    {
                        return await _context.Products.Where(p => p.Name.Contains(name)).OrderBy(p => p.Price).Skip((productsParameter.PageNumber - 1) * productsParameter.PageSize).Take(productsParameter.PageSize).ToListAsync();
                    }
                    else if (sortby == "Name")
                    {
                        return await _context.Products.Where(p => p.Name.Contains(name)).OrderBy(p => p.Name).Skip((productsParameter.PageNumber - 1) * productsParameter.PageSize).Take(productsParameter.PageSize).ToListAsync();
                    }
                    else
                    {
                        return await _context.Products.Where(p => p.Name.Contains(name)).Skip((productsParameter.PageNumber - 1) * productsParameter.PageSize).Take(productsParameter.PageSize).ToListAsync();
                    }
                }
                else
                {
                    return await _context.Products.Skip((productsParameter.PageNumber - 1) * productsParameter.PageSize).Take(productsParameter.PageSize).ToListAsync();
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }

        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.ProductID)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.ProductID }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductID == id);
        }
    }
}
