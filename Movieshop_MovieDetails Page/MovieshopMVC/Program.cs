using Microsoft.EntityFrameworkCore;
using ApplicationCore.Contracts.Services;
using Infrastructure.Services;
using Infrastructure.Data;
using ApplicationCore.Contracts.Repositories;
using Infrastructure.Repositories;





var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUserRepositry, UserRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IMovieService, MovieService>();

//builder.Services.AddScoped<IMovieService, MovieServiceMock>(); # to use mock service instead of real service


builder.Services.AddDbContext<MovieShopDbContext>(options=>{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MovieShopDbConnection"));
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


