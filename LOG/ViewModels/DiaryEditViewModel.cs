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
    public string Greeting { get; } = "欢迎编辑日志!";
    
    private ObservableCollection<Button> _logsButton = new ();
    
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
    
    public Log SelectedLog { get; set; }
    public DiaryEditViewModel(LogDbContext logDbContext)
    {
        _logDbContext = logDbContext;
    }

    public void Loaded()
    {
        ShowDiaries();
    }
    
    public void ShowDiaries()
    {
        LogsButton.Clear();
        var logs = _logDbContext.Peoples.Include(p=>p.Logs).First().Logs;
        foreach (var log in logs)
        {
            LogsButton.Add(new Button()
            {
                Content = $"{log.CreateTime} - {log.Content.Substring(0, Math.Min(log.Content.Length, 10))}...",
                Command = new RelayCommand(() =>
                {
                    SelectedLog = log;
                    var vm=ServiceLocator.Instance.DiaryChangeViewModel;
                    vm.CurrentLog = SelectedLog;
                    ServiceLocator.Instance.MainWindowViewModel.ViewModel = vm;

                })
            });
        }
        
    }
}