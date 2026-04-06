using System.Windows;
using System.Windows.Media;

namespace System.Backdrop.WPF;

internal static class ColorHelper
{
    public static BackdropColor ToMicaColor(this Color color)
    {
        return BackdropColor.FromArgb(color.A, color.R, color.G, color.B);
    }

    public static BackdropColor GetAcrylicColor(this Window window)
    {
        if (window.Background is SolidColorBrush brush)
        {
            return brush.Color.ToMicaColor();
        }

        return BackdropColors.Transparent;
    }
}
