﻿using Newtonsoft.Json;

namespace NJsonApi.Serialization.Representations.Relationships
{
    public class RelationshipLinks
    {
        [JsonProperty(PropertyName = "self", NullValueHandling = NullValueHandling.Ignore)]
        public ILink Self { get; set; }

        [JsonProperty(PropertyName = "related", NullValueHandling = NullValueHandling.Ignore)]
        public ILink Related { get; set; }
    }
}