using System;

namespace WinUICommunity.Shell
{
    public class InfoBarClosingEventArgs : EventArgs
    {
        public InfoBarCloseReason Reason { get; internal set; }

        public bool Cancel{ get; set; }
    }
}
