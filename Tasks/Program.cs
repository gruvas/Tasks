using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Task.DAL.EF;
using Tasks.DAL;
using Tasks.DAL.Repositories;
using Tasks.DAL.Repositories.Interface;
using Tasks.Models;
using Tasks.PostgresMigrate;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


var dbType = builder.Configuration["DbConfig:Type"];
string connectionString = builder.Configuration["ConnectionString:NpgsqlConnectionString"];

switch (dbType)
{
    case "Postgres":
        PostgresMigrator.Migrate(connectionString);
        builder.Services.AddScoped<IUserRepository, UserRepository>(x => new UserRepository(connectionString));
        builder.Services.AddScoped<ITaskRepository, TaskRepository>(x => new TaskRepository(connectionString));
        builder.Services.AddScoped<IContractorInitiatorRepository, ContractorInitiatorRepository>(x => new ContractorInitiatorRepository(connectionString));
    break;
    case "EF":
        PostgresMigrator.Migrate(connectionString);
        builder.Services.AddDbContext<PostgreeContext>(options =>
            options.UseNpgsql(connectionString));
        builder.Services.AddScoped<IUserRepository, UserRepositoryEF>();
        builder.Services.AddScoped<ITaskRepository, TaskRepositoryEF>();
        builder.Services.AddScoped<IContractorInitiatorRepository, ContractorInitiatorRepositoryEF>();
        break;
}

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

//app.UseExceptionHandler("/Error");
//app.UseStatusCodePagesWithRedirects("/Error/{0}");

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=UserList}/{action=Index}/{id?}");

app.Run();

