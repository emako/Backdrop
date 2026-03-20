using System;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Shell;
using WpfColor = System.Windows.Media.Color;
using WpfColors = System.Windows.Media.Colors;

namespace MicaDrop.WPF;

public static class BackdropHelper
{
    public static bool Apply(Window window, global::MicaDrop.BackdropType type, bool force = false)
    {
        if (window == null)
        {
            return false;
        }

        return Apply(window, type, window.Background is SolidColorBrush brush ? brush.Color : (WpfColor?)null, force);
    }

    public static bool Apply(Window window, global::MicaDrop.BackdropType type, WpfColor? acrylic10Color, bool force = false)
    {
        if (window == null)
        {
            return false;
        }

        var windowHandle = new WindowInteropHelper(window).EnsureHandle();

        if (windowHandle == IntPtr.Zero)
        {
            return false;
        }

        PrepareClientArea(window, windowHandle);

        return global::MicaDrop.BackdropHelper.Apply(
            windowHandle,
            type,
            force,
            acrylic10Color?.ToMicaColor() ?? window.GetAcrylicColor());
    }

    public static void Remove(Window window)
    {
        if (window == null)
        {
            return;
        }

        var windowHandle = new WindowInteropHelper(window).EnsureHandle();

        if (windowHandle == IntPtr.Zero)
        {
            return;
        }

        PrepareClientArea(window, windowHandle);

        global::MicaDrop.BackdropHelper.Remove(windowHandle);
    }

    private static void PrepareClientArea(Window window, IntPtr windowHandle)
    {
        if (ShouldUseTransparentCompositionTarget(window) && HwndSource.FromHwnd(windowHandle) is HwndSource hwndSource)
        {
            hwndSource.CompositionTarget.BackgroundColor = WpfColors.Transparent;
        }

        window.Background = Brushes.Transparent;

        var margins = new Margins()
        {
            LeftWidth = -1,
            RightWidth = -1,
            TopHeight = -1,
            BottomHeight = -1,
        };

        DwmExtendFrameIntoClientArea(window, ref margins);
    }

    public static bool DwmExtendFrameIntoClientArea(Window window, ref Margins margins)
    {
        var windowHandle = new WindowInteropHelper(window).EnsureHandle();

        if (windowHandle == IntPtr.Zero)
        {
            return false;
        }

        return global::MicaDrop.BackdropHelper.DwmExtendFrameIntoClientArea(windowHandle, ref margins);
    }

    private static bool ShouldUseTransparentCompositionTarget(Window window)
    {
        if (window.AllowsTransparency)
        {
            return false;
        }

        return WindowChrome.GetWindowChrome(window) == null;
    }

    public static void ApplyDarkMode(this Window window)
    {
        if (window == null)
        {
            return;
        }

        try
        {
            var windowHandle = new WindowInteropHelper(window).EnsureHandle();

            if (windowHandle == IntPtr.Zero)
            {
                return;
            }

            global::MicaDrop.BackdropHelper.ApplyDarkMode(windowHandle);
        }
        catch
        {
        }
    }

    public static void RemoveDarkMode(this Window window)
    {
        if (window == null)
        {
            return;
        }

        try
        {
            var windowHandle = new WindowInteropHelper(window).EnsureHandle();

            if (windowHandle == IntPtr.Zero)
            {
                return;
            }

            global::MicaDrop.BackdropHelper.RemoveDarkMode(windowHandle);
        }
        catch
        {
        }
    }
}
