using System;
using Newtonsoft.Json;

namespace Chameleon
{
    public class Link
    {
        [JsonProperty(PropertyName = "destination")]
        public string Destination { get; set; }

        [JsonProperty(PropertyName = "condition")]
        public object Condition { get; set; }

        public Link(Type destination, object condition)
        {
            this.Destination = destination.FullName;
            this.Condition = condition;
        }
    }
}
