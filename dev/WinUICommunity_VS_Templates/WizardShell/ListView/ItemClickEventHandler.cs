﻿using System.Windows;

namespace iNKORE.UI.WPF.Modern
{
    public delegate void ItemClickEventHandler(object sender, ItemClickEventArgs e);

    public sealed class ItemClickEventArgs : RoutedEventArgs
    {
        public ItemClickEventArgs()
        {
        }

        public object ClickedItem { get; internal set; }
    }
}
