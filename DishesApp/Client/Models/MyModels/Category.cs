using System.Text.Json.Serialization;

namespace Client.Models.MyModels
{
    public class Category
    {
        [JsonPropertyName("strCategory")]
        public string strCategory { get; set; }
    }
}
