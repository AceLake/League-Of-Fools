using League_Of_Fools.Service;
using NLog;
using NLog.Web;
using LogLevel = NLog.LogLevel;

//Setting up logging
var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

logger.Info("init logger");



var builder = WebApplication.CreateBuilder(args);

//this is where we add cookies
builder.Services.AddDistributedMemoryCache();

builder.Logging.ClearProviders();

//using NLog
builder.Host.UseNLog();


builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(1000);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddMvc();
// Add services to the container.
builder.Services.AddControllersWithViews();



builder.Services.AddTransient<IChampionService, ChampionService>();
builder.Services.AddSingleton<ISummonerService, SummonerService>();
builder.Services.AddSingleton<IAccountService, AccountService>();
builder.Services.AddSingleton<AccountDAO, AccountDAO>();


builder.Services.AddHttpClient("GetSummonerByNameAndTagLine", client =>
{
    // Configure your HttpClient here if needed
});

builder.Services.AddHttpClient("GetSummonerByPUUID", client =>
{
    // Configure your HttpClient here if needed
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
