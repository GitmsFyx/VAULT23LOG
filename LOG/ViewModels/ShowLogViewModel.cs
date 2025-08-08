using System;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using LOG.Services;
using Microsoft.EntityFrameworkCore;

namespace LOG.ViewModels;

public class ShowLogViewModel: ViewModelBase
{
    public string Greeting { get; } = "欢迎查看日志!";
    
    private ObservableCollection<Button> _logsButton = new ObservableCollection<Button>();
    
    public ObservableCollection<Button> LogsButton
    {
        get => _logsButton;
        set => SetProperty(ref _logsButton, value);
    }
        
    public Button[] Options { get; } =
    {
        new Button()
        { 
            Content = "[返回]",
            Command = new RelayCommand(()=>
            {
                ServiceLocator.Instance.MainWindowViewModel.ViewModel = ServiceLocator.Instance.DialogViewModel;
            })
        },
    };

    private LogDbContext _logDbContext;
    
    public ShowLogViewModel(LogDbContext logDbContext)
    {
        _logDbContext = logDbContext;
    }
    
    public void AddLog()
    {
        LogsButton.Clear();
        var logs = _logDbContext.Peoples.Include(p=>p.Logs).First().Logs;
        foreach (var log in logs)
        {
            var button = new Button
            {
                Content = $"{log.CreateTime} - {log.Content.Substring(0, Math.Min(log.Content.Length, 10))}...",
                Command = new RelayCommand(() =>
                {

                })
            };
            LogsButton.Add(button);
        }
    }
}