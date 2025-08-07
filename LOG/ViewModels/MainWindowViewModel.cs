using System;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Avalonia.Controls;
using LOG.Services;
using Microsoft.EntityFrameworkCore;

namespace LOG.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private string _time;
    public string Time
    {
        get => _time;
        set => SetProperty(ref _time, value);
    }

    private ViewModelBase _viewModel;
    
    public ViewModelBase ViewModel
    {
        get => _viewModel;
        set => SetProperty(ref _viewModel, value);
    }
    
    private LogDbContext _dbContext;
    
    public MainWindowViewModel(LogDbContext dbContext)
    {
        _dbContext = dbContext;
        
        Initialize();
        UpdateTimeAsync();
    }

    public async Task Initialize()
    {
        try
        {
            if (!await _dbContext.Peoples.AnyAsync())
            {
                ViewModel = ServiceLocator.Instance.HelloLogViewModel;
            }
            else
            {
                ViewModel = ServiceLocator.Instance.MainViewModel;

            }
        }
        catch (Exception e)
        {
            Console.WriteLine("启动错误[MainWindow]",e);
            throw;
        }
    }
    
    public async Task UpdateTimeAsync()
    {
        while (true)
        {
            Time = System.DateTime.UtcNow.AddHours(8).ToString("yyyy-MM-dd HH:mm:ss");
            await Task.Delay(1000);
        }
    }
}