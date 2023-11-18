using System;

namespace iNKORE.UI.WPF.Modern
{
    public sealed class SplitViewPaneClosingEventArgs : EventArgs
    {
        internal SplitViewPaneClosingEventArgs()
        {
        }

        public bool Cancel { get; set; }
    }
}