using System;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using LOG.Models;
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

    public ObservableCollection<Button> Options { get; } = new ObservableCollection<Button>();

    private LogDbContext _dbContext;
    
    public DiaryEditViewModel(LogDbContext dbContext)
    {
        _dbContext = dbContext;
        Options.Add(new Button()
        {
            Content = "[保存]",
            Command = new AsyncRelayCommand(async () =>
            {
                try
                {
                    var log = new Log()
                    {
                        PeopleId = 0,
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