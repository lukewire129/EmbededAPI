using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace EmbededAPI.ViewModels;

public partial  class MainViewModel : ObservableObject
{
    [ObservableProperty] string text;
    public MainViewModel()
    {
        
    }

    [RelayCommand]
    private void UpdateText(string text)
    {
        this.Text = text;
    }
}
