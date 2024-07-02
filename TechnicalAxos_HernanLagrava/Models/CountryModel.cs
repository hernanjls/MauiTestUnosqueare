using Newtonsoft.Json;

namespace TechnicalAxos_HernanLagrava.Models
{
    public class CountryModel
    {
        [JsonProperty("name")]
        public Name? Name { get; set; }
        [JsonProperty("region")]
        public string? Region { get; set; }
        [JsonProperty("cca2")]
        public string? CountryCode { get; set; }
        [JsonProperty("population")]
        public int? Population { get; set; }
        [JsonProperty("flags")]
        public Flags? Flags { get; set; }
        [JsonProperty("languages")]
        public Dictionary<string, string>? Languages { get; set; }

        public string LanguagesDisplay => Languages != null
        ? string.Join(", ", Languages.Keys)
        : string.Empty;

    }
    public class Name
    {
        [JsonProperty("common")]
        public string? Common { get; set; }
        [JsonProperty("official")]
        public string? Official { get; set; }
    }
    public class Flags
    {
        [JsonProperty("png")]
        public string? Png { get; set; }

        [JsonProperty("svg")]
        public string? Svg { get; set; }

    }

}
