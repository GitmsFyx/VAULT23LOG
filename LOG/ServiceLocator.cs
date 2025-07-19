using System;
using Avalonia;
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
    
    public ServiceLocator()
    {
        var services = new ServiceCollection();
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<MainWindowViewModel>();
        services.AddSingleton<DiaryViewModel>();

        _serviceProvider = services.BuildServiceProvider();
        
    } 
    
}