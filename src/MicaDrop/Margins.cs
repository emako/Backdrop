using System.Runtime.InteropServices;

namespace MicaDrop;

[StructLayout(LayoutKind.Sequential)]
public struct Margins
{
    public int LeftWidth;
    public int RightWidth;
    public int TopHeight;
    public int BottomHeight;
}
