using System;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using LOG.Models;
using LOG.Services;
using Microsoft.EntityFrameworkCore;
using Tmds.DBus.Protocol;

namespace LOG.ViewModels;

public class DiaryDetialViewModel :ViewModelBase
{
    public string Greeting { get; } = "日志信息...";
    
    public Log CurrentLog { get; set; }
    
    public ObservableCollection<Button> Options { get; } = new ObservableCollection<Button>();

    private LogDbContext _dbContext;
    
    public DiaryDetialViewModel(LogDbContext dbContext)
    {
        _dbContext = dbContext;
        Options.Add(new Button()
        {
            Content = "[返回]",
            Command = new RelayCommand(()=>
            {
                ServiceLocator.Instance.MainWindowViewModel.ViewModel = ServiceLocator.Instance.ShowDiaryViewModel;
            })
        });
    }
}