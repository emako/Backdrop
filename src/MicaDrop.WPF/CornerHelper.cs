using System;
using System.Windows;
using System.Windows.Interop;

namespace MicaDrop.WPF;

public static class CornerHelper
{
    public static bool SetWindowCorners(Window window, global::MicaDrop.WindowCornerStyle preference)
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

        return global::MicaDrop.CornerHelper.SetWindowCorners(windowHandle, preference);
    }

    public static bool EnableBackgroundBlur(Window window)
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

        return global::MicaDrop.CornerHelper.EnableBackgroundBlur(windowHandle);
    }
}