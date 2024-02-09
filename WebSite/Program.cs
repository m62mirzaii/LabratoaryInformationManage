using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Context;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using DataAccessLayer.Config;

var builder = WebApplication.CreateBuilder(args); 
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DataBaseContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("LabratoaryConnction"));
}); 
 

builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddMyDependencyGroup();
builder.Services.AddMemoryCache();
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


builder.Services.AddMvc(m => m.EnableEndpointRouting = false);
builder.Services.AddMemoryCache();
builder.Services.AddCors(o => o.AddPolicy("AllowAllOrigins", builder =>
{
    builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
}));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); 
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseMvc();
app.UseRouting();

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
app.UseAuthorization();
 
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseCors();
app.Run();
