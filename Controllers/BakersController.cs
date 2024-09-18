using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DotnetBakery.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetBakery.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BakersController : ControllerBase
    {
      // Within the BakersController class, there shall forthwith
      // be a property named _context whose type is ApplicationContext:
      private readonly ApplicationContext _context;

      // This is out constructor function
      // Our `ApplicationContext` is automagically passed to it 
      // as an argument by .NET, and _context's value is assigned
      // the actual mapping between model names and table names!
      public BakersController(ApplicationContext context) 
      {
          _context = context;
      }

      // GET /api/bakers
      [HttpGet]
      public IEnumerable<Baker> GetAll() 
      {
          // This is essentially:
          //    return await pool.query('SELECT * FROM "bakers");
          return _context.Bakers;
      }
    }
}
