﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Windows;

namespace iNKORE.UI.WPF.Modern
{
    internal interface IFlowLayoutAlgorithmDelegates
    {
        Size Algorithm_GetMeasureSize(int index, Size availableSize, VirtualizingLayoutContext context);
        Size Algorithm_GetProvisionalArrangeSize(int index, Size measureSize, Size desiredSize, VirtualizingLayoutContext context);
        bool Algorithm_ShouldBreakLine(int index, double remainingSpace);
        FlowLayoutAnchorInfo Algorithm_GetAnchorForRealizationRect(Size availableSize, VirtualizingLayoutContext context);
        FlowLayoutAnchorInfo Algorithm_GetAnchorForTargetElement(int targetIndex, Size availableSize, VirtualizingLayoutContext context);
        Rect Algorithm_GetExtent(Size availableSize,
            VirtualizingLayoutContext context,
            UIElement firstRealized,
            int firstRealizedItemIndex,
            Rect firstRealizedLayoutBounds,
            UIElement lastRealized,
            int lastRealizedItemIndex,
            Rect lastRealizedLayoutBounds);
        void Algorithm_OnElementMeasured(
            UIElement element,
            int index,
            Size availableSize,
            Size measureSize,
            Size desiredSize,
            Size provisionalArrangeSize,
            VirtualizingLayoutContext context);
        void Algorithm_OnLineArranged(
            int startIndex,
            int countInLine,
            double lineSize,
            VirtualizingLayoutContext context);
    }
}
