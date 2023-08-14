﻿using System.Windows;

namespace WinUICommunity.Shell
{
    /// <summary>
    /// Represents an animation for a back arrow that can be used as an animated icon source.
    /// </summary>
    public class AnimatedBackVisualSource : AnimatedVisualSource
    {
        static AnimatedBackVisualSource()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AnimatedBackVisualSource), new FrameworkPropertyMetadata(typeof(AnimatedBackVisualSource)));
        }

        public AnimatedBackVisualSource()
        {
        }

        protected override void OnStatePropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            var state = (string)e.NewValue;
            if (state == "Normal" || state == "Pressed" || state == "PointerOver")
            {
                VisualStateManager.GoToState(this, (string)e.NewValue, true);
            }
            else
            {
                VisualStateManager.GoToState(this, "Normal", true);
            }
        }
    }
}
