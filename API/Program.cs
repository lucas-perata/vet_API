using System.Text.Json.Serialization;
using API.Data;
using API.Entities;
using API.Entities.Identity;
using API.Extensions;
using API.Helpers;
using API.Identity;
using API.Interfaces;
using API.Repository;
using API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.Converters.Add(new UtcDateTimeConverter());
    });
builder.Services.AddIdentityServices(builder.Configuration);
/* builder.Services.AddControllers().AddJsonOptions(x => 
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve); */

// REPOS 
builder.Services.AddScoped<PetRepository>();
builder.Services.AddScoped<MedicalHistoryRepository>();
builder.Services.AddScoped<AdoptionRepository>(); 
builder.Services.AddScoped<ServiceRepository>(); 
builder.Services.AddScoped<AppointmentRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<ITokenService, TokenService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // Define the BearerAuth scheme
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer" // must be lowercase
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;  



var dataContext = services.GetRequiredService<DataContext>();
var userManager = services.GetRequiredService<UserManager<AppUser>>();
try
{
    await dataContext.Database.MigrateAsync();
    
    await AppIdentityDbContextSeed.SeedUsersAsync(userManager);

     if (!dataContext.Services.Any())
    {
        
        dataContext.Services.AddRange(
            new Service { Id = 1, Name = "Service 1", Description = "Description 1", Price = 100.0f, Duration = 60 },
            new Service { Id = 2, Name = "Service 2", Description = "Description 2", Price = 200.0f, Duration = 120 },
            new Service { Id = 3, Name = "Service 3", Description = "Description 3", Price = 300.0f, Duration = 180 }
        );
        await dataContext.SaveChangesAsync();
    }
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration");

}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
