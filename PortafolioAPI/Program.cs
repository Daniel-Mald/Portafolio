using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using Pomelo.EntityFrameworkCore.MySql.Internal;
using PortafolioAPI.Helpers;
using PortafolioAPI.Models.Entities;
using PortafolioAPI.Repositories;
using PortafolioAPI.Repositories.Interfaces;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

//builder.Services.AddOpenApi();
#region BearerAuthentication
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{

    options.RequireHttpsMetadata = true;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
        ValidateIssuerSigningKey = true
    };
});
#endregion

#region Swagger
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PortafolioAPI", Version = "v1",
       
    });
    
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Ingresa el Token Bearer creado por la aplicacion",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
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
            Array.Empty<string>()
        },
        
    });
});
#endregion


#region Connection
string connectionString = builder.Configuration["ConnectionStrings:PortafolioConnection"]!;
builder.Services.AddDbContext<LabsystePortafolioContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});
#endregion

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IWorkRepository, WorkRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddTransient<EncryptClass>();
var app = builder.Build();

// Configure the HTTP request pipeline.

    //app.MapOpenApi();


app.UseHttpsRedirection();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PortafolioAPI v1");
        
        c.RoutePrefix = "swagger";
    });

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
