using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Context;
using Core.Services;
using Core.Repository;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DataBaseContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("LabratoaryConnction"));
});
 
builder.Services.AddScoped<IPieceUsageRepository, PieceUsageService>();
builder.Services.AddScoped<IPieceRepository, PieceService>();
builder.Services.AddScoped<ITestRepository, TestService>();
builder.Services.AddScoped<ILabratoaryToolRepository, LabratoaryToolService>();
builder.Services.AddScoped<IProcessRepository, ProcessService>();
builder.Services.AddScoped<ITestRepository, TestService>();
builder.Services.AddScoped<IDefinitionRepository, DefinitionService>();
builder.Services.AddScoped<IUserRepository, UserService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IActionContextAccessor, ActionContextAccessor>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{ 
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); 
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
   
app.Run();
