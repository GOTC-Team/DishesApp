using ModelsLibrary.JsonModels;
using Server.Data;
using System.Configuration;

namespace DishesApp.Server.Data
{
    public class InitIngredients
    {
        private AppDbContext _dbManager;
        public InitIngredients(AppDbContext dbContext)
        {
            _dbManager = dbContext;
        }
        private HttpClient _httpClient = new HttpClient();
        public async void Init()
        {
            try
            {
                // https://www.themealdb.com/api/json/v1/1/list.php?i=list
                var result = await _httpClient.GetFromJsonAsync<IngredientsList>("https://www.themealdb.com/api/json/v1/1/list.php?i=list");
                if(result != null)
                {
                    _dbManager.Ingredients.RemoveRange(_dbManager.Ingredients);
                    foreach (var ingredient in result!.Ingredients)
                    {
                        await _dbManager.Ingredients.AddAsync(new DbModels.Ingredient() { Name = ingredient.Name });
                    }
                    await _dbManager.SaveChangesAsync();
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
