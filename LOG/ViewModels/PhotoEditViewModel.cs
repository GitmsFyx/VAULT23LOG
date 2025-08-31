using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.Input;
using LOG.Helpers;
using LOG.Models;
using LOG.Services;
using Microsoft.EntityFrameworkCore;

namespace LOG.ViewModels;

public class PhotoEditViewModel : ViewModelBase
{
    public string Greeting { get; } = "欢迎编辑相册!";
    
    private ObservableCollection<Button> _photoButton = new ();
    
    public ObservableCollection<Button> PhotoButton
    {
        get => _photoButton;
        set => SetProperty(ref _photoButton, value);
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
    
    public Photo SelectedPhoto { get; set; }
    
    public PhotoEditViewModel(LogDbContext logDbContext)
    {
        _logDbContext = logDbContext;
    }

    public void Loaded()
    {
        ShowDiaries();
    }
    
    public void ShowDiaries()
    {
        _photoButton.Clear();
        var photos = _logDbContext.Peoples.Include(p=>p.Photos).First().Photos;
        foreach (var photo in photos)
        {
            _photoButton.Add(new Button()
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
                            Text = $"{photo.CreateTime} \n {photo.Content.Substring(0, System.Math.Min(photo.Content.Length, 20))}...",
                        }
                    }
                },
                Command = new RelayCommand(() =>
                {
                    SelectedPhoto = photo;
                    var vm=ServiceLocator.Instance.PhotoChangeViewModel;
                    vm.CurrentPhoto = SelectedPhoto;
                    vm.CurrentContent= SelectedPhoto.Content;
                    vm.ImageBytes = SelectedPhoto.Image;
                    vm.ImageBitmap = new Bitmap(new MemoryStream(SelectedPhoto.Image));
                    ServiceLocator.Instance.MainWindowViewModel.ViewModel = vm;

                })
            });
        }
        
    }
}