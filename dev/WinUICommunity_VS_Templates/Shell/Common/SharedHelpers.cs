// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace WinUICommunity.Shell
{
    internal static class SharedHelpers
    {
        public static IconElement MakeIconElementFrom(IconSource iconSource)
        {
            if (iconSource is FontIconSource fontIconSource)
            {
                FontIcon fontIcon = new FontIcon();

                fontIcon.Glyph = fontIconSource.Glyph;
                fontIcon.FontSize = fontIconSource.FontSize;
                var newForeground = fontIconSource.Foreground;
                if (newForeground != null)
                {
                    fontIcon.Foreground = newForeground;
                }

                if (fontIconSource.FontFamily != null)
                {
                    fontIcon.FontFamily = fontIconSource.FontFamily;
                }

                fontIcon.FontWeight = fontIconSource.FontWeight;
                fontIcon.FontStyle = fontIconSource.FontStyle;
                //fontIcon.IsTextScaleFactorEnabled = fontIconSource.IsTextScaleFactorEnabled;
                //fontIcon.MirroredWhenRightToLeft = fontIconSource.MirroredWhenRightToLeft;

                return fontIcon;
            }
            else if (iconSource is SymbolIconSource symbolIconSource)
            {
                SymbolIcon symbolIcon = new SymbolIcon();
                symbolIcon.Symbol = symbolIconSource.Symbol;
                var newForeground = symbolIconSource.Foreground;
                if (newForeground != null)
                {
                    symbolIcon.Foreground = newForeground;
                }
                return symbolIcon;
            }
            else if (iconSource is BitmapIconSource bitmapIconSource)
            {
                BitmapIcon bitmapIcon = new BitmapIcon();

                if (bitmapIconSource.UriSource != null)
                {
                    bitmapIcon.UriSource = bitmapIconSource.UriSource;
                }

                bitmapIcon.ShowAsMonochrome = bitmapIconSource.ShowAsMonochrome;
                var newForeground = bitmapIconSource.Foreground;
                if (newForeground != null)
                {
                    bitmapIcon.Foreground = newForeground;
                }
                return bitmapIcon;
            }
            else if (iconSource is ImageIconSource imageIconSource)
            {
                ImageIcon imageIcon = new ImageIcon();
                var imageSource = imageIconSource.ImageSource;
                if (imageSource != null)
                {
                    imageIcon.Source = imageSource;
                }
                var newForeground = imageIconSource.Foreground;
                if (newForeground != null)
                {
                    imageIcon.Foreground = newForeground;
                }
                return imageIcon;
            }
            else if (iconSource is PathIconSource pathIconSource)
            {
                PathIcon pathIcon = new PathIcon();

                if (pathIconSource.Data != null)
                {
                    pathIcon.Data = pathIconSource.Data;
                }
                var newForeground = pathIconSource.Foreground;
                if (newForeground != null)
                {
                    pathIcon.Foreground = newForeground;
                }
                return pathIcon;
            }
            return null;
        }
    }
}
