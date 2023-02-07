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
using Microsoft.AspNetCore.Components.Authorization;
using Client.Static;
using ModelsLibrary.JsonModels;

namespace Client.Pages.Dashboard.Analysis
{
    public partial class Index
    {
        [CascadingParameter] protected Task<AuthenticationState> AuthenticationState { get; set; }
        private HttpClient _httpClient = new HttpClient();
        private bool _isAuthenticated { get; set; }
        private string _userName { get; set; }
        private Categories _categories = new Categories();
        private string _selectedCategoryName { get; set; }
        private List<string> _selectedCategoriesNames = new List<string>();
        IList<Meal> _meals = new List<Meal>();
        IList<Meal> _paginatedMeals = new List<Meal>();
        private List<IngredientDTO> _userIngredients = new List<IngredientDTO>();
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
        private async Task HandleCategoryItemsChange(IEnumerable<string> selectedCategories)
        {

            if (selectedCategories != null)
            {
                _selectedCategoriesNames = selectedCategories.ToList();
                int ids = 0;
                foreach (var categoryName in _selectedCategoriesNames)
                {
                    // Meals list
                    var resultMealsIds = await _httpClient.GetFromJsonAsync<Meals>($"https://www.themealdb.com/api/json/v1/1/filter.php?c={categoryName}");
                    if (resultMealsIds != null)
                    {
                        foreach (var meal in resultMealsIds.MealsList)
                        {
                            ids++;
                        }
                    }
                }
                await _message.Info(ids.ToString());
            }
        }
        [Inject] public IProjectService ProjectService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            _isLoading = true;
            var user = (await AuthenticationState).User;
            if (user.Identity.IsAuthenticated == true)
            {
                _isAuthenticated = true;
                _userName = user.Identity.Name;
                await LoadUserProducts();
            }
            // Categories list
            var resultCategories = await _httpClient.GetFromJsonAsync<Categories>("https://www.themealdb.com/api/json/v1/1/list.php?c=list");
            if (resultCategories != null)
            {
                foreach (var category in resultCategories.CategoriesList)
                    _categories.CategoriesList.Add(new Category() { strCategory = category.strCategory });
                _selectedCategoryName = _categories.CategoriesList[1].strCategory;
            }
            // Meals list
            await FindMeals();
            _isLoading = false;
        }
        private async Task FindMeals()
        {
            _isLoading = true;
            // Meals list
            List<int> mealsIdsList = new List<int>();
            var resultMealsIds = await _httpClient.GetFromJsonAsync<Meals>($"https://www.themealdb.com/api/json/v1/1/filter.php?c={_selectedCategoryName}");
            if (resultMealsIds != null)
            {
                foreach (var meal in resultMealsIds.MealsList)
                    mealsIdsList.Add(Int32.Parse(meal.idMeal));
            }
            if(mealsIdsList.Count > 0)
            {
                _meals.Clear();
                foreach (var mealId in mealsIdsList)
                {
                    var resultMeals = await _httpClient.GetFromJsonAsync<Meals>($"https://www.themealdb.com/api/json/v1/1/lookup.php?i={mealId}");
                    if (resultMeals != null)
                    {
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
                                Indgridients = new List<string>()
                                {
                                    meal.strIngredient1,
                                    meal.strIngredient2,
                                    meal.strIngredient3,
                                    meal.strIngredient4,
                                    meal.strIngredient5,
                                    meal.strIngredient6,
                                    meal.strIngredient7,
                                    meal.strIngredient8,
                                    meal.strIngredient9,
                                    meal.strIngredient10,
                                    meal.strIngredient11,
                                    meal.strIngredient12,
                                    meal.strIngredient13,
                                    meal.strIngredient14,
                                    meal.strIngredient15,
                                    meal.strIngredient16,
                                    meal.strIngredient17,
                                    meal.strIngredient18,
                                    meal.strIngredient19,
                                    meal.strIngredient20
                                }
                            });
                        }
                    }
                }
                // For displaying by pagination
                _currentPage = 1;
                _paginatedMeals = new List<Meal>();
                for (int i = 0; i < _countOfMealsDisplayOnPage && i < _meals.Count; i++)
                {
                    _paginatedMeals.Add(_meals[i]);
                }
            }
            _isLoading = false;
        }
        private async Task LoadUserProducts()
        {
            _isLoading = true;
            var userIngredientsResult = await _httpClient.GetFromJsonAsync<List<IngredientDTO>>(APIEndpoints.s_getUserIngredients + $"?userName={_userName}");
            _userIngredients = new List<IngredientDTO>();
            if (userIngredientsResult != null)
            {
                foreach (var ingredient in userIngredientsResult)
                    _userIngredients.Add(ingredient);
            }
            StateHasChanged();
            _isLoading = false;
        }
    }
}
