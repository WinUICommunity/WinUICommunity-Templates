// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using iNKORE.UI.WPF.Modern;
using System.Windows;

namespace MS.Internal
{
    internal static class PointUtil
    {
        internal static Rect ToRect(this PInvoke.RECT rc)
        {
            Rect rect = new Rect();
 
            rect.X      = rc.Left;
            rect.Y      = rc.Top;
            rect.Width  = rc.Right  - rc.Left;
            rect.Height = rc.Bottom - rc.Top;
 
            return rect;
        }
    }
}
