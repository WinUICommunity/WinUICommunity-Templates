using System;

namespace WinUICommunity.Shell
{
    public class InfoBarClosedEventArgs : EventArgs
    {
        public InfoBarCloseReason Reason { get; internal set; }
    }
}
