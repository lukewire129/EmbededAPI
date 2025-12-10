using EmbededAPI.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EmbededAPI.Api
{
    public class WebHost : IHostedService
    {
        private readonly IServiceProvider _provider;
        WebApplication _app = null;
        public WebHost(IServiceProvider provider)
        {
            this._provider = provider;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var builder = WebApplication.CreateBuilder ();

            _app = builder.Build ();

            _app.MapGet ("/data", () =>
            {
                var vm = this._provider.GetService<MainViewModel> ();
                return Results.Ok ($"현재 Data는 {vm.Text} 입니다.");
            });
            _app.MapPost ("/data", ([FromQuery]string text) =>
            {
                var vm = this._provider.GetService<MainViewModel> ();
                vm.UpdateTextCommand.Execute (text);
                return Results.Ok($"{text} 로 변경하였습니다.");
            });

            _app.Run ("http://127.0.0.1:8081");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_app != null)
            {
                await _app.StopAsync (cancellationToken);
                await _app.DisposeAsync ();
            }
        }
    }
}
