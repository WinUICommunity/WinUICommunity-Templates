﻿using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace iNKORE.UI.WPF.Modern
{
    public static class OSVersionHelper
    {
        public static readonly Version OSVersion = GetOSVersion();

        public static bool IsWindowsNT { get; } = Environment.OSVersion.Platform == PlatformID.Win32NT;

        public static bool IsWindowsVistaOrGreater { get; } = IsWindowsNT && OSVersion >= new Version(6, 0);

        public static bool IsWindows7OrGreater { get; } = IsWindowsNT && OSVersion >= new Version(6, 1);

        public static bool IsWindows8OrGreater { get; } = IsWindowsNT && OSVersion >= new Version(6, 2);

        public static bool IsWindows81OrGreater { get; } = IsWindowsNT && OSVersion >= new Version(6, 2);

        public static bool IsWindows10OrGreater { get; } = IsWindowsNT && OSVersion >= new Version(10, 0);

        public static bool IsWindows11OrGreater { get; } = IsWindowsNT && OSVersion >= new Version(10, 0, 21996);

        private static Version GetOSVersion()
        {
            var osv = new RTL_OSVERSIONINFOEX();
            osv.dwOSVersionInfoSize = (uint)Marshal.SizeOf(osv);
            int ret = RtlGetVersion(out osv);
            Debug.Assert(ret == 0);
            return new Version((int)osv.dwMajorVersion, (int)osv.dwMinorVersion, (int)osv.dwBuildNumber);
        }

        [DllImport("ntdll.dll")]
        private static extern int RtlGetVersion(out RTL_OSVERSIONINFOEX lpVersionInformation);

        [StructLayout(LayoutKind.Sequential)]
        private struct RTL_OSVERSIONINFOEX
        {
            internal uint dwOSVersionInfoSize;
            internal uint dwMajorVersion;
            internal uint dwMinorVersion;
            internal uint dwBuildNumber;
            internal uint dwPlatformId;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            internal string szCSDVersion;
        }
    }
}
