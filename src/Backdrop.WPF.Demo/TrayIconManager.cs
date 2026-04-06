using System.IO;
using System.IO.Packaging;
using System.NativeTray;
using System.Windows;
using System.Windows.Resources;

namespace System.Backdrop.WPF.Demo;

internal partial class TrayIconManager
{
    private static TrayIconManager _instance = null!;

    private readonly TrayIconHost? _iconHost = null;

    private TrayIconManager()
    {
        using Win32Icon icon = new(ResourceHelper.GetStream("pack://application:,,,/Backdrop.WPF.Demo;component/logo.ico"));

        _iconHost = new TrayIconHost()
        {
            ToolTipText = "Backdrop.WPF.Demo",
            Icon = icon.Handle,
            ThemeMode = TrayThemeMode.System,
            Menu =
            [
                new TrayMenuItem()
                {
                    Header = "Exit",
                    Command = new TrayCommand(Exit),
                }
            ],
        };

        _iconHost.LeftDoubleClick += (_, _) => ActivateOrRestoreMainWindow();
    }

    public static TrayIconManager GetInstance()
    {
        return _instance ??= new TrayIconManager();
    }

    public static void Start()
    {
        _ = GetInstance();
    }
}

internal partial class TrayIconManager
{
    private static void ActivateOrRestoreMainWindow()
    {
        if (Application.Current.MainWindow is not null)
        {
            if (Application.Current.MainWindow.IsVisible)
            {
                Application.Current.MainWindow.Hide();
            }
            else
            {
                Application.Current.MainWindow.Show();
                Application.Current.MainWindow.Activate();
            }
        }
    }

    private void Exit(object? commandParameter)
    {
        Application.Current.Shutdown();
    }
}

file static class ResourceHelper
{
    static ResourceHelper()
    {
        if (!UriParser.IsKnownScheme("pack"))
            _ = PackUriHelper.UriSchemePack;
    }

    public static Stream GetStream(string uriString)
    {
        Uri uri = new(uriString);
        StreamResourceInfo info = Application.GetResourceStream(uri);
        return info?.Stream!;
    }
}
