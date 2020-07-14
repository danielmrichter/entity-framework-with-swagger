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
    public class BakeryController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public BakeryController(ApplicationContext context) {
            _context = context;
        }

        [HttpGet]
        public IActionResult getBakeryState() {
            BakeryState bs = new BakeryState {
                breadInventory = _context.BreadInventory.Include(b => b.bakedBy).ToList(),
                bakers = _context.Bakers.Include(b => b.breads).ToList(),
            };
            return Ok(bs);
        }
    }
}
