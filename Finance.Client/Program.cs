using System.Globalization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using MudBlazor.Translations;
using Microsoft.JSInterop;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddLocalization();

builder.Services.AddMudServices();
builder.Services.AddMudTranslations();

// CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");

// Console.WriteLine("Current Culture: " + CultureInfo.CurrentCulture.Name + " - " + CultureInfo.CurrentCulture.DisplayName);

// await builder.Build().RunAsync();

var host = builder.Build();

const string defaultCulture = "en-US";

var js = host.Services.GetRequiredService<IJSRuntime>();
var result = await js.InvokeAsync<string>("blazorCulture.get");
var culture = CultureInfo.GetCultureInfo(result ?? defaultCulture);

if (result == null)
{
  await js.InvokeVoidAsync("blazorCulture.set", defaultCulture);
}

CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

Console.WriteLine("Current Culture: " + CultureInfo.CurrentCulture.Name + " - " + CultureInfo.CurrentCulture.DisplayName);

await host.RunAsync();
