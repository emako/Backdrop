using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace Backdrop.WPF;

public static class BackdropWindowHelper
{
    public static bool Apply(Window window, BackdropType type, bool force = false)
    {
        if (window == null)
        {
            return false;
        }

        return Apply(window, type, window.Background is SolidColorBrush brush ? brush.Color : null, force);
    }

    public static bool Apply(Window window, BackdropType type, Color? acrylic10Color, bool force = false)
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

        return BackdropHelper.Apply(
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
        BackdropHelper.Remove(windowHandle);
    }

    private static void PrepareClientArea(Window window, IntPtr windowHandle)
    {
        if (ShouldUseTransparentCompositionTarget(window) && HwndSource.FromHwnd(windowHandle) is HwndSource hwndSource)
        {
            hwndSource.CompositionTarget.BackgroundColor = Colors.Transparent;
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

        return global::Backdrop.BackdropHelper.DwmExtendFrameIntoClientArea(windowHandle, ref margins);
    }

    private static bool ShouldUseTransparentCompositionTarget(Window window)
    {
        if (window.AllowsTransparency)
        {
            return false;
        }

        return true;
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

            BackdropHelper.ApplyDarkMode(windowHandle);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
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

            BackdropHelper.RemoveDarkMode(windowHandle);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }
    }
}
