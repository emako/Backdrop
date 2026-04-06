using System.Windows;

namespace System.Backdrop.WPF.Demo;

public partial class App : Application
{
    public App()
    {
        TrayIconManager.Start();
        InitializeComponent();
    }
}
