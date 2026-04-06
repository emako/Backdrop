using System.Windows;
using System.Windows.Interop;

namespace System.Backdrop.WPF;

public static class CornerWindowHelper
{
    public static bool SetWindowCorners(Window window, WindowCornerStyle preference)
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

        return CornerHelper.SetWindowCorners(windowHandle, preference);
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

        return CornerHelper.EnableBackgroundBlur(windowHandle);
    }
}
