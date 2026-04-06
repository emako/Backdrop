using System;
using System.Windows;
using System.Windows.Interop;

namespace Backdrop.WPF;

public static class AcrylicWindowHelper
{
    public static bool Apply(Window window, bool force = false)
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

        return Acrylic10Helper.Apply(windowHandle, window.GetAcrylicColor(), force);
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

        Acrylic10Helper.Remove(windowHandle);
    }
}
