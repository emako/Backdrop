using System.Runtime.InteropServices;

namespace System.Backdrop.WinApi;

/// <summary>
/// This header is used by multiple technologies.
/// </summary>
internal static class User32
{
    /// <summary>
    /// Retrieves information about the specified window.
    /// The function also retrieves the 32-bit (DWORD) value at the specified offset into the extra window memory.
    /// </summary>
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern int GetWindowLong(nint hWnd, int nIndex);

    /// <summary>
    /// Changes an attribute of the specified window.
    /// The function also sets the 32-bit (long) value at the specified offset into the extra window memory.
    /// </summary>
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern int SetWindowLong(nint hWnd, int nIndex, int dwNewLong);
}
