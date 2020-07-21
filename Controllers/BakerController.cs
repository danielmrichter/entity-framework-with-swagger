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
    }
}
