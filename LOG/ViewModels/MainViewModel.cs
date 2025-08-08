using System;
using Avalonia;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;

namespace LOG.ViewModels;

public class MainViewModel : ViewModelBase
{
    public string Greeting { get; } = "欢迎访问 23号 终端!";
    
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
        new Button()
        {
            Content = "[退出]",
            Command = new RelayCommand(() =>
            {
                Environment.Exit(0);
            })
        },
        
    };
    
    public MainViewModel()
    {
        
    }
}