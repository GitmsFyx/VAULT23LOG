using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using LOG.Models;
using LOG.Services;
using Microsoft.EntityFrameworkCore;

namespace LOG.ViewModels;

public class ShowPhotoViewModel : ViewModelBase
{
    public string Greeting { get; } = "欢迎查看照片!";
    
    private ObservableCollection<Photo> _photos = new ();
    
    public ObservableCollection<Photo> Photos
    {
        get => _photos;
        set => SetProperty(ref _photos, value);
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
        Photos.Clear();

        var photos = _logDbContext.Peoples.Include(p => p.Photos).First().Photos;

        foreach (var photo in photos)
        {
            Photos.Add( 
                new Photo()
                {
                    Content = $"{photo.CreateTime} \n {photo.Content.Substring(0, System.Math.Min(photo.Content.Length, 20))}...",
                    Image = photo.Image
                }
            );
        }
    }
}