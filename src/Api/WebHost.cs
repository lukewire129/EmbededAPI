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
                return Results.Ok (new
                {
                    runState = vm.RunState,
                    count = vm.Count,
                    systemEfficiency = vm.SystemEfficiency,
                    totalMachines = vm.TotalMachines,
                    activeMachines = vm.ActiveMachines,
                    operationRate = vm.OperationRate,
                    warningCnt = vm.WarningCnt,
                    aAreaTemp = vm.AAreaTemp,
                    bAreaTemp = vm.BAreaTemp,
                    cAreaTemp = vm.CAreaTemp,
                    totalPower = vm.TotalPower,
                    productionPercent = vm.ProductionPercent,
                    hvacPercent = vm.HvacPercent,
                    lightPercent = vm.LightPercent,
                });
            });

            _app.MapPost ("/control", ([FromQuery]string state) =>
            {
                var vm = this._provider.GetService<MainViewModel> ();

                vm.MachineControlCommand.Execute (state);
                var _state = vm.RunState switch
                {
                    0 => "가동중",
                    1 => "정지",
                    2 => "일시정지",
                    99 => "데이터를 불러오는 중"
                };
                return Results.Ok($"{_state} 로 변경하였습니다.");
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
