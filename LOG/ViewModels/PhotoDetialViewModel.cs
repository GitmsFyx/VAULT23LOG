using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.Input;
using LOG.Models;
using LOG.Services;

namespace LOG.ViewModels;

public class PhotoDetialViewModel :ViewModelBase
{
    public string Greeting { get; } = "照片信息!";
    
    public string Content
    {
        get => CurrentPhoto.CreateTime.ToString("yyyy-MM-dd HH:mm:ss") + "\n" + CurrentPhoto.Content;
    }
    
    public ObservableCollection<Button> Options { get; } = new ObservableCollection<Button>();

    private Bitmap _imageBitmap;
    public Bitmap ImageBitmap
    {
        get => _imageBitmap;
        set => SetProperty(ref _imageBitmap, value);
    }
    
    public Photo CurrentPhoto { get; set; }

    private LogDbContext _logDbContext;
    
    public PhotoDetialViewModel(LogDbContext logDbContext)
    {
        _logDbContext = logDbContext;
        
        Options.Add(new Button()
        {
            Content = "[返回]",
            Command = new RelayCommand(() =>
            {
                ServiceLocator.Instance.MainWindowViewModel.ViewModel = ServiceLocator.Instance.ShowPhotoViewModel;
            })
        });

    }
}