﻿using System.Windows;

namespace iNKORE.UI.WPF.Modern
{
    internal sealed class TappedRoutedEventArgs : RoutedEventArgs
    {
        public TappedRoutedEventArgs()
        {
        }

        //public Point GetPosition(UIElement relativeTo);

        //public PointerDeviceType PointerDeviceType { get; }

        internal int Timestamp { get; set; }
    }
}
