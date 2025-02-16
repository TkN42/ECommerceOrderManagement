using BusinessLogic.Services;
using DataAccess;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Presentation.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// DevExpress Blazor bileþenlerini ekleme
builder.Services.AddDevExpressBlazor(options => {
    options.BootstrapVersion = DevExpress.Blazor.BootstrapVersion.v5;
    options.SizeMode = DevExpress.Blazor.SizeMode.Medium;
});

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();


// Servisleri ekleme
builder.Services.AddScoped<DapperDbConnection>(); // DB baðlantýsý için Scoped olarak
builder.Services.AddScoped<IKullaniciRepository, KullaniciRepository>(); // Kullanýcý repo
builder.Services.AddScoped<ISiparisRepository, SiparisRepository>(); // Sipariþ repo
builder.Services.AddScoped<AuthService>(); // Authentication servisi
builder.Services.AddScoped<IUrunRepository, UrunRepository>();
builder.Services.AddScoped<UrunlerService>();

await builder.Build().RunAsync();
