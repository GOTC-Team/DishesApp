using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ModelsLibrary.JsonModels
{
    public class IngredientDTO
    {
        [JsonPropertyName("strIngredient")]
        public string? Name { get; set; }
    }
    public class IngredientsList
    {
        public IngredientsList()
        {
            Ingredients = new List<IngredientDTO>();
        }
        [JsonPropertyName("meals")]
        public List<IngredientDTO> Ingredients { get; set; }
    }
}
