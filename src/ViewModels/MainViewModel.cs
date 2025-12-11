using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;

namespace EmbededAPI.ViewModels;

public partial  class MainViewModel : ObservableObject
{
    [ObservableProperty] int runState = 1;
    [ObservableProperty] int count = 847;
    [ObservableProperty] double systemEfficiency = 94.2;
    [ObservableProperty] int totalMachines = 25;
    [ObservableProperty] int activeMachines = 23;

    [ObservableProperty] double operationRate;

    [ObservableProperty] int warningCnt = 3;

    [ObservableProperty] double aAreaTemp = 72.4;
    [ObservableProperty] double bAreaTemp = 68.9;
    [ObservableProperty] double cAreaTemp = 85.2;

    [ObservableProperty] double totalPower = 1247.0;
    [ObservableProperty] double productionPercent = 62.0;
    [ObservableProperty] double hvacPercent = 25.0;
    [ObservableProperty] double lightPercent = 13.0;
    public MainViewModel()
    {
        RunState = 1;
        Count = 847;
        SystemEfficiency = 94.2;
        TotalMachines = 25;
        ActiveMachines = 23;
        OperationRate = ((double)ActiveMachines / (double)TotalMachines) * 100;
        WarningCnt = 3;
        AAreaTemp = 72.4;
        BAreaTemp = 68.9;
        CAreaTemp = 85.2;
        TotalPower = 1247.0;
        ProductionPercent = 62.0;
        HvacPercent = 25.0;
        LightPercent = 13.0;
    }

    [RelayCommand]
    private void MachineControl(string state)
    {
        RunState = state.ToUpper() switch
        {
            "START" => 0,
            "STOP" => 1,
            "PAUSE" => 2,
            _ => 99,
        };

        if (RunState == 99)
            Task.Run (async() =>
            {
                Load ();
                await Task.Delay (3000);
                Run ();
            });
    }

    void Run()
    {
        RunState = new Random(2).Next();
        Count = 847;
        SystemEfficiency = 94.2;
        TotalMachines = 25;
        ActiveMachines = 23;
        OperationRate = ((double)ActiveMachines / (double)TotalMachines) * 100;
        WarningCnt = 3;
        AAreaTemp = 72.4;
        BAreaTemp = 68.9;
        CAreaTemp = 85.2;
        TotalPower = 1247.0;
        ProductionPercent = 62.0;
        HvacPercent = 25.0;
        LightPercent = 13.0;
    }

    void Load()
    {
        Count = 0;
        SystemEfficiency = 0;
        TotalMachines = 0;
        ActiveMachines = 0;
        OperationRate = 0;
        WarningCnt = 0;
        AAreaTemp = 0;
        BAreaTemp = 0;
        CAreaTemp = 0;
        TotalPower = 0;
        ProductionPercent = 0;
        HvacPercent = 0;
        LightPercent = 0;
    }
}
