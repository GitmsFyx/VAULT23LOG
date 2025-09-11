using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using LOG.Models;
using LOG.Services;
using LOG.ViewModels;


namespace LOG.Views;

public partial class ArchiveView : UserControl
{
    private LogDbContext _context;

    public ArchiveView()
    {
        InitializeComponent();
        DataContext = ServiceLocator.Instance.ArchiveViewModel;
        Init();
    }

    public void Init()
    {
        _context = ServiceLocator.Instance.LogDbContext;
    }
    
    

    private async void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        if (sender is Button button&& button.Tag is int tag)
        {
            var editArchiveDialog = new ArchiveEditDialog();
            var name= await editArchiveDialog.ShowDialog<string>(ServiceLocator.Instance.MainWindowView);
            if (string.IsNullOrEmpty(name))
            {
                return;
            }
            var people = new People()
            {
                Name = name,
                ArchiveId = tag,
            };
            _context.Peoples.Add(people);
            _context.SaveChanges();
            ServiceLocator.Instance.ArchiveViewModel.LoadArchives();
            
        }
        
    }
}