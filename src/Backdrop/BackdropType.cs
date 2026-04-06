namespace System.Backdrop;

public enum BackdropType
{
    None = 1,
    Mica = 2,
    Acrylic = 3, // Automatically Acrylic. Automatically selects the best Acrylic effect available on the system (Acrylic11 > Acrylic10)
    Tabbed = 4, // Mica Alt
    Acrylic10 = 5, // Acrylic. Windows 10 style, supported on Windows 10 and 11
    Acrylic11 = 6, // Desktop Acrylic. Windows 11 style, supported on Windows 11 22523+ (Insider) and 22621+ (Stable)
}
