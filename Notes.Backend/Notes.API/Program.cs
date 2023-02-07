using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;
using Notes.API;
using Notes.Application;
using Notes.Persistence;
using Notes.API.CustomMiddlewares;
using Swashbuckle.AspNetCore.SwaggerGen;
using Notes.Application.Common;
using Notes.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// builder.Services.AddAutoMapper(config => // ?
// {
// 	config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
// 	config.AddProfile(new AssemblyMappingProfile(typeof(IApplicationContext).Assembly));
// });
builder.Services.AddApplication();
builder.Services.AddPersistance(builder.Configuration);
builder.Services.AddControllers();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://localhost:10001"; // address of IdentityServer
        options.Audience = "notes.api";
    });


builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAll", policy =>
	{
		policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});


// builder.Services.AddOpenApiDocument(settings => // conflicts with Swashbuckle
// {
// 	settings.DefaultReferenceTypeNullHandling = NJsonSchema.Generation.ReferenceTypeNullHandling.NotNull;
// });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddApiVersioning();
builder.Services.AddApiVersioning(opt =>
{
	opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1,0);
	opt.AssumeDefaultVersionWhenUnspecified = true;
	opt.ReportApiVersions = true;
	opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
													new HeaderApiVersionReader("x-api-version"),
													new MediaTypeApiVersionReader("x-api-version"));
});

// builder.Services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");
builder.Services.AddVersionedApiExplorer(setup =>
{
	setup.GroupNameFormat = "'v'VVV";
	setup.SubstituteApiVersionInUrl = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

builder.Services.AddSwaggerGen(c =>
{
	// добавление возможности отображнеия "///"-коментариев(summary) в swagger-UI
	var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
	c.IncludeXmlComments(xmlPath);
	
	c.SchemaFilter<RequireNonNullablePropertiesSchemaFilter>();
	c.SupportNonNullableReferenceTypes(); // Sets Nullable flags appropriately.              
	// c.UseAllOfToExtendReferenceSchemas(); // Allows $ref enums to be nullable
	// c.UseAllOfForInheritance();  // Allows $ref objects to be nullable
});

//builder.Services.AddHttpContextAccessor();
//builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

// https://github.com/RicoSuter/NSwag/wiki/AspNetCore-Middleware possible alternative 
app.UseSwagger();
app.UseSwaggerUI(config =>
{
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    foreach (var description in provider.ApiVersionDescriptions.Reverse())
    {
        config.SwaggerEndpoint(
            $"/swagger/{description.GroupName}/swagger.json",
            description.GroupName.ToUpperInvariant());
        config.RoutePrefix = string.Empty; // making access to swagger with / after localhost
    }

});

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowAll");

app.UseAuthentication(); // for identity server
app.UseAuthorization(); //

app.UseApiVersioning(); // enabling api versions
app.UseMiddleware<CustomExceptionHandler>();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider
        .GetRequiredService<ApplicationDbContext>();
	try
	{
		DbInitializer.Initialize(context);
	}
	catch (Exception)
	{
		scope.ServiceProvider
			.GetRequiredService<ILogger<Program>>()
			.LogError("Failed to init the DB");
	}
}

app.Run();