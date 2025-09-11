using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using LOG.Models;

namespace LOG.Views;

public partial class AddArchiveDialog : Window
{
    public AddArchiveDialog()
    {
        InitializeComponent();
    }


    private void Close_Click(object? sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(Content.Text))
        {
            Content.Text = "不能为空";
            return;
        }
        
        Close(Content.Text);
    }
}