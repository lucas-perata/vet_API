using API.Data;
using API.Entities;
using API.Entities.Identity;
using API.Extensions;
using API.Identity;
using API.Interfaces;
using API.Repository;
using API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddScoped<IPetRepository, IPetRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddDbContext<AppIdentityDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
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


var identityContext = services.GetRequiredService<AppIdentityDbContext>();
var dataContext = services.GetRequiredService<DataContext>();
var userManager = services.GetRequiredService<UserManager<AppUser>>();
try
{
    await identityContext.Database.MigrateAsync();
    await dataContext.Database.MigrateAsync();
    
    await AppIdentityDbContextSeed.SeedUsersAsync(userManager);

     if (!dataContext.Pets.Any())
    {
        var user = await userManager.FindByEmailAsync("juan@test.com");

        dataContext.Pets.AddRange(
            new Pet { Id = 1, Name = "Buddy", OwnerId = user.Id },
            new Pet { Id = 2, Name = "Lucy", OwnerId = user.Id }
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
