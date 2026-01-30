using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi; // Adicione este using no topo do arquivo, se ainda não existir
using System.Text;
using UFRA.Bolaio.API.Data;
using UFRA.Bolaio.API.Endpoints;
using UFRA.Bolaio.API.Middlewares;
using UFRA.Bolao.API.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options => {
    options.UseNpgsql(builder.Configuration.GetConnectionString("banco")).UseSnakeCaseNamingConvention();

    if (builder.Environment.IsDevelopment())
    {
        options.EnableSensitiveDataLogging();
        options.EnableDetailedErrors();
    }
});
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICarteiraService, CarteiraAppService>();
builder.Services.AddScoped<AuthAppService>();
builder.Services.AddScoped<ICarteiraRepository, CarteiraRepository>();

var jwtKey = builder.Configuration["Jwt:Key"];
var keyBytes = Encoding.ASCII.GetBytes(jwtKey);

bool emDebug = builder.Environment.IsDevelopment();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {

        ValidateIssuerSigningKey = !emDebug,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),

               
        ValidateIssuer = !emDebug,
        ValidIssuer = builder.Configuration["Jwt:Issuer"], // back producao

        ValidateAudience = !emDebug,
        ValidAudience = builder.Configuration["Jwt:Audience"], // front producao

        
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});


builder.Services.AddAuthorization();


var app = builder.Build();

 
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();


app.MapAuthEndpoints();
app.MapCarteiraEndpoints();


app.Run();
