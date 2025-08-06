using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Avalonia.Controls;

namespace LOG.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private string _time;
    public string Time
    {
        get => _time;
        set => SetProperty(ref _time, value);
    }
    
    private ViewModelBase _viewModel;
    
    public ViewModelBase ViewModel
    {
        get => _viewModel;
        set => SetProperty(ref _viewModel, value);
    }
    
    public MainWindowViewModel()
    {
        Initialize();
        UpdateTimeAsync();
    }

    public void Initialize()
    {

        ViewModel = ServiceLocator.Instance.HelloLogViewModel;
    }
    
    public async Task UpdateTimeAsync()
    {
        while (true)
        {
            Time = System.DateTime.UtcNow.AddHours(8).ToString("yyyy-MM-dd HH:mm:ss");
            await Task.Delay(1000);
        }
    }
}