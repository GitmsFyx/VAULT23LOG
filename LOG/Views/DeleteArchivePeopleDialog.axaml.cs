using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using LOG.Models;

namespace LOG.Views;

public partial class DeleteArchivePeopleDialog: Window
{
    public DeleteArchivePeopleDialog()
    {
        InitializeComponent();
    }

    private void NO_Click(object? sender, RoutedEventArgs e)
    {
        Close(false);
    }

    private void Yes_Click(object? sender, RoutedEventArgs e)
    {
        Close(true);
    }
}