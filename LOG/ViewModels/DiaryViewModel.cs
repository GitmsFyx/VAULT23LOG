using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;

namespace LOG.ViewModels;

public class DiaryViewModel: ViewModelBase
{
    public string Greeting { get; } = "已进入日志系统!";
    
    public Button[] Options { get; } =
    {
        new Button()
        {
            Content = "[添加日志]",
            Command = new RelayCommand(() =>
            {
                ServiceLocator.Instance.MainWindowViewModel.ViewModel = ServiceLocator.Instance.DiaryAddViewModel;
            })
        },
        new Button()
        {
            Content = "[查看日志]",
            Command = new RelayCommand(() =>
            {
                ServiceLocator.Instance.MainWindowViewModel.ViewModel= ServiceLocator.Instance.ShowDiaryViewModel;
            })
        },
        new Button()
        {
            Content = "[修改日志]",
            Command = new RelayCommand(() =>
            {
                ServiceLocator.Instance.MainWindowViewModel.ViewModel= ServiceLocator.Instance.ShowDiaryViewModel;
            })
        },
        new Button()
        {
            Content = "[删除日志]",
            Command = new RelayCommand(() =>
            {
                ServiceLocator.Instance.MainWindowViewModel.ViewModel= ServiceLocator.Instance.ShowDiaryViewModel;
            })
        },
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