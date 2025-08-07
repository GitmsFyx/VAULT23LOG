
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using LOG.Models;
using LOG.Services;
using Microsoft.EntityFrameworkCore;

namespace LOG.ViewModels;

public class HelloLogViewModel : ViewModelBase
{
    public string Greeting { get; } = "欢迎访问 23号 终端!";
    
    private string _name;
    public string Name { 
        get => _name; 
        set => SetProperty(ref _name, value); 
    } 
    
    private Button _selectedOption;
    public Button SelectedOption
    {
        get=>_selectedOption;
        set=>SetProperty(ref _selectedOption, value);
    }

    public ObservableCollection<Button> Options { get; } = new ObservableCollection<Button>();

    private async Task OnConfirmAsync()
    {
        if (string.IsNullOrWhiteSpace(Name)) return;
        
        try
        {
            var people=new People()
            {
                Name = Name
            };
            await _dbContext.Peoples.AddAsync(people);
            await _dbContext.SaveChangesAsync();
            
            ServiceLocator.Instance.MainWindowViewModel.ViewModel = ServiceLocator.Instance.MainViewModel;
        }
        catch (Exception e)
        {
            Console.WriteLine("数据库错误",e);
            throw;
        }

    }
    
    private LogDbContext _dbContext;
    
    public HelloLogViewModel(LogDbContext dbContext)
    {
        _dbContext=dbContext;
        Options.Add(new Button()
        { 
            Content = "[确定]",
            Command = new AsyncRelayCommand(OnConfirmAsync),
        });

    }
}
