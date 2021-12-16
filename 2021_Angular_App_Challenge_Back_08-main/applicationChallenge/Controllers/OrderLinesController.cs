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
    public class OrderLinesController : ControllerBase
    {
        private readonly ShopContext _context;

        public OrderLinesController(ShopContext context)
        {
            _context = context;
        }

        // GET: api/OrderLines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderLine>>> GetOrderLines()
        {
            return await _context.OrderLines
                .Include(o => o.Order)
                .ThenInclude(u => u.User)
                .Include(p => p.Product)
                .ThenInclude(c => c.Category)
                .ToListAsync();
        }

        // GET: api/OrderLines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderLine>> GetOrderLine(int id)
        {
            var orderLine = await _context.OrderLines
                .Include(o => o.Order)
                .ThenInclude(u => u.User)
                .Include(p => p.Product)
                .ThenInclude(c => c.Category)
                .SingleOrDefaultAsync(o => o.OrderLineID == id);

            if (orderLine == null)
            {
                return NotFound();
            }

            return orderLine;
        }


        [HttpGet("OrderID/{orderID}")]
        public async Task<ActionResult<IEnumerable<OrderLine>>> GetOrderLineByOrderID(int orderID)
        {
            return await _context.OrderLines
                .Where(o => o.OrderID == orderID)
                .Include(p => p.Product)
                .ThenInclude(c => c.Category)
                .Include(o => o.Order)
                .ThenInclude(u => u.User)
                .ToListAsync();
        }

        // PUT: api/OrderLines/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderLine(int id, OrderLine orderLine)
        {
            if (id != orderLine.OrderLineID)
            {
                return BadRequest();
            }

            _context.Entry(orderLine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderLineExists(id))
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

        // POST: api/OrderLines
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [HttpPost]
        public async Task<ActionResult<OrderLine>> PostOrderLine(OrderLine orderLine)
        {
            _context.OrderLines.Add(orderLine);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderLine", new { id = orderLine.OrderLineID }, orderLine);
        }

        // DELETE: api/OrderLines/5

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderLine(int id)
        {
            var orderLine = await _context.OrderLines.FindAsync(id);
            if (orderLine == null)
            {
                return NotFound();
            }

            _context.OrderLines.Remove(orderLine);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderLineExists(int id)
        {
            return _context.OrderLines.Any(e => e.OrderLineID == id);
        }
    }
}
