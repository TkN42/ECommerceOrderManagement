using BusinessLogic.Services;
using DataAccess;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Presentation.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// DevExpress Blazor bile�enlerini ekleme
builder.Services.AddDevExpressBlazor(options => {
    options.BootstrapVersion = DevExpress.Blazor.BootstrapVersion.v5;
    options.SizeMode = DevExpress.Blazor.SizeMode.Medium;
});

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();


// Servisleri ekleme
builder.Services.AddScoped<DapperDbConnection>(); // DB ba�lant�s� i�in Scoped olarak
builder.Services.AddScoped<IKullaniciRepository, KullaniciRepository>(); // Kullan�c� repo
builder.Services.AddScoped<ISiparisRepository, SiparisRepository>(); // Sipari� repo
builder.Services.AddScoped<AuthService>(); // Authentication servisi
builder.Services.AddScoped<IUrunRepository, UrunRepository>();
builder.Services.AddScoped<UrunlerService>();

await builder.Build().RunAsync();
