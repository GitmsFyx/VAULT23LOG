using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using LOG.Services;
using Microsoft.EntityFrameworkCore;

namespace LOG.ViewModels;

public class DiaryEditViewModel :ViewModelBase
{
    public string Greeting { get; } = "日志编辑中...";

    private string _currentContent;
    public string CurrentContent
    {
        get => _currentContent;
        set=>SetProperty(ref _currentContent, value);}
    
    private Button _selectedOption;
    public Button SelectedOption
    {
        get=>_selectedOption;
        set=>SetProperty(ref _selectedOption, value);
    }
    public Button[] Options { get; } =
    {
        new Button()
        {
            Content = "[保存]",
            Command = new RelayCommand(() =>
            {
                
                
            })
        },
        new Button()
        {
            Content = "[返回]",
            Command = new RelayCommand(() =>
            {
                ServiceLocator.Instance.MainWindowViewModel.ViewModel = ServiceLocator.Instance.DialogViewModel;
            })
        }
    };

    private LogDbContext _dbContext;
    
    public DiaryEditViewModel(LogDbContext dbContext)
    {
        _dbContext = dbContext;
    }
}