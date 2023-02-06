using System.Text.Json.Serialization;

namespace DishesApp.Server.Data.DbModels
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public virtual List<ApplicationUser>? ApplicationUsers { get; set; }
    }
}
