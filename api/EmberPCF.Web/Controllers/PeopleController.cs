using EmberPCF.Web.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace EmberPCF.Web.Controllers
{
    [Route("[controller]")]
    public class PeopleController : Controller
    {
        [HttpGet]
        public IEnumerable<Person> Get()
        {
            return StaticPersistentStore.People;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return new ObjectResult(StaticPersistentStore.People.Single(w => w.Id == id));
        }
    }
}