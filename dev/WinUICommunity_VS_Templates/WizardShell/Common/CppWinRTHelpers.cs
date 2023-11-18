// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using iNKORE.UI.WPF.Modern;
using System.Windows;

static class CppWinRTHelpers
{
    public static WinRTReturn GetTemplateChildT<WinRTReturn>(string childName, IControlProtected controlProtected) where WinRTReturn : DependencyObject
    {
        DependencyObject childAsDO = controlProtected.GetTemplateChild(childName);

        if (childAsDO != null)
        {
            return childAsDO as WinRTReturn;
        }
        return null;
    }
}
