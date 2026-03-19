using System;
using System.Windows;
using System.Windows.Interop;

namespace MicaDrop.WPF;

public static class AcrylicHelper
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

        return global::MicaDrop.Acrylic10Helper.Apply(windowHandle, window.GetAcrylicColor(), force);
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

        global::MicaDrop.Acrylic10Helper.Remove(windowHandle);
    }
}