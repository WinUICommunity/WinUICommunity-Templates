﻿using System.ComponentModel;

using System.Windows;

namespace iNKORE.UI.WPF.Modern
{
    public class CardControl : System.Windows.Controls.Primitives.ButtonBase
    {
        /// <summary>
        /// Property for <see cref="Header"/>.
        /// </summary>
        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
            nameof(Header),
            typeof(object),
            typeof(CardControl),
            new PropertyMetadata(null)
        );

        /// <summary>
        /// Property for <see cref="Icon"/>.
        /// </summary>
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
            nameof(Icon),
            typeof(IconElement),
            typeof(CardControl),
            new PropertyMetadata(null)
        );

        /// <summary>
        /// Property for <see cref="CornerRadius"/>
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
            nameof(CornerRadius),
            typeof(CornerRadius),
            typeof(CardControl),
            new PropertyMetadata(new CornerRadius(0))
        );

        /// <summary>
        /// Header is the data used to for the header of each item in the control.
        /// </summary>
        [Bindable(true)]
        public object Header
        {
            get => GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        /// <summary>
        /// Gets or sets displayed <see cref="IconElement"/>.
        /// </summary>
        [Bindable(true), Category("Appearance")]
        public IconElement Icon
        {
            get => (IconElement)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        /// <summary>
        /// Gets or sets the corner radius of the control.
        /// </summary>
        [Bindable(true), Category("Appearance")]
        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }
    }
}
