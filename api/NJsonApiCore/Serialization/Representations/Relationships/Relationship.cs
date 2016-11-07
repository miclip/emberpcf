﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace NJsonApi.Serialization.Representations.Relationships
{
    public class Relationship : IRelationship
    {
        [JsonProperty(PropertyName = "links", NullValueHandling = NullValueHandling.Ignore)]
        public RelationshipLinks Links { get; set; }

        [JsonProperty(PropertyName = "data", NullValueHandling = NullValueHandling.Ignore)]
        public IResourceLinkage Data { get; set; }

        [JsonProperty(PropertyName = "meta", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> Meta { get; set; }
    }
}