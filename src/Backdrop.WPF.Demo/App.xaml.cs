using System.Windows;

namespace Backdrop.WPF.Demo;

public partial class App : Application
{
    public App()
    {
        TrayIconManager.Start();
        InitializeComponent();
    }
}
