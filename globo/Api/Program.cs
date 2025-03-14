using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MiniValidation;
using System.Net.Cache;
using Microsoft.AspNetCore.Authentication.Cookies;
using ConfArch.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllersWithViews();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(o=>{
    o.Cookie.Name = "_Host-spa";
    o.Cookie.SameSite = SameSiteMode.Strict;
    o.Events.OnRedirectToLogin = (context) => {
        context.Response.StatusCode = StatusCodes.Status403Forbidden;
        //o.AccessDeniedPath = "_Forbidden.cshtml";
        o.LogoutPath = "/Account/LogOff";
        return Task.CompletedTask;
    };
});

builder.Services.AddAuthorization(o => o.AddPolicy("Admin",
   p => p.RequireClaim("role","Admin"))
);

builder.Services.AddDbContext<HouseDbContext>(o => 
    o.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)); 
builder.Services.AddScoped<IHouseRepository, HouseRepository>();
builder.Services.AddScoped<IBidRepository, BidRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseAuthentication();

//app.UseCors(p => p.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod());
//app.UseCors(p=>p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

//app.UseHttpsRedirection();

app.MapHouseEndPoints();

app.MapBidExtensions();

app.UseRouting();
app.UseAuthorization();
app.MapDefaultControllerRoute();
app.MapFallbackToFile("index.html");

app.Run();

