using AntDesign;
using AntDesign.ProLayout;
using Client.Models;
using Client.Static;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using ModelsLibrary;
using ModelsLibrary.JsonModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Client.Pages.MyProducts
{
    public partial class ProductsList
    {
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
        [Inject] HttpClient HttpClient { get; set; }
        [Inject] protected NavigationManager NavigationManager { get; set; }
        [CascadingParameter] protected Task<AuthenticationState> AuthenticationState { get; set; }
        //
        private readonly IList<string> _selectCategories = new List<string>();
        private readonly List<IngredientDTO> _ingredients = new List<IngredientDTO>();
        private List<IngredientDTO> _userIngredients = new List<IngredientDTO>();
        private string _mentionedIngredients { get; set; }
        private bool _isAuthenticated { get; set; }
        private string _userName { get; set; }
        private void Cancel()
        {
            _message.Error("Okay, never mind :)");
        }
        private async void RemoveProduct(string productName)
        {
            HttpResponseMessage deleteResult = await HttpClient.DeleteAsync(APIEndpoints.s_removeIngredientFromUser + $"?names={_userName}&names={productName}");
            if(deleteResult.IsSuccessStatusCode)
            {
                await LoadUserProducts();
                await _message.Success($"{productName} deleted!");
            }
            else
                await _message.Error($"Oops, try again.");

        }
        private async void AddSelectedProducts()
        {
            string[] ingredients = _mentionedIngredients.Split(" @");
            bool isSuccess = false;
            foreach (var ingredient in ingredients)
            {
                if(!String.IsNullOrWhiteSpace(ingredient))
                {
                    HttpResponseMessage httpResponseMessage = await HttpClient.PostAsJsonAsync(APIEndpoints.s_addIngredientToUser, 
                        new UserNameIngredient() { IngredientName = ingredient, UserName = _userName});
                    if (httpResponseMessage.IsSuccessStatusCode)
                        isSuccess = true;
                    else
                        break;
                }
            }
            if(isSuccess)
            {
                await LoadUserProducts();
                string res = ingredients.Length > 2 ? "Products was successfully added" : "Product was successfully added";
                await Swal.FireAsync(res);
            }
        }
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            var user = (await AuthenticationState).User;
            if (user.Identity.IsAuthenticated == true)
            {
                _isAuthenticated = true;
                _userName = user.Identity.Name;
                var allIngredientsResult = await HttpClient.GetFromJsonAsync<List<IngredientDTO>>(APIEndpoints.s_getAllIngredients);
                if (allIngredientsResult != null)
                {
                    foreach (var ingredient in allIngredientsResult)
                        _ingredients.Add(ingredient);
                }
                await LoadUserProducts();
            }
        }
        private async Task LoadUserProducts()
        {
            var userIngredientsResult = await HttpClient.GetFromJsonAsync<List<IngredientDTO>>(APIEndpoints.s_getUserIngredients + $"?userName={_userName}");
            _userIngredients = new List<IngredientDTO>();
            if (userIngredientsResult != null)
            {
                foreach (var ingredient in userIngredientsResult)
                    _userIngredients.Add(ingredient);
            }
            StateHasChanged();
        }
    }
}