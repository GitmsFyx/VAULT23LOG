using System;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using LOG.Models;
using LOG.Services;
using Microsoft.EntityFrameworkCore;
using Tmds.DBus.Protocol;

namespace LOG.ViewModels;

public class DiaryAddViewModel :ViewModelBase
{
    public string Greeting { get; } = "日志添加中...";

    private string _currentContent;
    public string CurrentContent
    {
        get => _currentContent;
        set=>SetProperty(ref _currentContent, value);
    }
    
    private string _TbWatermark = "请输入日志内容...";
    
    public string TbWatermark
    {
        get => _TbWatermark;
        set => SetProperty(ref _TbWatermark, value);
    }

    public ObservableCollection<Button> Options { get; } = new ObservableCollection<Button>();

    private LogDbContext _dbContext;
    
    public DiaryAddViewModel(LogDbContext dbContext)
    {
        _dbContext = dbContext;
        Options.Add(new Button()
        {
            Content = "[保存]",
            Command = new AsyncRelayCommand(async () =>
            {
                if (String.IsNullOrEmpty(CurrentContent))
                {
                    TbWatermark = "内容不能为空";
                    return;
                }
                try
                {
                    var log = new Log()
                    {
                        PeopleId = 1,
                        People = await _dbContext.Peoples.FirstAsync(),
                        Content = CurrentContent,

                    };
                    await _dbContext.Logs.AddAsync(log);
                    await _dbContext.SaveChangesAsync();
                    CurrentContent = string.Empty;
                    ServiceLocator.Instance.MainWindowViewModel.ViewModel= ServiceLocator.Instance.DialogViewModel;
                }
                catch (Exception e)
                {
                    Console.WriteLine("添加Log失败",e);
                    throw;
                }
            })
        });
        Options.Add(new Button()
        {
            Content = "[返回]",
            Command = new RelayCommand(() =>
            {
                ServiceLocator.Instance.MainWindowViewModel.ViewModel = ServiceLocator.Instance.DialogViewModel;
            })
        });
    }
}