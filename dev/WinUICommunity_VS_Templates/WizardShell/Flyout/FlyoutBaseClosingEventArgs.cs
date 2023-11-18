using System;

namespace iNKORE.UI.WPF.Modern
{
    internal sealed class FlyoutBaseClosingEventArgs : EventArgs
    {
        internal FlyoutBaseClosingEventArgs()
        {
        }

        public bool Cancel
        {
            get => false;
            set => throw new NotImplementedException();
        }
    }
}
