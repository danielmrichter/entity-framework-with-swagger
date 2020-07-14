using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using dotnet_bakery.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_bakery.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BakerController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public BakerController(ApplicationContext context) {
            _context = context;
        }

        [HttpGet("intTest")]
        public int getInt() {
            return 1;
        }

        // GET /
        // Returns all bakers
        [HttpGet]
        public IEnumerable<Baker> getAllBakers(int minBread = 0) {
            Console.WriteLine($"hasBread is {minBread}");

            // We have to use baker.breads.Count instead of baker.breadCount
            // because breadCount is not a database function, so linq is
            // not able to translate the call into SQL.
            return _context.Bakers
                .Include(baker => baker.breads)
                .Where(baker => baker.breads.Count >= minBread)
                .ToList();
        }

        // GET /{id}
        // Returns the baker with the given id
        [HttpGet("{id}")]
        public IActionResult GetById(int id) {
            // Baker baker = _context.Find<Baker>(id);
            Baker baker = _context.Bakers
                .Include(baker => baker.breads)
                .SingleOrDefault( baker => baker.id == id);
            if (baker == null) return NotFound();
            return Ok(baker);
        }

        // POST /
        // Adds a Baker to the database. The baker comes in via HTTP body
        [HttpPost]
        public IActionResult Post([FromBody] Baker baker) {
            // Manually check foreign key for now
            _context.Add(baker);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = baker.id}, baker);
        }

        // PUT /:id
        // Updates a baker
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Baker baker) {
            if (id != baker.id) return BadRequest();

            // Make sure the baker we are updating is real
            if (!_context.Bakers.Any(b => b.id == id)) return NotFound();

            // find the baker in the db and mark it as modified
            _context.Entry(baker).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        // DELETE /:id
        // Deletes a baker by primary key (id)
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            Baker baker = _context.Bakers.Find(id);
            if (baker == null) return NotFound();

            _context.Bakers.Remove(baker);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
