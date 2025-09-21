using System;
using Avalonia;
using LOG.Services;
using LOG.ViewModels;
using LOG.Views;
using Microsoft.Extensions.DependencyInjection;

namespace LOG;

public class ServiceLocator
{
    private readonly IServiceProvider _serviceProvider;
    
    public static readonly ServiceLocator Instance = Application.Current.Resources["ServiceLocator"] as ServiceLocator;
    
    public MainViewModel MainViewModel  => _serviceProvider.GetService<MainViewModel>();
    public MainWindowViewModel MainWindowViewModel=> _serviceProvider.GetService<MainWindowViewModel>();
    public DiaryViewModel DialogViewModel=> _serviceProvider.GetService<DiaryViewModel>();
    public DiaryAddViewModel DiaryAddViewModel => _serviceProvider.GetService<DiaryAddViewModel>();
    public HelloLogViewModel HelloLogViewModel => _serviceProvider.GetService<HelloLogViewModel>();
    
    public ShowDiaryViewModel ShowDiaryViewModel => _serviceProvider.GetService<ShowDiaryViewModel>();
    public PhotoViewModel PhotoViewModel => _serviceProvider.GetService<PhotoViewModel>();
    
    public ShowPhotoViewModel ShowPhotoViewModel => _serviceProvider.GetService<ShowPhotoViewModel>();
    
    public PhotoAddViewModel PhotoAddViewModel => _serviceProvider.GetService<PhotoAddViewModel>();
    
    public MainWindowView MainWindowView => _serviceProvider.GetService<MainWindowView>();
    
    public DiaryEditViewModel DiaryEditViewModel => _serviceProvider.GetService<DiaryEditViewModel>();
    
    public DiaryChangeViewModel DiaryChangeViewModel => _serviceProvider.GetService<DiaryChangeViewModel>();
    
    public PhotoChangeViewModel PhotoChangeViewModel => _serviceProvider.GetService<PhotoChangeViewModel>();
    
    public PhotoEditViewModel PhotoEditViewModel => _serviceProvider.GetService<PhotoEditViewModel>();
    
    public DiaryDetialViewModel DiaryDetialViewModel => _serviceProvider.GetService<DiaryDetialViewModel>();
    
    public PhotoDetialViewModel PhotoDetialViewModel => _serviceProvider.GetService<PhotoDetialViewModel>();
    
    public ArchiveViewModel ArchiveViewModel => _serviceProvider.GetService<ArchiveViewModel>();
    
    public ArchiveView ArchiveView => _serviceProvider.GetService<ArchiveView>();
    
    public LogDbContext LogDbContext => _serviceProvider.GetService<LogDbContext>();
    
    public ShowArchivePeopleViewModel ShowArchivePeopleViewModel => _serviceProvider.GetService<ShowArchivePeopleViewModel>();
    
    public ServiceLocator()
    {
        var services = new ServiceCollection();
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<MainWindowViewModel>();
        services.AddSingleton<DiaryViewModel>();
        services.AddSingleton<DiaryAddViewModel>();
        services.AddSingleton<HelloLogViewModel>();
        services.AddSingleton<ShowDiaryViewModel>();
        services.AddSingleton<PhotoViewModel>();
        services.AddSingleton<ShowPhotoViewModel>();
        services.AddSingleton<PhotoAddViewModel>();
        services.AddSingleton<MainWindowView>();
        services.AddSingleton<DiaryEditViewModel>();

        services.AddSingleton<PhotoEditViewModel>();
        services.AddSingleton<PhotoChangeViewModel>();
        services.AddTransient<DiaryChangeViewModel>();

        services.AddTransient<DiaryDetialViewModel>();
        services.AddTransient<PhotoDetialViewModel>();
        services.AddSingleton<ArchiveViewModel>();
        services.AddSingleton<ArchiveView>();
        services.AddTransient<ShowArchivePeopleViewModel>();
        
        services.AddDbContext<LogDbContext>();
        

        _serviceProvider = services.BuildServiceProvider();
        
    } 
}