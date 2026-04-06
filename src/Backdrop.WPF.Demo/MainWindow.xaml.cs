using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace System.Backdrop.WPF.Demo;

internal partial class MainWindow : Window
{
    private MainWindowViewModel ViewModel { get; }

    public MainWindow()
    {
        DataContext = ViewModel = new MainWindowViewModel();
        InitializeComponent();

        Loaded += MainWindow_Loaded;
        Activated += MainWindow_Activated;
        Deactivated += MainWindow_Deactivated;

        ViewModel.SelectedBackdrop = BackdropType.Acrylic11;
        ViewModel.SelectedCorner = WindowCornerStyle.Round;
        ViewModel.SelectedTheme = ViewModel.DarkModeOptions.FirstOrDefault(x => x == DarkModeOption.Dark);
        ViewModel.PropertyChanged += ViewModel_PropertyChanged;
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        ApplySelectedEffect();
    }

    private void MainWindow_Activated(object? sender, EventArgs e)
    {
        ApplySelectedEffect();
    }

    private void MainWindow_Deactivated(object? sender, EventArgs e)
    {
        //ApplySelectedEffect();
    }

    protected override void OnSourceInitialized(EventArgs e)
    {
        base.OnSourceInitialized(e);
        ApplySelectedEffect();
    }

    protected override void OnContentRendered(EventArgs e)
    {
        base.OnContentRendered(e);
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
