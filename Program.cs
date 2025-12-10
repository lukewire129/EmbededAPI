using EmbededAPI;
using EmbededAPI.Api;
using EmbededAPI.ViewModels;
using LazyVoom.Core;
using LazyVoom.Hosting.WPF;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder ();
builder.Services.AddSingleton<MainViewModel> ();
builder.Services.AddHostedService<WebHost> ();

var app = builder.BuildApp<App, MainWindow> ();  // 🔥
app.OnStartUpAsync = async provider =>
{
    Voom.Instance
        .WithContainerResolver (vmType =>
        {
            return provider.GetService (vmType) ??
                   ActivatorUtilities.CreateInstance (provider, vmType);
        });
};
// Exit 시 정리
app.OnExitAsync = async provider =>
{
};

app.Run ();
