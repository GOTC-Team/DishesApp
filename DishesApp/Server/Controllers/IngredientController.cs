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
        public async Task<ActionResult<List<IngredientDTO>>> GetAllIngredients()
        {
           var ingredients = from ingredient in _dbManager.Ingredients
                   select new IngredientDTO
                   {
                       Name = ingredient.Name
                   };
            return await ingredients.ToListAsync();
        }
        [Route("get-user-ingredients")]
        [HttpGet]
        public async Task<ActionResult<List<IngredientDTO>>> GetUserIngredients(string userName)
        {
            var ingredients = from user in _dbManager.Users.Where(u => u.UserName == userName)
                              from ingredient in _dbManager.Ingredients.Where(i => i.ApplicationUsers
                              .Where(u => u.UserName == userName).First().UserName == userName)
                              select new IngredientDTO
                              {
                                  Name = ingredient.Name
                              };
            return await ingredients.ToListAsync();
        }
        [Route("add-ingredient-to-user")]
        [HttpPost]
        public async Task<IActionResult> AddIngredientToUser([FromBody] UserNameIngredient userNameIngredient)
        {
            if (!String.IsNullOrWhiteSpace(userNameIngredient.UserName) && !String.IsNullOrWhiteSpace(userNameIngredient.IngredientName))
            {
                var user = await _dbManager.Users.Where(u => u.UserName == userNameIngredient.UserName).Include(p => p.Ingredients).FirstAsync();
                var existingIngredient = _dbManager.Ingredients.Where(i => i.Name == userNameIngredient.IngredientName.Trim()).FirstOrDefault();
                if(existingIngredient != null)
                {
                    user.Ingredients?.Add(existingIngredient);
                    await _dbManager.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
