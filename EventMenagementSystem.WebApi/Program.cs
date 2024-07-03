<<<<<<< HEAD
﻿using EventManagementSystem.Repositories;
using EventManagementSystem.Services;
using EventManagementSystem.UnitOfWork;
using EventMenagementSystem.EntityFramework.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Security.Claims;
using System.Text;
=======
using EventMenagementSystem.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using EventManagementSystem.Repositories;
using EventManagementSystem.UnitOfWork;
>>>>>>> bd3ff6bf1380c218b964be0c39adae59c95b979b

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
<<<<<<< HEAD
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Preserve reference loops in JSON serialization
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });
=======
builder.Services.AddControllers();
>>>>>>> bd3ff6bf1380c218b964be0c39adae59c95b979b

// Register DbContext with the dependency injection container
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Register repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();

// Register UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

<<<<<<< HEAD
// Register services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IReviewService, ReviewService>();

// Register AuthorizationController as a service
builder.Services.AddScoped<AuthorizationController>();

// Configure JWT authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"])),
            ValidateIssuer = false,
            ValidateAudience = false
        };

        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = async context =>
            {
                var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();

                // Extract username from validated token
                var username = context.Principal.Identity.Name;

                // Fetch user details from database using username
                var user = await userService.GetUserByUsername(username);
                if (user == null)
                {
                    context.Fail("Unauthorized");
                    return;
                }

                // Add user ID as claim for further use
                var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
                claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()));
            }
        };
    });



// Add session services
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost4200",
        builder => builder
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()); // Allows sending credentials (e.g., JWT token)
});

=======
>>>>>>> bd3ff6bf1380c218b964be0c39adae59c95b979b
// Add Swagger services
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
<<<<<<< HEAD
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
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
        }
    });
=======
>>>>>>> bd3ff6bf1380c218b964be0c39adae59c95b979b
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

<<<<<<< HEAD
// Apply CORS policy
app.UseCors("AllowLocalhost4200");

app.UseAuthentication();
app.UseAuthorization();

// Use session
app.UseSession();

=======
app.UseAuthorization();

>>>>>>> bd3ff6bf1380c218b964be0c39adae59c95b979b
// Map the default controller route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
<<<<<<< HEAD
=======


>>>>>>> bd3ff6bf1380c218b964be0c39adae59c95b979b
