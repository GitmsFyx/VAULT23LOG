using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.Input;
using LOG.Models;
using LOG.Services;
using Microsoft.EntityFrameworkCore;

namespace LOG.ViewModels;

public class ShowPhotoViewModel : ViewModelBase
{
    public string Greeting { get; } = "欢迎查看照片!";
    
    private ObservableCollection<Button> _photosButton = new ();
    
    public ObservableCollection<Button> PhotosButton
    {
        get => _photosButton;
        set => SetProperty(ref _photosButton, value);
    }
        
    public Button[] Options { get; } =
    {
        new Button()
        { 
            Content = "[返回]",
            Command = new RelayCommand(()=>
            {
                ServiceLocator.Instance.MainWindowViewModel.ViewModel = ServiceLocator.Instance.PhotoViewModel;
            })
        },
    };

    private LogDbContext _logDbContext;
    
    public ShowPhotoViewModel(LogDbContext logDbContext)
    {
        _logDbContext = logDbContext;
        ShowPhotos();
    }
    
    public void ShowPhotos()
    {
        PhotosButton.Clear();

        var photos = _logDbContext.Peoples.Include(p => p.Photos).First().Photos;

        foreach (var photo in photos)
        {
            PhotosButton.Add(
                new Button()
                {
                    Content = new StackPanel()
                    {
                        Orientation = Orientation.Horizontal,
                        Children =
                        {
                            new Image()
                            {
                                Width = 250,
                                Source = new Bitmap(new MemoryStream(photo.Image))
                            },
                            new TextBlock()
                            {
                                Text = $"{photo.CreateTime} \n {photo.Content}",
                                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center
                            }
                        },
                    },
                    Command = new RelayCommand(() =>
                    {
                        var vm = ServiceLocator.Instance.PhotoDetialViewModel;
                        vm.CurrentPhoto = photo;
                        ServiceLocator.Instance.MainWindowViewModel.ViewModel = vm;
                    })
                }
            );
        }
    }
}