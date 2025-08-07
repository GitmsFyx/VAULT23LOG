using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;

namespace LOG.ViewModels;

public class DiaryViewModel: ViewModelBase
{
    public string Greeting { get; } = "已进入日志!";
    
    private Button _selectedOption;
    public Button SelectedOption
    {
        get=>_selectedOption;
        set=>SetProperty(ref _selectedOption, value);
    }
    public Button[] Options { get; } =
    {
        new Button()
        {
            Content = "[添加日志]",
            Command = new RelayCommand(() =>
            {
                ServiceLocator.Instance.MainWindowViewModel.ViewModel = ServiceLocator.Instance.DiaryEditViewModel;
            })
        },
        new Button()
        {
            Content = "[查看日志]",
            Command = new RelayCommand(() =>
            {
                
            })
        },
        new Button(){Content = "[2.]"},
        new Button(){Content = "[3.]"},
        new Button()
        {
        Content = "[返回]",
        Command = new RelayCommand(() =>
        {
            ServiceLocator.Instance.MainWindowViewModel.ViewModel = ServiceLocator.Instance.MainViewModel;
        })
        },
    };
}