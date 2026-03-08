using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

using LuftBornTask.Infrastructure.Persistence;
using LuftBornTask.Infrastructure.Repositories;
using LuftBornTask.Infrastructure.Security;

using LuftBornTask.Domain.Interfaces;
using LuftBornTask.Application.Interfaces;
using LuftBornTask.Application.Services;

var builder = WebApplication.CreateBuilder(args);


// ===============================
// Connection String Decryption
// ===============================

var encryptedConnection = builder.Configuration.GetConnectionString("DefaultConnection");
var connectionString = EncryptionHelper.Decrypt(encryptedConnection);


// ===============================
// Database
// ===============================

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString,
        b => b.MigrationsAssembly("LuftBornTask.Infrastructure")));


// ===============================
// Dependency Injection
// ===============================

builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();

// JWT Service
builder.Services.AddScoped<JwtService>();


// ===============================
// Controllers
// ===============================

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


// ===============================
// Swagger + JWT Support
// ===============================

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "LuftBorn Task API",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Enter JWT Token like: Bearer {token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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


// ===============================
// Authentication (Local JWT)
// ===============================

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],

        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
        )
    };
});

builder.Services.AddAuthorization();


// ===============================
// CORS (Angular)
// ===============================

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});


// ===============================
// Build App
// ===============================

var app = builder.Build();


// ===============================
// Middleware Pipeline
// ===============================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngular");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();