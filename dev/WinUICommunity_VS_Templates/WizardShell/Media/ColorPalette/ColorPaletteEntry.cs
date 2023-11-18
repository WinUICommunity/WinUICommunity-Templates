﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Windows.Media;

namespace iNKORE.UI.WPF.Modern
{
    // These classes are not intended to be viewmodels.
    // They deal with the data about an editable palette and are passed to special purpose controls for editing
    internal class ColorPaletteEntry : IColorPaletteEntry
    {
        public ColorPaletteEntry(Color color)
        {
            ActiveColor = color;
        }

        public Color ActiveColor { get; }
    }
}

