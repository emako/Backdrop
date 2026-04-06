using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Media;
using System.ComponentModel;
using Backdrop.WPF.Demo.ViewModels;
using System.Linq;

namespace Backdrop.WPF.Demo;

internal partial class MainWindow : Window
{
    private MainWindowViewModel ViewModel { get; }

    public MainWindow()
    {
        InitializeComponent();
        DataContext = ViewModel = new MainWindowViewModel();

        // initialize defaults
        ViewModel.SelectedBackdrop = BackdropType.Acrylic11;
        ViewModel.SelectedCorner = WindowCornerStyle.Round;
        // default theme to Dark
        ViewModel.SelectedTheme = ViewModel.DarkModeOptions.FirstOrDefault(x => x == DarkModeOption.Dark);

        ViewModel.PropertyChanged += ViewModel_PropertyChanged;
    }

    protected override void OnSourceInitialized(EventArgs e)
    {
        base.OnSourceInitialized(e);
        ApplySelectedEffect();
    }

    private void ViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(MainWindowViewModel.SelectedBackdrop)
            or nameof(MainWindowViewModel.SelectedCorner)
            or nameof(MainWindowViewModel.SelectedTheme))
        {
            ApplySelectedEffect();
        }
    }

    protected override void OnContentRendered(EventArgs e)
    {
        base.OnContentRendered(e);
        ApplySelectedEffect();
    }

    private void ApplySelectedEffect()
    {
        var backdrop = GetSelectedBackdrop();
        var darkMode = ViewModel?.SelectedTheme == DarkModeOption.Dark;

        BackdropWindowHelper.Remove(this);

        if (darkMode)
        {
            this.ApplyDarkMode();
        }
        else
        {
            this.RemoveDarkMode();
        }

        CornerWindowHelper.SetWindowCorners(this, GetSelectedCorner());

        Background = Brushes.Transparent;
        BackdropWindowHelper.Apply(this, backdrop);
        UpdateStatus();
    }

    private void UpdateStatus()
    {
        var backdrop = GetSelectedBackdrop();
        var actualBackdrop = backdrop.GetActualBackdropType();
        var supported = backdrop.IsSupported();
        if (ViewModel is null)
            return;

        ViewModel.OsVersion = OSVersionHelper.OSVersion.ToString();
        ViewModel.Supported = supported ? "Yes" : "No";
        ViewModel.Selected = $"Backdrop: {backdrop}\nActual: {actualBackdrop}\nCorner: {GetSelectedCorner()}\nTheme: {ViewModel.SelectedTheme}";

        ViewModel.StatusText =
            $"OS Version      : {OSVersionHelper.OSVersion}\n" +
            $"Backdrop        : {backdrop}\n" +
            $"Actual Backdrop : {actualBackdrop}\n" +
            $"Supported       : {supported}\n" +
            $"Manual Bg Needed: {backdrop.IsManualBackgroundNeeded()}\n" +
            $"Corner          : {GetSelectedCorner()}\n" +
            $"Theme           : {ViewModel.SelectedTheme}";
    }

    private BackdropType GetSelectedBackdrop()
    {
        return ViewModel?.SelectedBackdrop ?? BackdropType.None;
    }

    private WindowCornerStyle GetSelectedCorner()
    {
        return ViewModel?.SelectedCorner ?? WindowCornerStyle.Default;
    }
}
