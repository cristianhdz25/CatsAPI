using CatAPI.BC.Utilities;
using CatAPI.BW.Interfaces.BW;
using CatAPI.BW.Interfaces.DA;
using CatAPI.BW.Interfaces.SG;
using CatAPI.BW.UC;
using CatAPI.DA.Actions;
using CatAPI.DA.Context;
using CatAPI.SG;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register HttpClient
builder.Services.AddHttpClient();

// Get JWT settings
var jwtSettings = builder.Configuration.GetSection("Jwt");
var secretKey = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"]); // Convertir la clave secreta a bytes

// Register services
builder.Services.AddSingleton<PasswordHasher>();

builder.Services.AddSingleton<JwtGenerator>(provider =>
{
    return new JwtGenerator(
        jwtSettings["SecretKey"],
        jwtSettings.GetValue<int>("ExpiryInMinutes")
    );
});

builder.Services.AddTransient<IManageUserDA, ManageUserDA>();
builder.Services.AddTransient<IAuthUserBW, AuthUserBW>();
builder.Services.AddTransient<IManageCatDA, ManageCatDA>();
builder.Services.AddTransient<ITheCatAPISG, TheCatAPISG>();

// Configuration of the authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(secretKey), // Usar la clave secreta
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true, // Validar la expiración del token
        ClockSkew = TimeSpan.Zero // No permitir desfase de tiempo
    };
});

// Configuration of the database
builder.Services.AddDbContext<CatAPIDbContext>(options =>
{
    var connectionString = "Server=localhost;Database=CatsDatabase;Trusted_Connection=True;TrustServerCertificate=true;";
    options.UseSqlServer(connectionString);
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var catBreedService = scope.ServiceProvider.GetRequiredService<IManageCatDA>();
    await catBreedService.LoadCatBreedsAsync();
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