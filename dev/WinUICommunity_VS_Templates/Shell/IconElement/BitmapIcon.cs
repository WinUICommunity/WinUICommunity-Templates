﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WinUICommunity.Shell
{
    /// <summary>
    /// Represents an icon that uses a bitmap as its content.
    /// </summary>
    public class BitmapIcon : IconElement
    {
        static BitmapIcon()
        {
            ForegroundProperty.OverrideMetadata(typeof(BitmapIcon), new FrameworkPropertyMetadata(OnForegroundChanged));
        }

        /// <summary>
        /// Initializes a new instance of the BitmapIcon class.
        /// </summary>
        public BitmapIcon()
        {
        }

        #region UriSource

        /// <summary>
        /// Identifies the UriSource dependency property.
        /// </summary>
        public static readonly DependencyProperty UriSourceProperty =
            BitmapImage.UriSourceProperty.AddOwner(
                typeof(BitmapIcon),
                new FrameworkPropertyMetadata(OnUriSourceChanged));

        /// <summary>
        /// Gets or sets the Uniform Resource Identifier (URI) of the bitmap to use as the
        /// icon content.
        /// </summary>
        /// <value>The Uri of the bitmap to use as the icon content. The default is <see langword="null"/>.</value>
        public Uri UriSource
        {
            get => (Uri)GetValue(UriSourceProperty);
            set => SetValue(UriSourceProperty, value);
        }

        private static void OnUriSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BitmapIcon)d).ApplyUriSource();
        }

        #endregion

        #region ShowAsMonochrome

        /// <summary>
        /// Identifies the ShowAsMonochrome dependency property.
        /// </summary>
        public static readonly DependencyProperty ShowAsMonochromeProperty =
            DependencyProperty.Register(
                nameof(ShowAsMonochrome),
                typeof(bool),
                typeof(BitmapIcon),
                new PropertyMetadata(true, OnShowAsMonochromeChanged));

        /// <summary>
        /// Gets or sets a value that indicates whether the bitmap is shown in a single color.
        /// </summary>
        /// <value><see langword="true"/> to show the bitmap in a single color; <see langword="false"/> to show the bitmap in full color. The default is <see langword="true"/>.</value>
        public bool ShowAsMonochrome
        {
            get => (bool)GetValue(ShowAsMonochromeProperty);
            set => SetValue(ShowAsMonochromeProperty, value);
        }

        private static void OnShowAsMonochromeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BitmapIcon)d).ApplyShowAsMonochrome();
        }

        #endregion

        private protected override void InitializeChildren()
        {
            _image = new Image
            {
                Visibility = Visibility.Hidden
            };

            _opacityMask = new ImageBrush();
            _foreground = new Rectangle
            {
                OpacityMask = _opacityMask
            };

            ApplyForeground();
            ApplyUriSource();

            Children.Add(_image);

            ApplyShowAsMonochrome();
        }

        private protected override void OnShouldInheritForegroundFromVisualParentChanged()
        {
            ApplyForeground();
        }

        private protected override void OnVisualParentForegroundPropertyChanged(DependencyPropertyChangedEventArgs args)
        {
            if (ShouldInheritForegroundFromVisualParent)
            {
                ApplyForeground();
            }
        }

        private static void OnForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BitmapIcon)d).ApplyForeground();
        }

        private void ApplyForeground()
        {
            if (_foreground != null)
            {
                _foreground.Fill = ShouldInheritForegroundFromVisualParent ? VisualParentForeground : Foreground;
            }
        }

        private void ApplyUriSource()
        {
            if (_image != null && _opacityMask != null)
            {
                var uriSource = UriSource;
                if (uriSource != null)
                {
                    var imageSource = new BitmapImage(uriSource);
                    _image.Source = imageSource;
                    _opacityMask.ImageSource = imageSource;
                }
                else
                {
                    _image.ClearValue(Image.SourceProperty);
                    _opacityMask.ClearValue(ImageBrush.ImageSourceProperty);
                }
            }
        }

        private void ApplyShowAsMonochrome()
        {
            bool showAsMonochrome = ShowAsMonochrome;

            if (_image != null)
            {
                _image.Visibility = showAsMonochrome ? Visibility.Hidden : Visibility.Visible;
            }

            if (_foreground != null)
            {
                if (showAsMonochrome)
                {
                    if (!Children.Contains(_foreground))
                    {
                        Children.Add(_foreground);
                    }
                }
                else
                {
                    Children.Remove(_foreground);
                }
            }
        }

        private Image _image;
        private Rectangle _foreground;
        private ImageBrush _opacityMask;
    }
}
