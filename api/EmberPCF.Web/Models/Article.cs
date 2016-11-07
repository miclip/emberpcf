using System.Collections.Generic;

namespace EmberPCF.Web.Models
{
    public class Article
    {
        public Article()
        {
        }

        public Article(string title)
        {
            Comments = new List<Comment>();
            Id = StaticPersistentStore.GetNextId();
            Title = title;
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public Person Author { get; set; }

        public string Body {get;set;}

        public List<Comment> Comments { get; set; }
    }
}