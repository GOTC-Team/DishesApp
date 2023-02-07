using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Client.Models
{
    public class Categories
    {
        public Categories()
        {
            CategoriesList = new List<Category>();
        }
        [JsonPropertyName("meals")]
        public List<Category> CategoriesList { get; set; }
    }
}
