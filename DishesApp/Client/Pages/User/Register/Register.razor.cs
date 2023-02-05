using Client.Static;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using ModelsLibrary;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Client.Pages.User
{
    public partial class Register
    {
        [Inject] HttpClient HttpClient { get; set; }
        [Inject] NavigationManager _navigationManager { get; set; }
        [Inject] SweetAlertService Swal { get; set; }
        private UserDTO _userToRegister = new UserDTO() { UserName = "UserName", EmailAdress = "user@example.com", Password = "Password1!" };
        private bool _registerSuccessful = false;
        private bool _attemptToRegisterFailed = false;
        private string? _attemptToRegisterFailedErrorMessage = String.Empty;

        private async Task RegisterUser()
        {
            _attemptToRegisterFailed = false;
            HttpResponseMessage httpResponseMessage = await HttpClient.PostAsJsonAsync(APIEndpoints.s_register, _userToRegister);
            Console.WriteLine(_userToRegister.UserName + " USERNAME");
            Console.WriteLine(_userToRegister.Password + " Password");
            Console.WriteLine(_userToRegister.EmailAdress + " EMAIL");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                _registerSuccessful = true;
                await Swal.FireAsync($"Hello, {_userToRegister.UserName}!");
                _navigationManager.NavigateTo("/");
            }
            else
            {
                string serverErrorMessages = await httpResponseMessage.Content.ReadAsStringAsync();
                _attemptToRegisterFailedErrorMessage = $"{serverErrorMessages} Change data and try again.";
                _attemptToRegisterFailed = true;
            }
        }
    }
}