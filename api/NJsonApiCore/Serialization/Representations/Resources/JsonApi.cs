using Newtonsoft.Json;

namespace NJsonApi.Serialization.Representations.Resources
{
    public class JsonApi
    {
        [JsonProperty(PropertyName = "version", NullValueHandling = NullValueHandling.Ignore)]
        public string Version => "1.0";
    }
}