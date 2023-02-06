using AntDesign;
using AntDesign.ProLayout;
using Blazored.LocalStorage;
using Client.Models;
using Client.Providers;
using Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Components
{
    public partial class RightContent
    {
        [CascadingParameter] protected Task<AuthenticationState> AuthenticationState { get; set; }
        [Inject] HttpClient Http { get; set; }
        private string _userName { get; set; } = string.Empty;
        //
        public AvatarMenuItem[] AvatarMenuItems { get; set; }
        [Inject] ILocalStorageService LocalStorageService { get; set; }
        [Inject] AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            SetClassMap();
            //
            var user = (await AuthenticationState).User;
            if (user.Identity.IsAuthenticated == true)
            {
                _userName = user.Identity.Name;
                AvatarMenuItems = new AvatarMenuItem[]
                {
                    new() { Key = "center", IconType = "user", Option = "user"},
                    new() { Key = "setting", IconType = "setting", Option = "setting"},
                    new() { IsDivider = true },
                    new() { Key = "logout", IconType = "logout", Option = "logout" }
                };
            }
        }

        protected void SetClassMap()
        {
            ClassMapper
                .Clear()
                .Add("right");
        }

        public async void HandleSelectUser(MenuItem item)
        {
            switch (item.Key)
            {
                case "center":
                    NavigationManager.NavigateTo("/account/center");
                    break;
                case "setting":
                    NavigationManager.NavigateTo("/account/settings");
                    break;
                case "logout":
                    await SignOut();
                    break;
            }
        }
        private async Task SignOut()
        {
            if (await LocalStorageService.ContainKeyAsync("bearerToken"))
            {
                await LocalStorageService.RemoveItemAsync("bearerToken");
                ((AppAuthenticationStateProvider)AuthenticationStateProvider).SignOut();
            }
            StateHasChanged();
            NavigationManager.NavigateTo("/", true);
        }

    }
}