using System;

namespace MicaDrop;

/// <summary>
/// Represents an ARGB color.
/// </summary>
public readonly struct Color : IEquatable<Color>
{
    public byte A { get; }
    public byte R { get; }
    public byte G { get; }
    public byte B { get; }

    public Color(byte r, byte g, byte b, byte a = 255)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }

    public static Color FromArgb(byte a, byte r, byte g, byte b) => new Color(r, g, b, a);

    public static Color FromRgb(byte r, byte g, byte b) => new Color(r, g, b, 255);

    public bool Equals(Color other) => A == other.A && R == other.R && G == other.G && B == other.B;

    public override bool Equals(object obj) => obj is Color other && Equals(other);

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

    public static bool operator ==(Color left, Color right) => left.Equals(right);

    public static bool operator !=(Color left, Color right) => !left.Equals(right);

    public override string ToString() => $"#{A:X2}{R:X2}{G:X2}{B:X2}";
}

/// <summary>
/// Predefined <see cref="Color"/> values.
/// </summary>
public static class Colors
{
    public static Color Transparent => new Color(0, 0, 0, 0);
    public static Color White => new Color(255, 255, 255, 255);
    public static Color Black => new Color(0, 0, 0, 255);
}
