using MudBlazor.Services;
using System.Globalization;
using MudBlazor.Translations;
using Finance.Config;
using Finance.Pages;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

AppOptions appOptions = builder.AppOptionsConfigure();

// builder.Services.Configure<RequestLocalizationOptions>(options =>
// {
//     var supportedCultures = new[]
//     {
//         new CultureInfo("en-US"),
//         new CultureInfo("pt-BR"),
//         new CultureInfo("es")
//     };

//     options.DefaultRequestCulture = new RequestCulture(supportedCultures[1]);
//     options.SupportedCultures = supportedCultures;
//     options.SupportedUICultures = supportedCultures;
// });

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddControllers();

// Add MudBlazor services
builder.Services.AddMudServices();
builder.Services.AddMudTranslations();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(appOptions.SupportedCultures[1])
    .AddSupportedCultures(appOptions.SupportedCultures)
    .AddSupportedUICultures(appOptions.SupportedCultures);

app.UseRequestLocalization(localizationOptions);

// CultureInfo.CurrentCulture = new CultureInfo("pt-BR");
Console.WriteLine($"Cultures supported: {string.Join(", ", appOptions.SupportedCultures)}");
Console.WriteLine($"Culture: {CultureInfo.CurrentCulture.Name}");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

    app.UseHttpsRedirection();
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapControllers();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
