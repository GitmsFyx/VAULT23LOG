using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;


namespace LOG.Models;

[ObservableObject]
public partial class LogItem
{
    [ObservableProperty]
    private int _id;
    
    [ObservableProperty]
    private int _peopleId;
    
    [ObservableProperty]
    private People _people;

    [ObservableProperty] private DateTime _createTime;
    
    [ObservableProperty]
    private string _content = string.Empty;
    
    [ObservableProperty]
    private bool _visible = true;
    
}