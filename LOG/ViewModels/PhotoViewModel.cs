using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;

namespace LOG.ViewModels;

public class PhotoViewModel : ViewModelBase
{
    public string Greeting { get; } = "已进入相册系统!";
    
    public Button[] Options { get; } =
    {
        new Button()
        {
            Content = "[查看照片]",
            Command = new RelayCommand(() =>
            {
                ServiceLocator.Instance.MainWindowViewModel.ViewModel = ServiceLocator.Instance.ShowPhotoViewModel;
            })
        },
        new Button()
        {
            Content = "[添加照片]",
            Command = new RelayCommand(() =>
            {
                ServiceLocator.Instance.MainWindowViewModel.ViewModel = ServiceLocator.Instance.PhotoAddViewModel;
            })
        },
        new Button()
        {
            Content = "[修改图片]",
            Command = new RelayCommand((() =>
            {
                ServiceLocator.Instance.PhotoEditViewModel.Loaded();
                ServiceLocator.Instance.MainWindowViewModel.ViewModel = ServiceLocator.Instance.PhotoEditViewModel;
            }))
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