using System.Windows;

namespace WinUICommunity.Shell
{
    public class InfoBarTemplateSettings : DependencyObject
    {
        internal InfoBarTemplateSettings()
        {
        }

        #region IconElement

        private static readonly DependencyPropertyKey IconElementPropertyKey =
            DependencyProperty.RegisterReadOnly(
                nameof(IconElement),
                typeof(IconElement),
                typeof(InfoBarTemplateSettings),
                null);

        public static readonly DependencyProperty IconElementProperty = IconElementPropertyKey.DependencyProperty;

        public IconElement IconElement
        {
            get => (IconElement)GetValue(IconElementProperty);
            internal set => SetValue(IconElementPropertyKey, value);
        }

        #endregion
    }
}
