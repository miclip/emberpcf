using Microsoft.AspNetCore.Mvc;
using EmberPCF.Web.Models;
using System;
using System.Collections.Generic;

namespace EmberPCF.Web.Controllers.Tests
{
    [Route("[controller]")]
    public class TestExamplesController : Controller
    {
        [HttpGet]
        [Route("[action]")]
        public void ThrowException()
        {
            throw new NotImplementedException("An example exception thrown");
        }

        [HttpGet]
        [Route("[action]")]
        public IEnumerable<Article> GetEmptyList()
        {
            return new List<Article>();
        }
    }
}