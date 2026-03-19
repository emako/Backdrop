using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using WpfColor = System.Windows.Media.Color;

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

        global::MicaDrop.BackdropHelper.Remove(windowHandle);
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