using Microsoft.Extensions.DependencyInjection;
using Tasks.DAL;
using Tasks.DAL.Repositories;
using Tasks.DAL.Repositories.Interface;
using Tasks.Models;
using Tasks.PostgresMigrate;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserMockData>();


//var connectionString = builder.Configuration.GetConnectionString("NpgsqlConnectionString");
var connectionString = "Server=localhost;Port=5432;Database=c#;User Id=postgres;Password=root;";
//PostgresMigrator.Migrate(connectionString);

//builder.Services.AddScoped<IUserRepository, UserRepository>(x => new UserRepository(connectionString));


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
    pattern: "{controller=UserList}/{action=Index}/{id?}");

app.Run();

