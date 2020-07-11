using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace dotnet_bakery.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BreadController : ControllerBase
    {

        [HttpGet]
        [Route("int")]
        public int getInt()
        {
            return 1;
        }

        [HttpGet]
        [Route("test")]
        public IEnumerable<Bread> GetBreads() {
            Bread newBread = new Bread();
            newBread.name = "SourDough";
            newBread.description = "This is the special sauce";
            
            Bread newBread2 = new Bread();
            newBread2.name = "SourDough Rye";
            newBread2.description = "A rye version of the old school classic";

            List<Bread> ret = new List<Bread>();
            ret.Add(newBread);
            ret.Add(newBread2);
            return ret;
        }
    }
}
