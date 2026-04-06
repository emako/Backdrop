using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace System.Backdrop.WPF.Demo;

[method: SuppressMessage("Usage", "CA2263:Prefer generic overload when type is known")]
public partial class MainWindowViewModel() : ObservableObject
{
    [ObservableProperty]
    public partial BackdropType SelectedBackdrop { get; set; }

    [ObservableProperty]
    public partial WindowCornerStyle SelectedCorner { get; set; }

    [ObservableProperty]
    public partial DarkModeOption SelectedTheme { get; set; }

    public List<DarkModeOption> DarkModeOptions { get; } = [.. (DarkModeOption[])Enum.GetValues(typeof(DarkModeOption))];

    [ObservableProperty]
    public partial string OsVersion { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string Supported { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string Selected { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string StatusText { get; set; } = string.Empty;

    public List<BackdropType> BackdropOptions { get; } = [.. (BackdropType[])Enum.GetValues(typeof(BackdropType))];

    public List<WindowCornerStyle> CornerOptions { get; } = [.. (WindowCornerStyle[])Enum.GetValues(typeof(WindowCornerStyle))];
}
