﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Windows;

namespace WinUICommunity.Shell
{
    internal interface IControlProtected
    {
        DependencyObject GetTemplateChild(string childName);
    }
}
