using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ModelsLibrary.JsonModels
{
    public class Ingredient
    {
        [JsonPropertyName("strIngredient")]
        public string? Name { get; set; }
    }
    public class IngredientsList
    {
        public IngredientsList()
        {
            Ingredients = new List<Ingredient>();
        }
        [JsonPropertyName("meals")]
        public List<Ingredient> Ingredients { get; set; }
    }
}
