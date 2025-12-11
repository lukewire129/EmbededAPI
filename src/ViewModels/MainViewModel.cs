using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace EmbededAPI.ViewModels;

public partial  class MainViewModel : ObservableObject
{
    [ObservableProperty] bool isRun;
    [ObservableProperty] string text;
    public MainViewModel()
    {
        
    }

    [RelayCommand]
    private void MachineRun(bool isRun)
    {
        this.IsRun = isRun;
    }
}
