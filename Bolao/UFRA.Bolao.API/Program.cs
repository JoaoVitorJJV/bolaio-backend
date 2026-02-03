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
using Microsoft.OpenApi;
using System.Reflection.Metadata;
using System.Text;
using UFRA.Bolaio.API.Data;
using UFRA.Bolaio.API.Endpoints;
using UFRA.Bolaio.API.Middlewares;
using UFRA.Bolao.API.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Ufra - BolaoIo",
        Version="v1",
        Description="teste"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below.",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });
    options.EnableAnnotations();
    //options.AddSecurityRequirement(doc => new OpenApiSecurityRequirement
    //{
    //    {
    //        new OpenApiSecuritySchemeReference("Bearer"),
    //        new List<string>()
    //    }
    //});
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});



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
builder.Services.AddScoped<IBolaoRepository, BolaoRepository>();
builder.Services.AddScoped<IBolaoService,BolaoService>();
builder.Services.AddScoped<IBolaoQueries,BolaoQueriesService>();


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

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseExceptionHandler();

if (emDebug)
{
    app.MapOpenApi();
    app.UseSwagger();
    //app.UseSwaggerUI(options =>
    //{
    //    options.SwaggerEndpoint("/openapi/v1.json", "UFRA - BolaoIo");
    //    options.RoutePrefix = "swagger";
        
    //});
}


app.UseAuthentication();

app.UseAuthorization();

app.MapAuthEndpoints();
app.MapCarteiraEndpoints();
app.MapBolaoEndpoints();

app.Run();