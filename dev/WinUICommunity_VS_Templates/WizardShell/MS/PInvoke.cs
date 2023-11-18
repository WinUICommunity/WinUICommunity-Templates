using System;
using System.Runtime.InteropServices;
using System.Text;

namespace iNKORE.UI.WPF.Modern
{
    public static class PInvoke
    {
        public const int HTCLIENT = 1;

        public const int MONITORINFOF_PRIMARY = 0x00000001;
        public const int WM_NCLBUTTONDOWN = 0x00A1;
        public const int WM_NCMOUSELEAVE = 0x02A2;
        public const int WM_NCLBUTTONUP = 0x00A2;
        public const int WM_NCHITTEST = 0x0084;

        // HT codes for WM_NCHITTEST
        public const int HTMAXBUTTON = 9;
        public enum SYSTEM_METRICS_INDEX : int
        {
            SM_CXSIZEFRAME = 32,         // Width of the sizing border
            SM_CYSIZEFRAME = 33,         // Height of the sizing border
            SM_CXPADDEDBORDER = 92,      // Width of the padded border
            SM_CYPADDEDBORDER = 93,      // Height of the padded border
            SM_CXFRAME = 32,             // Width of the window frame
            SM_CYFRAME = 33,             // Height of the window frame
            SM_CXBORDER = 5,             // Width of a window border
            SM_CYBORDER = 6,             // Height of a window border
            SM_CXEDGE = 45,              // Width of a 3D border
            SM_CYEDGE = 46,              // Height of a 3D border
                                         // Add more constants as needed
        }

        public const int WM_SETTINGCHANGE = 0x001A;
        public const int WM_WINDOWPOSCHANGING = 0x0046;
        public const int WM_WINDOWPOSCHANGED = 0x0047;

        public struct HICON
        {
            public IntPtr Handle;

            public HICON(IntPtr handle)
            {
                Handle = handle;
            }
        }

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DestroyIcon(IntPtr hIcon);

        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        [DllImport("shell32.dll", SetLastError = true)]
        public static extern uint ExtractIconEx(string lpszFile, int nIconIndex, out IntPtr phiconLarge, out IntPtr phiconSmall, uint nIcons);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern IntPtr GetActiveWindow();

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int GetCurrentPackageFullName(ref int packageFullNameLength, IntPtr packageFullName);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern uint GetModuleFileName(IntPtr hModule, [Out] StringBuilder lpFilename, uint nSize);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFO lpmi);

        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(SYSTEM_METRICS_INDEX smIndex);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

        [DllImport("user32.dll")]
        public static extern IntPtr MonitorFromRect(ref RECT lprc, MonitorFlag dwFlags);

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        // Structs and Enums

        [StructLayout(LayoutKind.Sequential)]
        public struct MONITORINFO
        {
            public int cbSize;
            public RECT rcMonitor;
            public RECT rcWork;
            public int dwFlags;
        }

        [Flags]
        public enum SET_WINDOW_POS_FLAGS : uint
        {
            SWP_NOSIZE = 0x0001,
            SWP_NOMOVE = 0x0002,
            SWP_NOZORDER = 0x0004,
            SWP_NOACTIVATE = 0x0010,
            // Add more flags as needed
        }

        public class HWND
        {
            public IntPtr Handle { get; }

            public HWND(IntPtr handle)
            {
                Handle = handle;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
            public int Width => Right - Left;
            public int Height => Bottom - Top;
            public RECT(int left, int top, int right, int bottom)
            {
                Left = left;
                Top = top;
                Right = right;
                Bottom = bottom;
            }
        }

        public enum MonitorFlag
        {
            MONITOR_DEFAULTTONULL = 0x00000000,
            MONITOR_DEFAULTTOPRIMARY = 0x00000001,
            MONITOR_DEFAULTTONEAREST = 0x00000002,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWPOS
        {
            public IntPtr hwnd;
            public IntPtr hwndInsertAfter;
            public int x;
            public int y;
            public int cx;
            public int cy;
            public uint flags;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWPLACEMENT
        {
            public int length;
            public int flags;
            public ShowWindowCommands showCmd;
            public POINT ptMinPosition;
            public POINT ptMaxPosition;
            public RECT rcNormalPosition;
        }

        public enum ShowWindowCommands : int
        {
            SW_HIDE = 0,
            SW_SHOWNORMAL = 1,
            SW_NORMAL = 1,
            SW_SHOWMINIMIZED = 2,
            SW_SHOWMAXIMIZED = 3,
            SW_MAXIMIZE = 3,
            SW_SHOWNOACTIVATE = 4,
            SW_SHOW = 5,
            SW_MINIMIZE = 6,
            SW_SHOWMINNOACTIVE = 7,
            SW_SHOWNA = 8,
            SW_RESTORE = 9,
            SW_SHOWDEFAULT = 10,
            SW_FORCEMINIMIZE = 11,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;
        }
    }
}
