using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Client.Models.MyModels
{
    public class Meals
    {
        public Meals()
        {
            MealsList = new List<Meal>();
        }
        [JsonPropertyName("meals")]
        public List<Meal> MealsList { get; set; }
    }
}
