using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using LOG.Models;
using LOG.Services;
using Microsoft.EntityFrameworkCore;

namespace LOG.ViewModels;

public class DiaryEditViewModel : ViewModelBase
{
    public string Greeting { get; } = "欢迎查看日志!";
    
    private ObservableCollection<Log> _logs = new ();
    
    public ObservableCollection<Log> Logs
    {
        get => _logs;
        set => SetProperty(ref _logs, value);
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
    
    public DiaryEditViewModel(LogDbContext logDbContext)
    {
        _logDbContext = logDbContext;
        ShowDiaries();
    }
    
    public void ShowDiaries()
    {
        Logs.Clear();
        var logs = _logDbContext.Peoples.Include(p=>p.Logs).First().Logs;
        foreach (var log in logs)
        {
            Logs.Add(log);
        }
        
    }
}