using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories;
using OnlineShop.Db.Repositories.Interfaces;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Profiles;
using OnlineShopWebApp.Redis;
using Serilog;
using StackExchange.Redis;
using System.Globalization;
using Prometheus;
using OnlineShopWebApp.ReviewMicroservice.ApiClients;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) => configuration
.ReadFrom.Configuration(context.Configuration)
.Enrich.WithProperty("ApplicationName", "Online Shop"));

var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new ProductProfile());
    mc.AddProfile(new CartProfile());
    mc.AddProfile(new OrderProfile());
    mc.AddProfile(new FavoritesProfile());
    mc.AddProfile(new UserProfile());
    mc.AddProfile(new RoleProfile());
});

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var redisConfiguration = ConfigurationOptions.Parse(builder.Configuration.GetSection("Redis:ConnectionString").Value);
redisConfiguration.AbortOnConnectFail = false; 
redisConfiguration.ConnectTimeout = 50; 
redisConfiguration.SyncTimeout = 50;
redisConfiguration.ReconnectRetryPolicy = new LinearRetry(50);

builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    return ConnectionMultiplexer.Connect(redisConfiguration);
});

builder.Services.AddSingleton<IRedisCacheService, RedisCacheService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

string connection = builder.Configuration.GetConnectionString("online_shop");
builder.Services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(connection));

builder.Services.AddIdentity<User, OnlineShop.Db.Models.Role>(options => {
    // ���������, �������� �� ������� ��������������, ��������:
    options.User.RequireUniqueEmail = true;
})
                .AddEntityFrameworkStores<DatabaseContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromHours(24);
    options.LoginPath = "/Accounts/Login";
    options.LogoutPath = "/Accounts/Logout";
    options.Cookie = new CookieBuilder
    {
        IsEssential = true
    };
});
builder.Services.AddHttpClient();
builder.Services.AddTransient<IProductsRepository, ProductsDbRepository>();
builder.Services.AddTransient<ICartsRepository, CartsDbRepository>();
builder.Services.AddTransient<IOrdersRepository, OrdersDbRepository>();
builder.Services.AddTransient<IFavoritesRepository, FavoritesDbRepository>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddTransient<ImagesProvider>();
builder.Services.AddHttpClient<IReviewsApiClient, ReviewsApiClient>();

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("en-US"),
    };
    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<User>>();
    var roleManager = services.GetRequiredService<RoleManager<OnlineShop.Db.Models.Role>>();
    IdentityInitializer.Initialize(userManager, roleManager);
}

var localizationOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;
app.UseRequestLocalization(localizationOptions);

app.UseSerilogRequestLogging();

app.UseMetricServer();
app.UseHttpMetrics();

app.MapControllerRoute(
    name: "MyArea",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();