using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;

namespace LOG.ViewModels;

public class ShowLogViewModel: ViewModelBase
{
    public string Greeting { get; } = "欢迎查看日志!";
    
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
            Content = "[日志]",
            Command = new RelayCommand(()=>
            {
                ServiceLocator.Instance.MainWindowViewModel.ViewModel = ServiceLocator.Instance.DialogViewModel;
            })
        },

        new Button() {Content = "[相册]"},
        new Button() {Content = "[...]"},
        
    };
}