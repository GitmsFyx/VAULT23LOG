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

public class PhotoAddViewModel :ViewModelBase
{
    public string Greeting { get; } = "已进入添加照片界面!";

    private string _content;

    public string Content
    {
        get => _content;
        set => SetProperty(ref _content, value);
    }
    
    public AsyncRelayCommand UploadPhotoCommand { get; }

    public ObservableCollection<Button> Options { get; } = new ObservableCollection<Button>();

    private Bitmap _imageBitmap;

    public Bitmap ImageBitmap
    {
        get => _imageBitmap;
        set => SetProperty(ref _imageBitmap, value);
    }

    private LogDbContext _logDbContext;

    private IReadOnlyList<IStorageFile> _files;
    
    public PhotoAddViewModel(LogDbContext logDbContext)
    {
        _logDbContext = logDbContext;
        UploadPhotoCommand= new AsyncRelayCommand(UploadPhoto);

        Options.Add(new Button()
        {
            Content = "[保存]",
            Command = new AsyncRelayCommand( async () =>
            {
                try
                {
                    var photo = new Photo()
                    {
                        PeopleId = 1,
                        People = await _logDbContext.Peoples.FindAsync(0),
                        Content = string.IsNullOrWhiteSpace(Content)? "无描述": Content,
                        Image = ImageBitmap != null ? File.ReadAllBytes(_files[0].Path.LocalPath) : null,
                        CreateTime = DateTime.Now
                    };
                    await _logDbContext.Photos.AddAsync(photo);
                    await _logDbContext.SaveChangesAsync();
                    ServiceLocator.Instance.MainWindowViewModel.ViewModel = ServiceLocator.Instance.PhotoViewModel;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            })
        });
        Options.Add(new Button()
        {
            Content = "[返回]",
            Command = new RelayCommand(() =>
            {
                ServiceLocator.Instance.MainWindowViewModel.ViewModel = ServiceLocator.Instance.PhotoViewModel;
            })
        });

    }

    private async Task UploadPhoto()
    {
        var topLevel = TopLevel.GetTopLevel(ServiceLocator.Instance.MainWindowView);

        if (topLevel is null)
        {
            return;
        }
        
        
        var filePickerOptions = new FilePickerOpenOptions()
        {
            Title = "请选择要上传的照片",
            AllowMultiple = false,
            FileTypeFilter = new[]
            {
                new FilePickerFileType("图片")
                {
                    Patterns = new[] { "*.png", "*.jpg", "*.jpeg", "*.bmp", "*.gif" }
                }
            }
        };

        _files = await topLevel.StorageProvider.OpenFilePickerAsync(filePickerOptions);
        
        if (_files.Count >= 1)
        {
            await using var stream=await _files[0].OpenReadAsync();
            ImageBitmap = new Bitmap(stream);
        }
        
        
    }
    
}