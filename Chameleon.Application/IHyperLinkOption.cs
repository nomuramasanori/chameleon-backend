using System;
using Newtonsoft.Json;

namespace Chameleon
{
    public interface IHyperLinkOption
    {
        [JsonProperty(PropertyName = "iconName")]
        string IconName { get; set; }
    }
}
