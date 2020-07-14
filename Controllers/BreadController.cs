using System.Net.NetworkInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using dotnet_bakery.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_bakery.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BreadController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public BreadController(ApplicationContext context) {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<BreadInventory> getAllBreads() {
            return _context.BreadInventory.Include(bread => bread.bakedBy).ToList();
            // return _context.BreadItems.ToList();
        }

        [HttpPost]
        public IActionResult Post([FromBody] BreadInventory bread) {
            // Manually check foreign key for now
            Baker baker = _context.Bakers.SingleOrDefault( m => m.id == bread.bakedByid);
            if (baker == null) {
                ModelState.AddModelError("bakedByid", "Invalid Baker ID");
                return ValidationProblem(ModelState);
            }
            _context.Add(bread);
            _context.SaveChanges();
            // return Ok();
            return CreatedAtAction(nameof(GetById), new { id = bread.id}, bread);
        }

        // GET /{id}
        // Returns the bread with the given id
        [HttpGet("{id}")]
        public IActionResult GetById(int id) {
            // Baker baker = _context.Find<Baker>(id);
            BreadInventory bread = _context.BreadInventory
                .Include(bread => bread.bakedBy)
                .SingleOrDefault( bread => bread.id == id);
            if (bread == null) return NotFound();
            return Ok(bread);
        }

        [HttpPut("{id}/sell")]
        public IActionResult SellById(int id) {
            BreadInventory bread = _context.BreadInventory.Find(id);
            if (bread == null) return NotFound();
            if (bread.inventory <= 0) return BadRequest(new { error = "Cant reduce inventory below zero" });
            bread.sell();
            _context.Update(bread);
            _context.SaveChanges();
            return Ok(bread);
        }

        [HttpPut("{id}/bake")]
        public IActionResult BakeById(int id) {
            BreadInventory bread = _context.BreadInventory.Find(id);
            if (bread == null) return NotFound();
            bread.bake();
            _context.Update(bread);
            _context.SaveChanges();
            return Ok(bread);
        }

        // PUT /:id
        // Updates a bread inventory item
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] BreadInventory bread) {
            if (id != bread.id) return BadRequest();

            // Make sure the baker we are updating is real
            if (!_context.BreadInventory.Any(b => b.id == id)) return NotFound();

            // find the baker in the db and mark it as modified
            _context.Entry(bread).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        // DELETE /:id
        // Deletes a bread by primary key (id)
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            BreadInventory bread = _context.BreadInventory.Find(id);
            if (bread == null) return NotFound();

            _context.BreadInventory.Remove(bread);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpGet]
        [Route("test")]
        public IEnumerable<BreadInventory> GetBreads() {
            Baker blaine = new Baker{
                name = "Blaine"
            };
            // _context.Add(blaine);
            // _context.SaveChanges();

            BreadInventory newBread = new BreadInventory {
                name = "Daily SourDough",
                description = "This is the special sauce",
                // breadType = BreadType.Sourdough,
                bakedBy = blaine,
            };
            // newBread.bakedBy = blaine;
            
            BreadInventory newBread2 = new BreadInventory {
                name = "Classic Rye",
                description = "This is a classic sandwhich style rye bread",
                bakedBy = blaine,
                // breadType = BreadType.Rye
            };

            newBread.bake(10);

            // _context.Add(newBread);
            // _context.Add(newBread2);
            // _context.SaveChanges();

            return new List<BreadInventory>{ newBread, newBread2};
        }
    }
}
