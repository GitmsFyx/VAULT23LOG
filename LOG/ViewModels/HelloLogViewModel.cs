
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
    
    public Button[] Options { get; }

    private async Task OnConfirmAsync()
    {
        if (!string.IsNullOrWhiteSpace(Name)) return;

        var people=new People()
        {
            Name = Name
        };
        await _dbContext.Peoples.AddAsync(people);
        await _dbContext.SaveChangesAsync();
        
        ServiceLocator.Instance.MainWindowViewModel.ViewModel = ServiceLocator.Instance.MainViewModel;

    }
    
    private LogDbContext _dbContext;
    
    public HelloLogViewModel(LogDbContext dbContext)
    {
        _dbContext=dbContext;
        Options=new[]
        {
            new Button()
            { 
                Content = "[确定]",
                Command = new AsyncRelayCommand(OnConfirmAsync),
            }
        };
    }
}
