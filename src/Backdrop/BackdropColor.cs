namespace System.Backdrop;

/// <summary>
/// Represents an ARGB color.
/// </summary>
public readonly struct BackdropColor(byte r, byte g, byte b, byte a = 255) : IEquatable<BackdropColor>
{
    public byte A { get; } = a;
    public byte R { get; } = r;
    public byte G { get; } = g;
    public byte B { get; } = b;

    public static BackdropColor FromArgb(byte a, byte r, byte g, byte b) => new(r, g, b, a);

    public static BackdropColor FromRgb(byte r, byte g, byte b) => new(r, g, b, 255);

    public bool Equals(BackdropColor other) => A == other.A && R == other.R && G == other.G && B == other.B;

    public override bool Equals(object obj) => obj is BackdropColor other && Equals(other);

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 31 + A.GetHashCode();
            hash = hash * 31 + R.GetHashCode();
            hash = hash * 31 + G.GetHashCode();
            hash = hash * 31 + B.GetHashCode();
            return hash;
        }
    }

    public static bool operator ==(BackdropColor left, BackdropColor right) => left.Equals(right);

    public static bool operator !=(BackdropColor left, BackdropColor right) => !left.Equals(right);

    public override string ToString() => $"#{A:X2}{R:X2}{G:X2}{B:X2}";
}

/// <summary>
/// Predefined <see cref="BackdropColor"/> values.
/// </summary>
public static class BackdropColors
{
    public static BackdropColor Transparent => new(0, 0, 0, 0);
    public static BackdropColor White => new(255, 255, 255, 255);
    public static BackdropColor Black => new(0, 0, 0, 255);
}
