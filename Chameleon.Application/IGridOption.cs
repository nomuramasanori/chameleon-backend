using System;
using Newtonsoft.Json;

namespace Chameleon
{
    public interface IGridOption
    {
        [JsonProperty(PropertyName = "rowEditable")]
        bool RowEditable { get; }

        [JsonProperty(PropertyName = "frozenRows")]
        int FrozenRows { get; }

        [JsonProperty(PropertyName = "frozenColumns")]
        int FrozenColumns { get; }
    }
}
