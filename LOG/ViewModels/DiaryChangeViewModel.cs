using System.Collections.ObjectModel;
using Avalonia.Controls;
using LOG.Models;
using LOG.Services;

namespace LOG.ViewModels;

public class DiaryChangeViewModel : ViewModelBase
{
    public string Greeting { get; } = "日志修改中...";
    
    public Log CurrentLog { get; set; }
    
    private string _currentContent;
    public string CurrentContent
    {
        get => _currentContent;
        set=>SetProperty(ref _currentContent, value);
    }
    
    public ObservableCollection<Button> Options { get; } = new();

    private LogDbContext _dbContext;

    public DiaryChangeViewModel(LogDbContext dbContext)
    {
        _dbContext = dbContext;
    }
}