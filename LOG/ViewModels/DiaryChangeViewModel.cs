using System.Collections.ObjectModel;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
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
                    var log= await _dbContext.Logs.FindAsync(CurrentLog.Id);
                    log.Content = CurrentContent;
                    await _dbContext.SaveChangesAsync();
                    ServiceLocator.Instance.MainWindowViewModel.ViewModel = ServiceLocator.Instance.DialogViewModel;
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
                    var log = await _dbContext.Logs.FindAsync(CurrentLog.Id);
                    _dbContext.Logs.Remove(log);
                    await _dbContext.SaveChangesAsync();
                    ServiceLocator.Instance.MainWindowViewModel.ViewModel = ServiceLocator.Instance.DialogViewModel;
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
                ServiceLocator.Instance.MainWindowViewModel.ViewModel= ServiceLocator.Instance.DialogViewModel;
            })
        });
    }
}