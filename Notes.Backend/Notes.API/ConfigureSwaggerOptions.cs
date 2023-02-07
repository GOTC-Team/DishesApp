using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Notes.API;

public class ConfigureSwaggerOptions: IConfigureOptions<SwaggerGenOptions>
{
    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider descriptionProvider)
    {
        _descriptionProvider = descriptionProvider;
    }
    
    private readonly IApiVersionDescriptionProvider _descriptionProvider;
    
    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _descriptionProvider.ApiVersionDescriptions)
        {
            var apiVersion = description.ApiVersion.ToString();
            options.SwaggerDoc(description.GroupName, new OpenApiInfo
            {
                Version = apiVersion,
                Title = $"Notes API {apiVersion}",
                Description = "Advance API ASP.NET Core Web API example",
                TermsOfService = new Uri("https://google.com/"),
                Contact = new OpenApiContact{ Url = new Uri("https://google.com/"), Email = String.Empty, Name = "GG Chat"},
                License = new OpenApiLicense{ Name = "My tg channel", Url = new Uri("https://t.me/fake")}
            });
            
            // enabling authorization in swagger
            options.AddSecurityDefinition($"AuthToken {apiVersion}", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer",
                Name = "Authorization",
                Description = "Authorization token"
            });
            
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference()
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = $"AuthToken {apiVersion}"
                        }
                    },
                    new string[]{}
                }
            });
            options.CustomOperationIds(apiDescription => apiDescription.TryGetMethodInfo(out MethodInfo methodInfo) ? methodInfo.Name : null); // operationid = method name
        }
    }
}