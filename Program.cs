using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using ShiipingAPI.Data;
using ShiipingAPI.Migrations;
using System.Reflection;
using ShiipingAPI.Services;

var builder = WebApplication.CreateBuilder(args);
var SqlConnectingString = builder.Configuration["SqlConnectingString"];


builder.Services.AddScoped<IPortService, PortService>();
builder.Services.AddScoped<IShipService, ShipService>();

/*builder.Services.AddDbContext<ShiipingAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ShiipingAPIContext") ?? throw new InvalidOperationException("Connection string 'ShiipingAPIContext' not found."))); */

builder.Services.AddDbContext<ShiipingAPIContext>(options => options.UseSqlServer(SqlConnectingString ?? throw new InvalidOperationException("Connection string 'ShiipingAPIContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Shipping API",
        Description = "An ASP.NET Core Web API for managing Shipping items",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Shiiping Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Shiiping License",
            Url = new Uri("https://example.com/license")
        }
    });

    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        PortSeedData.Initialize(services);
        ShipSeedData.Initialize(services);
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
