using AntDesign.ProLayout;
using Blazored.LocalStorage;
using Client.Providers;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddAntDesign();
            builder.Services.Configure<ProSettings>(x =>
            {
                x.Title = "Smart Cooker";
                x.NavTheme = "light";
                x.Layout = "mix";
                x.PrimaryColor = "daybreak";
                x.ContentWidth = "Fluid";
                x.HeaderHeight = 64;
                x.FixedHeader = true;
                x.FixSiderbar = true;
                x.MenuRender = true;
                x.MenuHeaderRender = true;
                x.FooterRender = true;
                x.HeaderRender = true;
            });
            // Authorization services
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AppAuthenticationStateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider>(provider
                => provider.GetRequiredService<AppAuthenticationStateProvider>());
            // Cute UI
            builder.Services.AddSweetAlert2();
            // Cors Problem
            await builder.Build().RunAsync();
        }
    }
}