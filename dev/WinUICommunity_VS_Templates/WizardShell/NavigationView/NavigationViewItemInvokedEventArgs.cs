﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;

namespace iNKORE.UI.WPF.Modern
{
    public sealed class NavigationViewItemInvokedEventArgs : EventArgs
    {
        public NavigationViewItemInvokedEventArgs()
        {
        }

        public object InvokedItem { get; internal set; }
        public bool IsSettingsInvoked { get; internal set; }

        public NavigationViewItemBase InvokedItemContainer { get; internal set; }
        public NavigationTransitionInfo RecommendedNavigationTransitionInfo { get; internal set; }
    }
}
