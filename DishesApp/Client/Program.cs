using AntDesign.ProLayout;
using Client.Services;
using CurrieTechnologies.Razor.SweetAlert2;
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
            builder.Services.AddScoped<IChartService, ChartService>();
            builder.Services.AddScoped<IProjectService, ProjectService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IProfileService, ProfileService>();
            builder.Services.AddSweetAlert2();
            await builder.Build().RunAsync();
        }
    }
}