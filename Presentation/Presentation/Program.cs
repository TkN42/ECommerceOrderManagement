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

// **Veritabaný Baðlantýsý ve Identity Yapýlandýrmasý**
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddIdentity<IdentityUser, IdentityRole>()
//    .AddEntityFrameworkStores<ApplicationDbContext>()  // EntityFramework ile Identity yapýlandýrmasý
//    .AddDefaultTokenProviders(); // Identity servisleri ekleniyor

// **Blazor Bileþenlerini Ekle**
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// **Yetkilendirme**
//builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
//builder.Services.AddAuthorizationCore();

// **Dapper Veritabaný Baðlantýsý**
builder.Services.AddSingleton<DapperDbConnection>(); // Dapper DB baðlantýsý

// **Repository ve Servisler**
builder.Services.AddScoped<IKullaniciRepository, KullaniciRepository>(); // Kullanýcý repo
builder.Services.AddScoped<ISiparisRepository, SiparisRepository>();   // Sipariþ repo
builder.Services.AddScoped<IUrunRepository, UrunRepository>();         // Ürün repo
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

// **DevExpress Blazor Ayarlarý**
builder.Services.AddDevExpressBlazor(options => {
    options.BootstrapVersion = DevExpress.Blazor.BootstrapVersion.v5;
    options.SizeMode = DevExpress.Blazor.SizeMode.Medium;
});

// **Diðer Servisler**
builder.Services.AddSingleton<WeatherForecastService>();

// **MVC Ekle (API veya Razor Pages için)**
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDistributedMemoryCache(); // Eðer baþka bir session store kullanmýyorsan
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // 30 dakika boyunca oturum açýk kalsýn
    options.Cookie.HttpOnly = true; // Güvenlik için HttpOnly
    options.Cookie.IsEssential = true; // Zorunlu olarak tanýmlansýn
});

var app = builder.Build();

// **Hata Yönetimi ve Ortam Kontrolleri**
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

// **Kimlik Doðrulama ve Yetkilendirme**
app.UseAuthentication();
app.UseAuthorization();

// **Blazor Bileþenlerini Haritalama**
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Counter).Assembly)
    .AllowAnonymous();

// **Uygulamayý Çalýþtýr**
app.Run();
