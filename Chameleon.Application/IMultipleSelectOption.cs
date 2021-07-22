using System;
using Newtonsoft.Json;

namespace Chameleon
{
    public interface IMultipleSelectOption
    {
        [JsonProperty(PropertyName = "grouping")]
        bool Grouping { get; set; }
    }
}
