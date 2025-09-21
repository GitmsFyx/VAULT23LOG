using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using LOG.Models;

namespace LOG.Views;

public partial class ArchiveAddPeopleDialog : Window
{
    public ArchiveAddPeopleDialog()
    {
        InitializeComponent();
    }


    private void Close_Click(object? sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(Name.Text))
        {
            Name.Text = "不能为空";
            return;
        }
        
        Close(Name.Text);
    }
}