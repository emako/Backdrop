using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using BackdropHelper = global::MicaDrop.BackdropHelper;
using BackdropType = global::MicaDrop.BackdropType;
using CornerHelper = global::MicaDrop.CornerHelper;
using MicaColor = global::MicaDrop.Color;
using OSVersionHelper = global::MicaDrop.OSVersionHelper;
using WpfColor = System.Windows.Media.Color;
using WindowCornerStyle = global::MicaDrop.WindowCornerStyle;

namespace MicaDrop.WPF;

public partial class MainWindow : Window
{
    private readonly IReadOnlyList<AcrylicColorOption> _acrylicOptions =
    [
        new AcrylicColorOption("Slate", MicaColor.FromArgb(255, 95, 109, 140)),
        new AcrylicColorOption("Ocean", MicaColor.FromArgb(255, 44, 122, 167)),
        new AcrylicColorOption("Forest", MicaColor.FromArgb(255, 49, 104, 84)),
        new AcrylicColorOption("Rose", MicaColor.FromArgb(255, 157, 77, 98)),
        new AcrylicColorOption("Amber", MicaColor.FromArgb(255, 171, 117, 48))
    ];

    private IntPtr _windowHandle;

    public MainWindow()
    {
        InitializeComponent();
        InitializeOptions();
        UpdateSurfaceTheme();
    }

    protected override void OnSourceInitialized(EventArgs e)
    {
        base.OnSourceInitialized(e);
        _windowHandle = new WindowInteropHelper(this).Handle;
        ApplySelectedEffect();
    }

    private void InitializeOptions()
    {
        BackdropComboBox.ItemsSource = Enum.GetValues(typeof(BackdropType));
        BackdropComboBox.SelectedItem = BackdropType.Mica;

        CornerComboBox.ItemsSource = Enum.GetValues(typeof(WindowCornerStyle));
        CornerComboBox.SelectedItem = WindowCornerStyle.Round;

        AcrylicColorComboBox.ItemsSource = _acrylicOptions;
        AcrylicColorComboBox.SelectedIndex = 0;

        DarkModeCheckBox.IsChecked = true;
        OsVersionTextBlock.Text = OSVersionHelper.OSVersion.ToString();
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
        UpdateSurfaceTheme();
        ApplySelectedEffect();
    }

    private void ApplySelectedEffect()
    {
        if (_windowHandle == IntPtr.Zero)
        {
            return;
        }

        var backdrop = GetSelectedBackdrop();
        var acrylicOption = GetSelectedAcrylicColor();
        var darkMode = DarkModeCheckBox.IsChecked == true;

        BackdropHelper.Remove(_windowHandle);

        if (darkMode)
        {
            BackdropHelper.ApplyDarkMode(_windowHandle);
        }
        else
        {
            BackdropHelper.RemoveDarkMode(_windowHandle);
        }

        CornerHelper.SetWindowCorners(_windowHandle, GetSelectedCorner());
        BackdropHelper.Apply(_windowHandle, backdrop, acrylic10Color: acrylicOption.Color);

        Background = CreateWindowBackgroundBrush(backdrop, acrylicOption.Color);
        UpdateSurfaceTheme();
        UpdateStatus();
    }

    private Brush CreateWindowBackgroundBrush(BackdropType backdrop, MicaColor acrylicColor)
    {
        if (backdrop == BackdropType.None)
        {
            return new SolidColorBrush(WpfColor.FromRgb(242, 244, 247));
        }

        if (backdrop.GetActualBackdropType() == BackdropType.Acrylic10)
        {
            return new SolidColorBrush(WpfColor.FromArgb(28, acrylicColor.R, acrylicColor.G, acrylicColor.B));
        }

        return Brushes.Transparent;
    }

    private void UpdateSurfaceTheme()
    {
        var darkMode = DarkModeCheckBox?.IsChecked == true;
        var foreground = darkMode ? Brushes.White : new SolidColorBrush(WpfColor.FromRgb(24, 28, 36));
        var panelBackground = darkMode
            ? new SolidColorBrush(WpfColor.FromArgb(132, 17, 24, 39))
            : new SolidColorBrush(WpfColor.FromArgb(170, 255, 255, 255));
        var previewBackground = darkMode
            ? new SolidColorBrush(WpfColor.FromArgb(88, 10, 16, 30))
            : new SolidColorBrush(WpfColor.FromArgb(132, 255, 255, 255));
        var borderBrush = darkMode
            ? new SolidColorBrush(WpfColor.FromArgb(68, 255, 255, 255))
            : new SolidColorBrush(WpfColor.FromArgb(40, 15, 23, 42));

        Foreground = foreground;
        ControlsPanel.Background = panelBackground;
        ControlsPanel.BorderBrush = borderBrush;
        PreviewCard.Background = previewBackground;
        PreviewCard.BorderBrush = borderBrush;
        StatusTextBlock.Foreground = foreground;
    }

    private void UpdateStatus()
    {
        var backdrop = GetSelectedBackdrop();
        var actualBackdrop = backdrop.GetActualBackdropType();
        var supported = backdrop.IsSupported();
        var acrylicOption = GetSelectedAcrylicColor();

        SupportedTextBlock.Text = supported ? "Yes" : "No";
        SelectedTextBlock.Text = $"Backdrop: {backdrop}\nActual: {actualBackdrop}\nCorner: {GetSelectedCorner()}\nDark Mode: {DarkModeCheckBox.IsChecked == true}";
        AcrylicColorTextBlock.Text = $"{acrylicOption.Name} {acrylicOption.Color}";

        StatusTextBlock.Text =
            $"OS Version      : {OSVersionHelper.OSVersion}\n" +
            $"Backdrop        : {backdrop}\n" +
            $"Actual Backdrop : {actualBackdrop}\n" +
            $"Supported       : {supported}\n" +
            $"Manual Bg Needed: {backdrop.IsManualBackgroundNeeded()}\n" +
            $"Corner          : {GetSelectedCorner()}\n" +
            $"Dark Mode       : {DarkModeCheckBox.IsChecked == true}\n" +
            $"Acrylic Tint    : {acrylicOption.Name} {acrylicOption.Color}";
    }

    private BackdropType GetSelectedBackdrop()
    {
        return BackdropComboBox.SelectedItem is BackdropType backdrop
            ? backdrop
            : BackdropType.None;
    }

    private WindowCornerStyle GetSelectedCorner()
    {
        return CornerComboBox.SelectedItem is WindowCornerStyle corner
            ? corner
            : WindowCornerStyle.Default;
    }

    private AcrylicColorOption GetSelectedAcrylicColor()
    {
        return AcrylicColorComboBox.SelectedItem as AcrylicColorOption ?? _acrylicOptions[0];
    }

    private sealed class AcrylicColorOption
    {
        public AcrylicColorOption(string name, MicaColor color)
        {
            Name = name;
            Color = color;
        }

        public string Name { get; }

        public MicaColor Color { get; }
    }
}