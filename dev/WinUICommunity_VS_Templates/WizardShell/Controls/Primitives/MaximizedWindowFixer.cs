﻿using Standard;
using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;

namespace iNKORE.UI.WPF.Modern
{
    internal class MaximizedWindowFixer
    {
        #region MaximizedWindowFixer

        public static readonly DependencyProperty MaximizedWindowFixerProperty =
            DependencyProperty.RegisterAttached(
                "MaximizedWindowFixer",
                typeof(MaximizedWindowFixer),
                typeof(MaximizedWindowFixer),
                new PropertyMetadata(OnMaximizedWindowFixerChanged));

        public static MaximizedWindowFixer GetMaximizedWindowFixer(Window window)
        {
            return (MaximizedWindowFixer)window.GetValue(MaximizedWindowFixerProperty);
        }

        public static void SetMaximizedWindowFixer(Window window, MaximizedWindowFixer value)
        {
            window.SetValue(MaximizedWindowFixerProperty, value);
        }

        private static void OnMaximizedWindowFixerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue is MaximizedWindowFixer oldValue)
            {
                oldValue.UnsetWindow();
            }

            if (e.NewValue is MaximizedWindowFixer newValue)
            {
                newValue.SetWindow((Window)d);
            }
        }

        #endregion

        private Thickness MaximizedWindowBorder => _maximizedWindowBorder ??= GetMaximizedWindowBorder();

        private bool IsWindowPosAdjusted
        {
            get => _isWindowPosAdjusted;
            set
            {
                if (_isWindowPosAdjusted != value)
                {
                    _isWindowPosAdjusted = value;
                    InvalidateMaximizedWindowBorder();
                }
            }
        }

        private void SetWindow(Window window)
        {
            UnsubscribeWindowEvents();

            _window = window;
            _hwnd = new WindowInteropHelper(window).Handle;

            _window.StateChanged += WindowStateChanged;
#if NET462_OR_NEWER
            _window.DpiChanged += WindowDpiChanged;
#endif
            _window.Closed += WindowClosed;

            if (_hwnd != IntPtr.Zero)
            {
                WindowSourceInitialized(null, null);
            }
            else
            {
                _window.SourceInitialized += WindowSourceInitialized;
            }
        }

        private void UnsetWindow()
        {
            UnsubscribeWindowEvents();
        }

        private void UnsubscribeWindowEvents()
        {
            if (_window != null)
            {
                _window.SourceInitialized -= WindowSourceInitialized;
                _window.StateChanged -= WindowStateChanged;
#if NET462_OR_NEWER
                _window.DpiChanged -= WindowDpiChanged;
#endif
                _window.Closed -= WindowClosed;
                _window.ClearValue(Control.PaddingProperty);
                _window = null;
            }

            if (_hwndSource != null)
            {
                _hwndSource.RemoveHook(new HwndSourceHook(WindowFilterMessage));
                _hwndSource = null;
            }

            _hwnd = IntPtr.Zero;
            _maximizedWindowBorder = null;
            _isWindowPosAdjusted = false;
        }

        private void WindowSourceInitialized(object sender, EventArgs e)
        {
            _hwnd = new WindowInteropHelper(_window).Handle;
            _hwndSource = HwndSource.FromHwnd(_hwnd);
            _hwndSource.AddHook(new HwndSourceHook(WindowFilterMessage));

            UpdateWindowPadding();
        }

        private void WindowStateChanged(object sender, EventArgs e)
        {
            UpdateWindowPadding();
        }

#if NET462_OR_NEWER
        private void WindowDpiChanged(object sender, DpiChangedEventArgs e)
        {
            InvalidateMaximizedWindowBorder();
            UpdateWindowPadding();
        }
