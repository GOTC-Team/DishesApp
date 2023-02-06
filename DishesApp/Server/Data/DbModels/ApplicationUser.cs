using Microsoft.AspNetCore.Identity;

namespace DishesApp.Server.Data.DbModels
{
    public class ApplicationUser : IdentityUser
    {
        public virtual List<Ingredient>? Ingredients { get; set; }
    }
}
