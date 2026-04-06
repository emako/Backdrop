using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Media;
using System.ComponentModel;
using Backdrop.WPF.Demo.ViewModels;

namespace Backdrop.WPF.Demo;

internal partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        // Use MVVM view model for ComboBox ItemsSource / SelectedItem and DarkMode
        ViewModel = new MainWindowViewModel();
        DataContext = ViewModel;

        // initialize defaults
        ViewModel.SelectedBackdrop = BackdropType.Acrylic11;
        ViewModel.SelectedCorner = WindowCornerStyle.Round;
        ViewModel.IsDarkMode = true;

        ViewModel.PropertyChanged += ViewModel_PropertyChanged;
    }

    protected override void OnSourceInitialized(EventArgs e)
    {
        base.OnSourceInitialized(e);
        ApplySelectedEffect();
    }

    [SuppressMessage("Usage", "CA2263:Prefer generic overload when type is known")]
    private void InitializeOptions()
    {
        // Initialization is moved to the view model. This method is kept for
        // compatibility but no longer manipulates UI controls directly.
    }

    private void ViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(MainWindowViewModel.SelectedBackdrop)
            or nameof(MainWindowViewModel.SelectedCorner)
            or nameof(MainWindowViewModel.IsDarkMode))
        {
            ApplySelectedEffect();
        }
    }

    private MainWindowViewModel ViewModel { get; set; }

    protected override void OnContentRendered(EventArgs e)
    {
        base.OnContentRendered(e);
        ApplySelectedEffect();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        UpdateStatus();
    }

    private void SelectionChanged_Reapply(object sender, EventArgs e)
    {
        ApplySelectedEffect();
    }

    private void DarkModeCheckBox_Changed(object sender, RoutedEventArgs e)
    {
        ApplySelectedEffect();
    }

    private void ApplySelectedEffect()
    {
        var backdrop = ViewModel?.SelectedBackdrop ?? BackdropType.None;
        var darkMode = ViewModel?.IsDarkMode == true;

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
        ViewModel.Selected = $"Backdrop: {backdrop}\nActual: {actualBackdrop}\nCorner: {GetSelectedCorner()}\nDark Mode: {ViewModel.IsDarkMode}";

        ViewModel.StatusText =
            $"OS Version      : {OSVersionHelper.OSVersion}\n" +
            $"Backdrop        : {backdrop}\n" +
            $"Actual Backdrop : {actualBackdrop}\n" +
            $"Supported       : {supported}\n" +
            $"Manual Bg Needed: {backdrop.IsManualBackgroundNeeded()}\n" +
            $"Corner          : {GetSelectedCorner()}\n" +
            $"Dark Mode       : {ViewModel.IsDarkMode}";
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
