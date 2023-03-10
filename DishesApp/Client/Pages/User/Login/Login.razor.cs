using AntDesign;
using Blazored.LocalStorage;
using Client.Models;
using Client.Providers;
using Client.Static;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using ModelsLibrary;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Client.Pages.User
{
    public partial class Login
    {
        [Inject] HttpClient HttpClient { get; set; }
        [Inject] ILocalStorageService LocalStorageService { get; set; }
        [Inject] AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject] SweetAlertService Swal { get; set; }
        [Inject] NavigationManager _navigationManager { get; set; }

        private UserDTO _userToSignIn = new UserDTO();
        private bool _signInSuccessful = false;
        private bool _attemptToSignInFailed = false;

        private async Task SignInUser()
        {
            _attemptToSignInFailed = false;
            HttpResponseMessage httpResponseMessage = await HttpClient.PostAsJsonAsync(APIEndpoints.s_signIn, _userToSignIn);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string jsonWebToken = await httpResponseMessage.Content.ReadAsStringAsync();
                await LocalStorageService.SetItemAsync("bearerToken", jsonWebToken);
                await ((AppAuthenticationStateProvider)AuthenticationStateProvider).SignIn();

                HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", jsonWebToken);
                _signInSuccessful = true;
                await Swal.FireAsync($"Hello, {_userToSignIn.UserName}!");
                _navigationManager.NavigateTo("/");
            }
            else
            {
                _attemptToSignInFailed = true;
            }
        }
    }
}