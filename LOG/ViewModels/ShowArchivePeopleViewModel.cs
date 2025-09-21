using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using LOG.Models;
using LOG.Services;
using Microsoft.EntityFrameworkCore;

namespace LOG.ViewModels;

public class ShowArchivePeopleViewModel : ViewModelBase
{
    public string Greeting { get; } = "欢迎查看档案!";

    private ObservableCollection<Button> _logsButton = new ObservableCollection<Button>();

    private int _peopleID;
    
    public int PeopleID
    {
        get => _peopleID;
        set
        {
            SetProperty(ref _peopleID, value); 
            PeopleIDHasChanged();
        }
    }

    private void PeopleIDHasChanged()
    {
        People = _logDbContext.Peoples.Include(p=>p.Logs).Where(p=>p.Id == PeopleID).FirstOrDefault();
    }

    private string _content = "";
    
    public string Content
    {
        get => _content;
        set => SetProperty(ref _content, value);
    }

    public ObservableCollection<Button> Options { get; } = new ();

    private People _people;
    public People People
    {
        get => _people;
        set => SetProperty(ref _people, value);
    }

    private LogDbContext _logDbContext;

    public RelayCommand SaveNewLogCommand { get; }
    
    public RelayCommand<Object[]> UpdateLogCommand { get; }
    
    public ShowArchivePeopleViewModel()
    {
        _logDbContext= ServiceLocator.Instance.LogDbContext;
        SaveNewLogCommand = new RelayCommand(SaveNewLog);
        UpdateLogCommand = new RelayCommand<Object[]>(UpdateLog);
        Init();
    }

    private ObservableCollection<LogItem> _Logs = new();

    public ObservableCollection<LogItem> Logs
    {
        get => _Logs;
        set => SetProperty(ref _Logs, value);
    }
    
    private void UpdateLog(Object[] obj)
    {
        int id = (int)obj[0];
        string content = (string)obj[1];
        var log = _logDbContext.Logs.Find(id);
        if (log == null) return;
        if (content==String.Empty)
        {
            _logDbContext.Logs.Remove(log);
        }
        else
        {
            log.Content = content;
        }
        _logDbContext.SaveChanges();
        var instanceShowArchivePeopleViewModel = ServiceLocator.Instance.ShowArchivePeopleViewModel;
        instanceShowArchivePeopleViewModel.PeopleID = PeopleID;
        instanceShowArchivePeopleViewModel.Load();
        ServiceLocator.Instance.MainWindowViewModel.ViewModel = instanceShowArchivePeopleViewModel;
        
    }

    private void SaveNewLog()
    {
        var people = _logDbContext.Peoples.Find(PeopleID);
        if (people == null) return;
        if (Content == String.Empty) return;
        var log = new Log()
        {
            PeopleId = PeopleID,
            Content = Content,
            Visible = true,
        };
        _logDbContext.Logs.Add(log);
        _logDbContext.SaveChanges();
        var instanceShowArchivePeopleViewModel = ServiceLocator.Instance.ShowArchivePeopleViewModel;
        instanceShowArchivePeopleViewModel.PeopleID = PeopleID;
        instanceShowArchivePeopleViewModel.Load();
        ServiceLocator.Instance.MainWindowViewModel.ViewModel = instanceShowArchivePeopleViewModel; 
    }

    public void Load()
    {
        People=_logDbContext.Peoples.AsNoTracking().Include(p=>p.Logs).Where(p=>p.Id == PeopleID).FirstOrDefault();
        Logs = new ObservableCollection<LogItem>(People.Logs.Select(l => new LogItem
        {
            Id = l.Id,
            PeopleId = l.PeopleId,
            People = People,
            CreateTime = l.CreateTime,
            Content = l.Content,
            Visible = l.Visible
        }));
    }
    
    public int count = 0;
    public void Init()
    {

        Options.Add(
        new Button()
            {
                Content = "[返回]",
                Command = new RelayCommand(() =>
                {
                    ServiceLocator.Instance.MainWindowViewModel.ViewModel = ServiceLocator.Instance.ArchiveViewModel;
                })
            }
        );
    }
}