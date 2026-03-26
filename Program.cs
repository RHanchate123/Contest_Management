using Contest_Management.API.Interfaces;
using Contest_Management.API.Middleware;
using Contest_Management.Interfaces;
using Contest_Management.Model;
using Contest_Management.Seeders;
using Contest_Management.Services;
using ContestSystem.API.DTOs;
using ContestSystem.API.Repositories;
using IdentityApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using PrismatchMiddleware.API.JWT;
using PrismatchMiddleware.API.Services;
using Serilog;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Register Application Services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
builder.Services.AddTransient<JwtConfiguration>();
builder.Services.AddTransient<TokenService>();



//builder.Services.AddScoped<UserManager<ApplicationUser>>();
builder.Services.AddScoped<IUserInterface, UserService>();
builder.Services.AddScoped<ContestRepository>();
builder.Services.AddScoped<IContestInterface, ContestService>();
// Add DbContext and Identity services
builder.Services.AddDbContext<CMSDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<CMSDbContext>()
    .AddDefaultTokenProviders();
#endregion

builder.Host.UseSerilog((context, config) =>
{
    config
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console();
});


#region RateLimiter
var readLimiter = "readLimiter";
var writeLimiter = "writeLimiter";
builder.Services.AddRateLimiter(options =>
{
    // Read APIs (GET requests)
    options.AddFixedWindowLimiter(policyName: readLimiter, limiterOptions =>
    {
        limiterOptions.PermitLimit = 100;                // 100 requests
        limiterOptions.Window = TimeSpan.FromMinutes(1); // per minute
        limiterOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        limiterOptions.QueueLimit = 10;
    });

    // Write APIs (POST/PUT/DELETE)
    options.AddFixedWindowLimiter(policyName: writeLimiter, limiterOptions =>
    {
        limiterOptions.PermitLimit = 2;                  // 10 requests
        limiterOptions.Window = TimeSpan.FromSeconds(10); // per 10 sec
        limiterOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        limiterOptions.QueueLimit = 2;
    });

    options.OnRejected = async (context, token) =>
    {
        context.HttpContext.Response.StatusCode = 429;

        await context.HttpContext.Response.WriteAsJsonAsync(new
        {
            message = "Too many requests. Please try again later."
        });
    };

});
#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseRateLimiter();


#region Database Migration
///*The below code will run migrations on its own, create DB, tables etc*/
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CMSDbContext>();
    await dbContext.Database.MigrateAsync();
}

await Seeder.SeedAsync(app.Services);
#endregion

#region AllowAnonymous Methods
app.MapGet("/", () => "Contest_Management Version 1.0.0.0");


app.MapPost("/registeruser", async (RegisterUserDto registerUserDTO, IUserInterface userService) =>
{
    var result = await userService.RegisterUserAsync(registerUserDTO);
    return result;
})
    .AllowAnonymous()
    .RequireRateLimiting(writeLimiter)
    .WithName("registeruser");

app.MapPost("/login", async (LoginDto loginDTO, TokenService tokenService, IUserInterface userService) =>
{
    var user = await userService.LoginAsync(loginDTO);
    var token = await tokenService.GenerateToken(user);
    return Results.Ok(new
    {
        token,
        statuscode = 200,
        success = true
    });
})
    .AllowAnonymous();


//app.MapGet("/", () => "WORKING");


app.MapGet("/contests", async (IContestInterface contestInterface) =>
{
    var contests = await contestInterface.GetAllAsync();
    return Results.Ok(contests);
});
    
#endregion

app.Run();