//using Microsoft.AspNetCore.Components.Authorization;
//using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Presentation.Services;
using Presentation.Components;
using Presentation.Client.Pages;
using BusinessLogic.Services;
using DataAccess;
using Presentation;
using Microsoft.AspNetCore.Components.Server;
using Blazored.SessionStorage;

var builder = WebApplication.CreateBuilder(args);

// **Veritaban� Ba�lant�s� ve Identity Yap�land�rmas�**
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddIdentity<IdentityUser, IdentityRole>()
//    .AddEntityFrameworkStores<ApplicationDbContext>()  // EntityFramework ile Identity yap�land�rmas�
//    .AddDefaultTokenProviders(); // Identity servisleri ekleniyor

// **Blazor Bile�enlerini Ekle**
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// **Yetkilendirme**
//builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
//builder.Services.AddAuthorizationCore();

// **Dapper Veritaban� Ba�lant�s�**
builder.Services.AddSingleton<DapperDbConnection>(); // Dapper DB ba�lant�s�

// **Repository ve Servisler**
builder.Services.AddScoped<IKullaniciRepository, KullaniciRepository>(); // Kullan�c� repo
builder.Services.AddScoped<ISiparisRepository, SiparisRepository>();   // Sipari� repo
builder.Services.AddScoped<IUrunRepository, UrunRepository>();         // �r�n repo
builder.Services.AddScoped<UrunlerService>();
builder.Services.AddScoped<AuthService>(); // Authentication servisi
builder.Services.AddHttpContextAccessor(); 
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7155/"); // API adresi
});

builder.Services.Configure<CircuitOptions>(options => { options.DetailedErrors = true; });

//builder.Services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();

// **DevExpress Blazor Ayarlar�**
builder.Services.AddDevExpressBlazor(options => {
    options.BootstrapVersion = DevExpress.Blazor.BootstrapVersion.v5;
    options.SizeMode = DevExpress.Blazor.SizeMode.Medium;
});

// **Di�er Servisler**
builder.Services.AddSingleton<WeatherForecastService>();

// **MVC Ekle (API veya Razor Pages i�in)**
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDistributedMemoryCache(); // E�er ba�ka bir session store kullanm�yorsan
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // 30 dakika boyunca oturum a��k kals�n
    options.Cookie.HttpOnly = true; // G�venlik i�in HttpOnly
    options.Cookie.IsEssential = true; // Zorunlu olarak tan�mlans�n
});

var app = builder.Build();

// **Hata Y�netimi ve Ortam Kontrolleri**
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// **Kimlik Do�rulama ve Yetkilendirme**
app.UseAuthentication();
app.UseAuthorization();

// **Blazor Bile�enlerini Haritalama**
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Counter).Assembly)
    .AllowAnonymous();

// **Uygulamay� �al��t�r**
app.Run();
