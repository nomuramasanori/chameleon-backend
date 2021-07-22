using Newtonsoft.Json;

namespace Chameleon
{
    public interface ITextOption
    {
        [JsonProperty(PropertyName = "length")]
        int Length { get; set; }

        [JsonProperty(PropertyName = "minLength")]
        int MinLength { get; set; }

        [JsonProperty(PropertyName = "maxLength")]
        int MaxLength { get; set; }
    }
}
