using DishesApp.Server.Data.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelsLibrary;
using ModelsLibrary.JsonModels;
using Server.Data;

namespace DishesApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : Controller
    {
        private AppDbContext _dbManager;
        public IngredientController(AppDbContext appDbContext)
        {
            _dbManager = appDbContext;
        }
        [Route("get-all-ingredients")]
        [HttpGet]
        public List<Ingredient> GetUsersAsync()
        {
            return _dbManager.Ingredients.ToList();
        }
        [Route("get-user-ingredients")]
        [HttpGet]
        public async Task<ActionResult<List<IngredientDTO>>> GetUserIngredients(string userId)
        {
            var ingredients = from user in _dbManager.Users.Where(u => u.Id == userId)
                              from ingredient in _dbManager.Ingredients.Where(i => i.ApplicationUsers
                              .Where(u => u.Id == userId).First().Id == userId )
                              select new IngredientDTO
                              {
                                  Name = ingredient.Name
                              };
            return await ingredients.ToListAsync();
        }
        [Route("add-ingredient-to-user")]
        [HttpPost]
        public async Task<IActionResult> AddIngredientToUser(string userId, string ingredientName)
        {
            if(!String.IsNullOrWhiteSpace(ingredientName))
            {
                var user = await _dbManager.Users.Where(u => u.Id == userId).Include(p => p.Ingredients).FirstAsync();
                var existingIngredient = await _dbManager.Ingredients.SingleAsync(i => i.Name == ingredientName);
                user.Ingredients?.Add(existingIngredient);
                await _dbManager.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
