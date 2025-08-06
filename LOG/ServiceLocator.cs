using System;
using Avalonia;
using LOG.Services;
using LOG.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace LOG;

public class ServiceLocator
{
    private readonly IServiceProvider _serviceProvider;
    
    public static readonly ServiceLocator Instance = Application.Current.Resources["ServiceLocator"] as ServiceLocator;
    
    public MainViewModel MainViewModel  => _serviceProvider.GetService<MainViewModel>();
    public MainWindowViewModel MainWindowViewModel=> _serviceProvider.GetService<MainWindowViewModel>();
    public DiaryViewModel DialogViewModel=> _serviceProvider.GetService<DiaryViewModel>();
    public DiaryEditViewModel DiaryEditViewModel => _serviceProvider.GetService<DiaryEditViewModel>();
    public HelloLogViewModel HelloLogViewModel => _serviceProvider.GetService<HelloLogViewModel>();
    
    public ServiceLocator()
    {
        var services = new ServiceCollection();
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<MainWindowViewModel>();
        services.AddSingleton<DiaryViewModel>();
        services.AddSingleton<DiaryEditViewModel>();
        services.AddSingleton<HelloLogViewModel>();
        services.AddDbContext<LogDbContext>();

        _serviceProvider = services.BuildServiceProvider();
        
    } 
    
}