using EmberPCF.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace EmberPCF.Web.Controllers
{
    [Route("[controller]")]
    public class CommentsController : Controller
    {
        [HttpGet]
        public IEnumerable<Comment> Get()
        {
            return StaticPersistentStore.Comments;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return new ObjectResult(StaticPersistentStore.Comments.Single(w => w.Id == id));
        }
    }
}