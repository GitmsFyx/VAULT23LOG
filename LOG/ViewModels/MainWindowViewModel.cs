using System.Dynamic;
using System.Runtime.CompilerServices;
using Avalonia.Controls;

namespace LOG.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    
    private ViewModelBase _viewModel;
    
    public ViewModelBase ViewModel
    {
        get => _viewModel;
        set => SetProperty(ref _viewModel, value);
    }
    
    public MainWindowViewModel()
    {
        Initialize();
    }

    public void Initialize()
    {
        ViewModel = ServiceLocator.Instance.MainViewModel;
    }
}