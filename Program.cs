using Fluxor;
using JAWSWebApp;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var currentAssembly = typeof(Program).Assembly;
builder.Services.AddFluxor(options => options.ScanAssemblies(currentAssembly));

builder.Services.AddOidcAuthentication(options =>
{
    // 環境変数はセキュリティ上の理由でProgram.csに記載
    if (builder.HostEnvironment.BaseAddress.Contains("localhost"))
    {
        options.ProviderOptions.Authority = "";
        options.ProviderOptions.ClientId = "";
        options.ProviderOptions.RedirectUri = "";
        options.ProviderOptions.PostLogoutRedirectUri = "";
        options.ProviderOptions.ResponseType = "code";
    }
    else if (builder.HostEnvironment.BaseAddress.Contains("develop"))
    {
        options.ProviderOptions.Authority = "";
        options.ProviderOptions.ClientId = "";
        options.ProviderOptions.RedirectUri = "";
        options.ProviderOptions.PostLogoutRedirectUri = "";
        options.ProviderOptions.ResponseType = "code";
    }
    else
    {
        options.ProviderOptions.Authority = "";
        options.ProviderOptions.ClientId = "";
        options.ProviderOptions.RedirectUri = "";
        options.ProviderOptions.PostLogoutRedirectUri = "";
        options.ProviderOptions.ResponseType = "code";
    }
});

await builder.Build().RunAsync();
