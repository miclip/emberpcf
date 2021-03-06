﻿using EmberPCF.Web.Models;
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

        [HttpGet("/comments/{commentId}/author")]
        public IActionResult GetComments(int commentId)
        {
            return new ObjectResult(StaticPersistentStore.Comments.Single(a=>a.Id == commentId).Author);
        }
        
    }
}