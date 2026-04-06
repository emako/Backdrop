using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;

namespace Backdrop.WPF.Demo.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    public MainWindowViewModel()
    {
        BackdropOptions = new List<BackdropType>((BackdropType[])Enum.GetValues(typeof(BackdropType)));
        CornerOptions = new List<WindowCornerStyle>((WindowCornerStyle[])Enum.GetValues(typeof(WindowCornerStyle)));
    }

    [ObservableProperty]
    private BackdropType selectedBackdrop;

    [ObservableProperty]
    private WindowCornerStyle selectedCorner;

    [ObservableProperty]
    private bool isDarkMode;

    [ObservableProperty]
    private string osVersion = string.Empty;

    [ObservableProperty]
    private string supported = string.Empty;

    [ObservableProperty]
    private string selected = string.Empty;

    [ObservableProperty]
    private string statusText = string.Empty;

    public List<BackdropType> BackdropOptions { get; }

    public List<WindowCornerStyle> CornerOptions { get; }
}
