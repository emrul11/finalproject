using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CVBuilder.Persistence;
using CVBuilder.Persistence.Extentions;
using System.Reflection;
using Serilog;
using Serilog.Events;
using CVBuilder.Web;
using CVBuilder.Web.Areas;
using CVBuilder.Web.Areas.Users.Factory;
using CVBuilder.Infrastructure;
using CVBuilder.Application;
using CVBuilder.Domain.Utilities;
using Autofac.Core;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//serilog integration
builder.Host.UseSerilog((ctx, lc) => lc
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(builder.Configuration));


// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
var migrationAssembly = Assembly.GetExecutingAssembly().FullName;


try
{
    //Configure Autofac Start
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        //Module class binding here
        containerBuilder.RegisterModule(new PersistenceModule(connectionString, migrationAssembly));
        containerBuilder.RegisterModule(new WebModule());
        containerBuilder.RegisterModule(new InfrastructureModule());
        containerBuilder.RegisterModule(new ApplicationModule());

        //containerBuilder.RegisterModule(new AreaModule());




    });
    //Configure Autofac End


    //No need this block because it already configured into ApplicationDbContext
    //But some show no ApplicationDbContext found that time need this block
    builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        ///options.JsonSerializerOptions.
    });


    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString, (x) => x.MigrationsAssembly(migrationAssembly)));

    builder.Services.AddDatabaseDeveloperPageExceptionFilter();

    builder.Services.AddIdentity();

     builder.Services.AddControllersWithViews();

    //collect smtp info from Appsetting
    builder.Services.Configure<Smtp>(builder.Configuration.GetSection("Smtp"));
    builder.Services.Configure<SMTPConfigure>(builder.Configuration.GetSection("SMTPConfig"));

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseMigrationsEndPoint();
    }
    else
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
        name: "areas",
        pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

    //app.MapControllerRoute(
    //    name: "areas",
    //        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.MapRazorPages();

    Log.Information("Application Starting...");
    app.Run();
}

catch (Exception ex)
{
    Log.Fatal(ex, "Failed to start applications.");
}
finally
{
    Log.CloseAndFlush();
}

