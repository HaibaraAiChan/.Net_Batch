using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MovieshopMVC.Services;
using MovieshopMVC.Middlewares;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole;
using Serilog.Sinks.File;
using Serilog.Extensions.Logging;
using Serilog.AspNetCore; // Add this using directive


var builder = WebApplication.CreateBuilder(args);

// Serilog configuration
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.Logger(lc=>lc.Filter.ByIncludingOnly(le => le.Level == LogEventLevel.Error )
        .WriteTo.File(
        path: Path.Combine("Logs",$"{DateTime.Now:yyyy-MM-dd}","Error.log"), // separate folder for error logs
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 7 // keep logs for 7 days
        ))
    .WriteTo.Logger(lc => lc
    .Filter.ByIncludingOnly(le => le.Level == LogEventLevel.Information )
        .WriteTo.File(  
        path: Path.Combine("Logs", $"{DateTime.Now:yyyy-MM-dd}","Info.log"), // separate folder for info logs
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 7 // keep logs for 7 days
        ))
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Host.UseSerilog(); // Use Serilog for logging
//builder.Host.UseSerilog((ctx, lc) => lc
//    .ReadFrom.Configuration(ctx.Configuration)
//    .Enrich.FromLogContext()
//);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUserRepositry, UserRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<ICurrentUser,CurrentUser>();
builder.Services.AddHttpContextAccessor();

//builder.Services.AddScoped<IMovieService, MovieServiceMock>(); # to use mock service instead of real service


builder.Services.AddDbContext<MovieShopDbContext>(options=>{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MovieShopDbConnection"));
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.Cookie.Name = "MovieShopAuthCookie";
    options.ExpireTimeSpan = TimeSpan.FromHours(2);
    options.LoginPath = "/account/Login";
});


var app = builder.Build();
Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Logs", $"{DateTime.Now:yyyy-MM-dd}"));

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // production scenarios
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseMovieShopExceptionMiddleware(); // development scenarios, you can see the detailed exception page
}
app.UseSerilogRequestLogging(); // Serilog request logging middleware


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();




