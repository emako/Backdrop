using System.Windows.Media;
using WpfColor = System.Windows.Media.Color;

namespace MicaDrop.WPF;

internal static class ColorHelper
{
    public static global::MicaDrop.Color ToMicaColor(this WpfColor color)
    {
        return global::MicaDrop.Color.FromArgb(color.A, color.R, color.G, color.B);
    }

    public static global::MicaDrop.Color GetAcrylicColor(this System.Windows.Window window)
    {
        if (window.Background is SolidColorBrush brush)
        {
            return brush.Color.ToMicaColor();
        }

        return global::MicaDrop.Colors.Transparent;
    }
}