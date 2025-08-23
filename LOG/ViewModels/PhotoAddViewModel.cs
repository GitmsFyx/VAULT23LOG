using System.IO;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.Input;

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
    
    public Button[] Options { get; } =
    {

        new Button()
        {
            Content = "[返回]",
            Command = new RelayCommand(() =>
            {
                ServiceLocator.Instance.MainWindowViewModel.ViewModel = ServiceLocator.Instance.PhotoViewModel;
                
            })
        },
    };
    
    public PhotoAddViewModel()
    {
        UploadPhotoCommand= new AsyncRelayCommand(UploadPhoto);
    }

    private async Task UploadPhoto()
    {
        var topLevel = TopLevel.GetTopLevel(ServiceLocator.Instance.MainWindowView);

        var files=await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions()
        {
            Title = "Open Text File",
            AllowMultiple = false
        });

        if (files.Count >= 1)
        {
            await using var stream=await files[0].OpenReadAsync();
            using var streamReader = new StreamReader(stream);
            var fileCOntent = await streamReader.ReadToEndAsync();
        }
    }
    
}