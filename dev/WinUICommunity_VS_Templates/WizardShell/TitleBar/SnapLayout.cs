﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;

namespace iNKORE.UI.WPF.Modern
{
    /// <summary>
    /// Brings the Snap Layout functionality from Windows 11 to a custom <see cref="Controls.TitleBar"/>.
    /// </summary>
    public class SnapLayout
    {
        public SolidColorBrush DefaultButtonBackground { get; set; } = Brushes.Transparent;

        private bool _isButtonFocused;

        private bool _isButtonClicked;

        private double _dpiScale;

        private Button _button;

        private SolidColorBrush _hoverColor;
        private SolidColorBrush _pressedColor;

        public void Register(Button button)
        {
            _isButtonFocused = false;
            _button = button;

            HwndSource hwnd = (HwndSource)PresentationSource.FromVisual(button);

#if NET462_OR_NEWER
            _dpiScale = VisualTreeHelper.GetDpi(button).DpiScaleX;
#else
            Matrix transformToDevice = hwnd.CompositionTarget.TransformToDevice;
            _dpiScale = transformToDevice.M11;
#endif

            SetHoverColor();

            if (hwnd != null) hwnd.AddHook(HwndSourceHook);

            _button.IsHitTestVisible = false;
        }

        public static bool IsSupported => OSVersionHelper.IsWindows11OrGreater;

        /// <summary>
        /// Represents the method that handles Win32 window messages.
        /// </summary>
        /// <param name="hWnd">The window handle.</param>
        /// <param name="uMsg">The message ID.</param>
        /// <param name="wParam">The message's wParam value.</param>
        /// <param name="lParam">The message's lParam value.</param>
        /// <param name="handled">A value that indicates whether the message was handled. Set the value to <see langword="true"/> if the message was handled; otherwise, <see langword="false"/>.</param>
        /// <returns>The appropriate return value depends on the particular message. See the message documentation details for the Win32 message being handled.</returns>
        private IntPtr HwndSourceHook(IntPtr hWnd, int uMsg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            // TODO: This whole class is one big todo

            uint mouseNotification = (uint)uMsg;

            switch (mouseNotification)
            {
                case PInvoke.WM_NCLBUTTONDOWN:
                    if (IsOverButton(wParam, lParam))
                    {
                        _isButtonClicked = true;
                        RefreshButtonColor();
                        handled = true;
                    }
                    break;

                case PInvoke.WM_NCMOUSELEAVE:
                    _isButtonFocused = false;
                    RefreshButtonColor();
                    break;

                case PInvoke.WM_NCLBUTTONUP:
                    if (_isButtonClicked)
                    {
                        if (IsOverButton(wParam, lParam))
                        {
                            RaiseButtonClick();
                        }
                        _isButtonClicked = false;
                        RefreshButtonColor();
                    }
                    break;

                case PInvoke.WM_NCHITTEST:
                    if (IsOverButton(wParam, lParam))
                    {
                        _isButtonFocused = true;
                        RefreshButtonColor();
                        handled = true;
                    }
                    else
                    {
                        _isButtonFocused = false;
                        _isButtonClicked = false;
                        RefreshButtonColor();
                    }
                    return new IntPtr(PInvoke.HTMAXBUTTON);

                default:
                    handled = false;
                    break;
            }
            return new IntPtr(PInvoke.HTCLIENT);
        }

        private void RefreshButtonColor()
        {
            if (_isButtonClicked)
            {
                _button.Background = _pressedColor;
            }
            else
            {
                if (_isButtonFocused)
                {
                    _button.Background = _hoverColor;
                }
                else
                {
                    _button.Background = DefaultButtonBackground;
                }
            }
        }


        private bool IsOverButton(IntPtr wParam, IntPtr lParam)
        {
            try
            {
                int positionX = lParam.ToInt32() & 0xffff;
                int positionY = lParam.ToInt32() >> 16;

                Rect rect = new Rect(_button.PointToScreen(new Point()),
                    new Size(_button.Width * _dpiScale, _button.Height * _dpiScale));

                if (rect.Contains(new Point(positionX, positionY)))
                    return true;
            }
            catch (OverflowException)
            {
                return true; // or not to true, that is the question
            }

            return false;
        }

        private void RaiseButtonClick()
        {
            if (_button.IsEnabled && new ButtonAutomationPeer(_button).GetPattern(PatternInterface.Invoke) is IInvokeProvider invokeProv)
                invokeProv?.Invoke();
        }

        private void SetHoverColor()
        {
            _hoverColor = (SolidColorBrush)Application.Current.Resources["SystemControlHighlightListLowBrush"] ?? new SolidColorBrush(Color.FromArgb(21, 255, 255, 255));
            _pressedColor = (SolidColorBrush)Application.Current.Resources["SystemControlHighlightListMediumBrush"] ?? new SolidColorBrush(Color.FromArgb(50, 0, 0, 0));
        }

    }
}
