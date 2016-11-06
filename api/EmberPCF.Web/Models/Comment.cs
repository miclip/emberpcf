using System;
using System.Collections.Generic;
using System.Linq;
using EmberPCF.Web.Models;

namespace EmberPCF.Web.Models
{
    public class Comment
    {
        public Comment(string body)
        {
            Id = StaticPersistentStore.GetNextId();
            Body = body;
        }

        public int Id { get; set; }

        public string Body { get; set; }

        public Person Author { get; set; }
    }
}