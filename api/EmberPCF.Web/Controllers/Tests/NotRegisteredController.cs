using Microsoft.AspNetCore.Mvc;
using NJsonApiCore.Web.MVCCore.HelloWorld.Models;
using System.Collections.Generic;

namespace NJsonApiCore.Web.MVCCore.HelloWorld.Controllers.Tests
{
    [Route("[controller]")]
    public class NotRegisteredController : Controller
    {
        [HttpGet]
        public IEnumerable<SimpleModel> Get()
        {
            return new List<SimpleModel> { new SimpleModel() { Id = 1 } };
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return new ObjectResult(new SimpleModel() { Id = id });
        }
    }
}