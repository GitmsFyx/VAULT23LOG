using System;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Xaml.Interactivity;
using LOG.ViewModels;

namespace LOG.Behaviors;

public class UserControlKeyDownBehavior : Behavior<UserControl>
{

    private Key _UPKey = Key.Up;
    private DateTime _lastKeyTime;
    
    protected override void OnAttached()
    {
        AssociatedObject.KeyDown+= OnKeyDown;
    }
    
    protected override void OnDetaching()
    {
        AssociatedObject.KeyDown -= OnKeyDown;
    }
    
    private void OnKeyDown(object? sender, KeyEventArgs e)
    {
        var currentTime = DateTime.Now;
        var elapsed= (currentTime - _lastKeyTime).TotalMilliseconds;

        if (e.Key == _UPKey && elapsed < 500)
        {
            HandleDoubleKeyPress(e.Key);
            e.Handled = true;
        }
        else if(e.Key==_UPKey)
        {
            _lastKeyTime = currentTime;
        }
    }

    private void HandleDoubleKeyPress(Key eKey)
    {
        ServiceLocator.Instance.MainWindowViewModel.ViewModel = ServiceLocator.Instance.ArchiveViewModel;
    }
}