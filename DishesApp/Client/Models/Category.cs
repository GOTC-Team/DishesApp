using System.Text.Json.Serialization;

namespace Client.Models
{
    public class Category
    {
        [JsonPropertyName("strCategory")]
        public string strCategory { get; set; }
    }
}
