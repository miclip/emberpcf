﻿using System;
using System.Collections.Generic;
using System.Linq;
using NJsonApi.Web.MVCCore.HelloWorld.Models;

namespace NJsonApi.Web.MVCCore.HelloWorld.Models
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