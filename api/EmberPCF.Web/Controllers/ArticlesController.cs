using Microsoft.AspNetCore.Mvc;
using NJsonApi.Infrastructure;
using EmberPCF.Web.Models;
using System.Collections.Generic;
using System.Linq;


namespace EmberPCF.Web.Controllers
{
    [Route("[controller]")]
    public class ArticlesController : Controller
    {
        [HttpGet]
        public IEnumerable<Article> Get()
        {
            return StaticPersistentStore.Articles;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return new ObjectResult(StaticPersistentStore.Articles.Single(w => w.Id == id));
        }

        [HttpPost]
        public IActionResult Post([FromBody]Delta<Article> article)
        {
            var newArticle = article.ToObject();
            newArticle.Id = StaticPersistentStore.GetNextId();
            StaticPersistentStore.Articles.Add(newArticle);
            return CreatedAtAction("Get", new { id = newArticle.Id }, newArticle);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch([FromBody]Delta<Article> update, int id)
        {
            var article = StaticPersistentStore.Articles.Single(w => w.Id == id);
            update.ApplySimpleProperties(article);
            return new ObjectResult(article);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            StaticPersistentStore.Articles.RemoveAll(x => x.Id == id);
            return new NoContentResult();
        }

        [HttpGet("/articles/{articleId}/comments")]
        public IEnumerable<Comment> GetComments(int articleId)
        {
            return StaticPersistentStore.Articles.Single(a=>a.Id == articleId).Comments;
        }

        [HttpGet("/articles/{articleId}/author")]
        public IActionResult GetAuthor(int articleId)
        {
            return new ObjectResult(StaticPersistentStore.Articles.Single(a=>a.Id == articleId).Author);
        }
    }
}