using BlazorSignalrOrleans.Grains.Interfaces.Observers;
using BlazorSignalrOrleans.Server.Data;
using BlazorSignalrOrleans.Server.Hubs;
using BlazorSignalrOrleans.Server.Models;
using BlazorSignalrOrleans.Server.Observers;
using BlazorSignalrOrleans.Server.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentityServer()
    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

builder.Services.AddAuthentication()
    .AddIdentityServerJwt();

builder.Services.AddSignalR();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Async(x => x.Console())
    .CreateLogger();

builder.Logging.AddSerilog();

//builder.Host.UseSerilog();

builder.Host.UseOrleansClient(clientBuilder => clientBuilder.UseLocalhostClustering());

builder.Services.AddSingleton<IChatObserver, ChatObserver>();

builder.Services.AddHostedService<ChatObserverHostedService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.MapHub<ChatHub>("/hubs/chathub");

var bundledMode = builder.Configuration.GetValue<bool>("BundledMode");

if (bundledMode)
{
    await BlazorSignalrOrleans.Silo.Program.StartHost();
}

var appLifetime = app.Services.GetRequiredService<IHostApplicationLifetime>();
appLifetime.ApplicationStopping.Register(OnShutdown);

app.Run();

static async void OnShutdown()
{
    await BlazorSignalrOrleans.Silo.Program.StopHost();
}