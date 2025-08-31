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

public class PhotoChangeViewModel : ViewModelBase
{
    public string Greeting { get; } = "日志修改中...";
    
    public Photo CurrentPhoto { get; set; }
    
    private string _currentContent;
    public string CurrentContent
    {
        get => _currentContent;
        set=>SetProperty(ref _currentContent, value);
    }
    
    public ObservableCollection<Button> Options { get; } = new();

    private LogDbContext _dbContext;
    
    private IReadOnlyList<IStorageFile> _files;
    
    private Bitmap _imageBitmap;
    public Bitmap ImageBitmap
    {
        get=> _imageBitmap; 
        set=>SetProperty(ref _imageBitmap, value);
    }
    
    public byte[] ImageBytes { get; set; }
    public AsyncRelayCommand UploadPhotoCommand { get; }

    public PhotoChangeViewModel(LogDbContext dbContext)
    {
        _dbContext = dbContext;
        
        UploadPhotoCommand=new AsyncRelayCommand(UploadPhoto);
        
        Options.Add(new Button
        {
            Content = "[保存]",
            Command = new AsyncRelayCommand(async () =>
            {
                if (string.IsNullOrEmpty(CurrentContent))
                {
                    return;
                }
                try
                {
                    var photo= await _dbContext.Photos.FindAsync(CurrentPhoto.Id);
                    photo.Content = CurrentContent;
                    photo.Image = ImageBytes;
                    await _dbContext.SaveChangesAsync();
                    ServiceLocator.Instance.MainWindowViewModel.ViewModel = ServiceLocator.Instance.PhotoViewModel;
                }
                catch
                {
                    // ignored
                }
            })
        });
        Options.Add(new Button()
        {
            Content = "[删除]",
            Command = new AsyncRelayCommand(async () =>
            {
                try
                {
                    var photo = await _dbContext.Photos.FindAsync(CurrentPhoto.Id);
                    _dbContext.Photos.Remove(photo);
                    await _dbContext.SaveChangesAsync();
                    ServiceLocator.Instance.MainWindowViewModel.ViewModel = ServiceLocator.Instance.PhotoViewModel;
                }
                catch
                {
                    // ignored
                }
            })
        });
        Options.Add(new Button()
        {
            Content = "[返回]",
            Command = new RelayCommand(()=>
            {
                ServiceLocator.Instance.MainWindowViewModel.ViewModel= ServiceLocator.Instance.PhotoViewModel;
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
            ImageBytes =  File.ReadAllBytes(_files[0].Path.LocalPath);
            
            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            ImageBytes = memoryStream.ToArray();
            
            using var byteStream = new MemoryStream(ImageBytes);
            ImageBitmap = new Bitmap(byteStream);

        }
    }
}