using System;
using Newtonsoft.Json;

namespace Chameleon
{
    public interface INumberOption
    {
        [JsonProperty(PropertyName = "max")]
        double Max { get; set; }

        [JsonProperty(PropertyName = "min")]
        double Min { get; set; }

        [JsonProperty(PropertyName = "displayDigit")]
        int DisplayDigit { get; set; }
    }
}
