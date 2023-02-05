using AntDesign;
using Client.Models.MyModels;
using Client.Models;
using Client.Services;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Net.Http;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace Client.Pages.Dashboard.Analysis
{
    public partial class Index
    {
        private HttpClient _httpClient = new HttpClient();
        private Categories _categories = new Categories();
        private List<string> _selectedCategoriesNames = new List<string>();
        IList<Meal> _meals = new List<Meal>();
        public List<string> CategoryMember { get; set; } = new List<string>();
        private readonly ListGridType _listGridType = new ListGridType
        {
            Gutter = 16,
            Xs = 1,
            Sm = 2,
            Md = 3,
            Lg = 3,
            Xl = 4,
            Xxl = 4,
        };

        private readonly FormItemLayout _formItemLayout = new FormItemLayout
        {
            WrapperCol = new ColLayoutParam
            {
                Xs = new EmbeddedProperty { Span = 24 },
                Sm = new EmbeddedProperty { Span = 16 },
            }
        };
        private readonly ListFormModel _model = new ListFormModel();

        private IList<ListItemDataType> _fakeList = new List<ListItemDataType>();
        private void HandleCategoryItemsChange(IEnumerable<string> selectedCategories)
        {
            if (selectedCategories != null)
            {
                _selectedCategoriesNames = selectedCategories.ToList();
                foreach (var categoryName in _selectedCategoriesNames)
                {
                    Console.WriteLine(categoryName);
                }
            }
        }
        [Inject] public IProjectService ProjectService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            // Categories list
            var resultCategories = await _httpClient.GetFromJsonAsync<Categories>("https://www.themealdb.com/api/json/v1/1/list.php?c=list");
            if (resultCategories != null)
            {
                foreach (var category in resultCategories.CategoriesList)
                    _categories.CategoriesList.Add(new Category() { strCategory = category.strCategory });
            }
            // Meals list
            var resultMeals = await _httpClient.GetFromJsonAsync<Meals>("https://www.themealdb.com/api/json/v1/1/search.php?f=a");
            if (resultMeals != null)
            {
                int counter = 0;
                foreach (var meal in resultMeals.MealsList)
                {
                    _meals.Add(new Meal()
                    {
                        idMeal = meal.idMeal,
                        dateModified = meal.dateModified,
                        strArea = meal.strArea,
                        strCategory = meal.strCategory,
                        strCreativeCommonsConfirmed = meal.strCreativeCommonsConfirmed,
                        strDrinkAlternate = meal.strDrinkAlternate,
                        strImageSource = meal.strImageSource,
                        strInstructions = meal.strInstructions,
                        strMeal = meal.strMeal,
                        strMealThumb = meal.strMealThumb,
                        strSource = meal.strSource,
                        strTags = meal.strTags,
                        strYoutube = meal.strYoutube,
                        strIngredient1 = meal.strIngredient1,
                        strIngredient2 = meal.strIngredient2,
                        strIngredient3 = meal.strIngredient3,
                        strIngredient4 = meal.strIngredient4,
                        strIngredient5 = meal.strIngredient5,
                        strIngredient6 = meal.strIngredient6,
                        strIngredient7 = meal.strIngredient7,
                        strIngredient8 = meal.strIngredient8,
                        strIngredient9 = meal.strIngredient9,
                        strIngredient10 = meal.strIngredient10,
                        strIngredient11 = meal.strIngredient11,
                        strIngredient12 = meal.strIngredient12,
                        strIngredient13 = meal.strIngredient13,
                        strIngredient14 = meal.strIngredient14,
                        strIngredient15 = meal.strIngredient15,
                        strIngredient16 = meal.strIngredient16,
                        strIngredient17 = meal.strIngredient17,
                        strIngredient18 = meal.strIngredient18,
                        strIngredient19 = meal.strIngredient19,
                        strIngredient20 = meal.strIngredient20
                    });
                    _meals[counter].Indgridients.Add(_meals[counter].strIngredient1);
                    _meals[counter].Indgridients.Add(_meals[counter].strIngredient2);
                    _meals[counter].Indgridients.Add(_meals[counter].strIngredient3);
                    _meals[counter].Indgridients.Add(_meals[counter].strIngredient4);
                    _meals[counter].Indgridients.Add(_meals[counter].strIngredient5);
                    _meals[counter].Indgridients.Add(_meals[counter].strIngredient6);
                    _meals[counter].Indgridients.Add(_meals[counter].strIngredient7);
                    _meals[counter].Indgridients.Add(_meals[counter].strIngredient8);
                    _meals[counter].Indgridients.Add(_meals[counter].strIngredient9);
                    _meals[counter].Indgridients.Add(_meals[counter].strIngredient10);
                    _meals[counter].Indgridients.Add(_meals[counter].strIngredient11);
                    _meals[counter].Indgridients.Add(_meals[counter].strIngredient12);
                    _meals[counter].Indgridients.Add(_meals[counter].strIngredient13);
                    _meals[counter].Indgridients.Add(_meals[counter].strIngredient14);
                    _meals[counter].Indgridients.Add(_meals[counter].strIngredient15);
                    _meals[counter].Indgridients.Add(_meals[counter].strIngredient16);
                    _meals[counter].Indgridients.Add(_meals[counter].strIngredient17);
                    _meals[counter].Indgridients.Add(_meals[counter].strIngredient18);
                    _meals[counter].Indgridients.Add(_meals[counter].strIngredient19);
                    _meals[counter].Indgridients.Add(_meals[counter].strIngredient20);
                    counter++;
                    // Def
                }
            }
            _fakeList = await ProjectService.GetFakeListAsync(8);
        }
    }
}
