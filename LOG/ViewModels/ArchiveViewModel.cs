using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using LOG.Models;
using LOG.Services;
using LOG.Views;
using Microsoft.EntityFrameworkCore;

namespace LOG.ViewModels;

public class ArchiveViewModel: ViewModelBase
{
    public string Greeting { get; } = "已进入档案系统!";

    //public bool ArchivesIsEmpty { get; set; } = true;
    
    public ObservableCollection<Archive> Archives { get; set; }= new ObservableCollection<Archive>();
    
    public RelayCommand LoadArchivesCommand { get; }
    
    public RelayCommand AddArchiveCommand { get; }
    
    public RelayCommand EditArchiveCommand{ get; }
    
    public RelayCommand DeleteArchiveCommand { get; }

    private People _selectedPeople;

    public People SelectedPeople
    {
        get=>_selectedPeople; 
        set=>SetProperty(ref _selectedPeople,value);
    }
    
    public Button[] Options { get; } =
    {
        new Button()
        {
            Content = "[返回]",
            Command = new RelayCommand(() =>
            {
                ServiceLocator.Instance.MainWindowViewModel.ViewModel = ServiceLocator.Instance.MainViewModel;
            })
        },
    };

    private LogDbContext _context;
    
    public ArchiveViewModel()
    {
        _context = ServiceLocator.Instance.LogDbContext;
        LoadArchivesCommand = new RelayCommand(LoadArchives);
        AddArchiveCommand = new RelayCommand(AddArchive);
        EditArchiveCommand= new RelayCommand(EditArchive);
        DeleteArchiveCommand = new RelayCommand(DeleteArchive);
    }

    private void DeleteArchive()
    {
        Console.WriteLine(_selectedPeople.Name);
    }

    private void EditArchive()
    {
        Console.WriteLine("111");
    }


    private async void AddArchive()
    {
        var name =int.Parse(_context.Archives.OrderBy(a=>a.Id).Last().Name);
        
        var addArchiveDialog = new AddArchiveDialog();
        var archiveContent = await addArchiveDialog.ShowDialog<string>(ServiceLocator.Instance.MainWindowView);
        
        var newArchive = new Archive
        {
            Name = "" + (name + 1),
            Content = archiveContent
        };

        _context.Archives.Add(newArchive);
        _context.SaveChanges();
        LoadArchives();
    }

    public void LoadArchives()
    {
        var archives = _context.Archives.Include(a => a.Peoples).Where(a=>a.Id!=1).AsNoTracking();

        Archives.Clear();
        foreach (var archive in archives)
        {
            Archives.Add(archive);
        }
    }
}