#endif

        private void WindowClosed(object sender, EventArgs e)
        {
            UnsetWindow();
        }

        private IntPtr WindowFilterMessage(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            IntPtr retInt = IntPtr.Zero;
            uint message = (uint)msg;
            
            switch (message)
            {
                case PInvoke.WM_SETTINGCHANGE:
                    InvalidateMaximizedWindowBorder();
                    UpdateWindowPadding();
                    break;
                case PInvoke.WM_WINDOWPOSCHANGING:
                    OnWindowPosChanging(lParam);
                    break;
                case PInvoke.WM_WINDOWPOSCHANGED:
                    if (!_maximizedWindowBorder.HasValue)
                    {
                        UpdateWindowPadding();
                    }
                    break;
            }

            return retInt;
        }

        private unsafe void OnWindowPosChanging(IntPtr lParam)
        {
            var pos = (PInvoke.WINDOWPOS)Marshal.PtrToStructure(lParam, typeof(PInvoke.WINDOWPOS));
            if ((pos.flags & (int)PInvoke.SET_WINDOW_POS_FLAGS.SWP_NOSIZE) == 0)
            {
                bool windowPosAdjusted = false;
                try
                {
                    PInvoke.WINDOWPLACEMENT placement = GetWindowPlacement(pos.hwnd);
                    if (placement.showCmd == PInvoke.ShowWindowCommands.SW_MAXIMIZE)
                    {
                        if (GetTaskbarAutoHide(out ABEdge edge))
                        {
                            PInvoke.RECT rect = new PInvoke.RECT(pos.x, pos.y, pos.x + pos.cx, pos.y + pos.cy);
                            IntPtr monitor = MonitorFromRect(ref rect, PInvoke.MonitorFlag.MONITOR_DEFAULTTONEAREST);
                            if (monitor != IntPtr.Zero)
                            {
                                PInvoke.MONITORINFO info = GetMonitorInfo(monitor);
                                bool primary = (info.dwFlags & PInvoke.MONITORINFOF_PRIMARY) != 0;
                                if (primary)
                                {
                                    if (pos.x < 0 &&
                                        pos.y < 0 &&
                                        pos.cx > info.rcMonitor.Width &&
                                        pos.cy > info.rcMonitor.Height)
                                    {
                                        AdjustWindowPosForTaskbarAutoHide(ref pos, edge);
                                        Marshal.StructureToPtr(pos, lParam, true);
                                        windowPosAdjusted = true;
                                    }
                                    else if (pos.x == 0 && pos.y == 0)
                                    {
                                        windowPosAdjusted = true;
                                    }
                                }
                            }
                        }
                    }
                }
                catch { }

                IsWindowPosAdjusted = windowPosAdjusted;
            }
        }

        private Thickness GetMaximizedWindowBorder()
        {
            if (IsWindowPosAdjusted)
            {
                return new Thickness();
            }

            double dpiScaleX, dpiScaleY;
#if NET462_OR_NEWER
            DpiScale dpi = VisualTreeHelper.GetDpi(_window);
            dpiScaleX = dpi.DpiScaleX;
            dpiScaleY = dpi.DpiScaleY;
#else
            Matrix transformToDevice = _hwndSource.CompositionTarget.TransformToDevice;
            dpiScaleX = transformToDevice.M11;
            dpiScaleY = transformToDevice.M22;
#endif

            int frameWidth = PInvoke.GetSystemMetrics(PInvoke.SYSTEM_METRICS_INDEX.SM_CXSIZEFRAME);
            int frameHeight = PInvoke.GetSystemMetrics(PInvoke.SYSTEM_METRICS_INDEX.SM_CYSIZEFRAME);
            int borderPadding = PInvoke.GetSystemMetrics(PInvoke.SYSTEM_METRICS_INDEX.SM_CXPADDEDBORDER);
            Size borderSize = new Size(frameWidth + borderPadding, frameHeight + borderPadding);
            Size borderSizeInDips = DpiHelper.DeviceSizeToLogical(borderSize, dpiScaleX, dpiScaleY);

            return new Thickness(borderSizeInDips.Width, borderSizeInDips.Height, borderSizeInDips.Width, borderSizeInDips.Height);
        }

        private void InvalidateMaximizedWindowBorder()
        {
            _maximizedWindowBorder = null;
        }

        private void UpdateWindowPadding()
        {
            if (_hwndSource == null || _hwndSource.IsDisposed || _hwndSource.CompositionTarget == null)
            {
                return;
            }

            if (_window.WindowState == WindowState.Maximized)
            {
                _window.Padding = MaximizedWindowBorder;
            }
            else
            {
                _window.ClearValue(Control.PaddingProperty);
            }
        }

        private static bool GetTaskbarAutoHide(out ABEdge edge)
        {
            IntPtr trayWnd = PInvoke.FindWindow("Shell_TrayWnd", null);
            if (trayWnd != IntPtr.Zero)
            {
                APPBARDATA abd = new APPBARDATA();
                abd.cbSize = Marshal.SizeOf(abd);
                abd.hWnd = trayWnd;
                SHAppBarMessage(ABMsg.ABM_GETTASKBARPOS, ref abd);
                bool autoHide = Convert.ToBoolean(SHAppBarMessage(ABMsg.ABM_GETSTATE, ref abd));
                edge = autoHide ? GetEdge(abd.rc) : default;
                return autoHide;
            }

            edge = default;
            return false;

            static ABEdge GetEdge(PInvoke.RECT rc)
            {
                if (rc.Top == rc.Left && rc.Bottom > rc.Right)
                {
                    return ABEdge.ABE_LEFT;
                }
                else if (rc.Top == rc.Left && rc.Bottom < rc.Right)
                {
                    return ABEdge.ABE_TOP;
                }
                else if (rc.Top > rc.Left)
                {
                    return ABEdge.ABE_BOTTOM;
                }
                else
                {
                    return ABEdge.ABE_RIGHT;
                }
            }
        }

        private static void AdjustWindowPosForTaskbarAutoHide(ref PInvoke.WINDOWPOS pos, ABEdge edge)
        {
            pos.cx += pos.x * 2;
            pos.cy += pos.y * 2;
            pos.x = 0;
            pos.y = 0;

            switch (edge)
            {
                case ABEdge.ABE_LEFT:
                    pos.x = 2;
                    pos.cx -= 2;
                    break;
                case ABEdge.ABE_TOP:
                    pos.y = 2;
                    pos.cy -= 2;
                    break;
                case ABEdge.ABE_RIGHT:
                    pos.cx -= 2;
                    break;
                case ABEdge.ABE_BOTTOM:
                    pos.cy -= 2;
                    break;
            }
        }

        #region Win32 Interop

        private enum ABEdge
        {
            ABE_LEFT = 0,
            ABE_TOP = 1,
            ABE_RIGHT = 2,
            ABE_BOTTOM = 3
        }

        private enum ABMsg
        {
            ABM_GETSTATE = 4,
            ABM_GETTASKBARPOS = 5,
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct APPBARDATA
        {
            public int cbSize;
            public IntPtr hWnd;
            public int uCallbackMessage;
            public int uEdge;
            public PInvoke.RECT rc;
            public bool lParam;
        }

        [DllImport("shell32", CallingConvention = CallingConvention.StdCall)]
        private static extern uint SHAppBarMessage(ABMsg dwMessage, ref APPBARDATA pData);

        [DllImport("user32.dll", EntryPoint = "GetMonitorInfo", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool _GetMonitorInfo(IntPtr hMonitor, [In, Out] PInvoke.MONITORINFO lpmi);

        private static PInvoke.MONITORINFO GetMonitorInfo(IntPtr hMonitor)
        {
            var mi = new PInvoke.MONITORINFO();
            if (!_GetMonitorInfo(hMonitor, mi))
            {
                throw new Win32Exception();
            }
            return mi;
        }

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowPlacement(IntPtr hwnd, PInvoke.WINDOWPLACEMENT lpwndpl);

        private static PInvoke.WINDOWPLACEMENT GetWindowPlacement(IntPtr hwnd)
        {
            PInvoke.WINDOWPLACEMENT wndpl = new PInvoke.WINDOWPLACEMENT();
            if (GetWindowPlacement(hwnd, wndpl))
            {
                return wndpl;
            }
            throw new Win32Exception();
        }

        [DllImport("user32.dll", ExactSpelling = true)]
        private static extern IntPtr MonitorFromRect(ref PInvoke.RECT rect, PInvoke.MonitorFlag flags);

        #endregion

        private Window _window;
        private IntPtr _hwnd;
        private HwndSource _hwndSource;
        private Thickness? _maximizedWindowBorder;
        private bool _isWindowPosAdjusted;
    }
}
