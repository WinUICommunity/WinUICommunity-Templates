using System.Windows;
using System.Windows.Controls;
using WinUICommunity_VS_Templates.WizardUI;

namespace WinUICommunity_VS_Templates
{
    public partial class ResourcePage : Page
    {
        public ResourcePage()
        {
            InitializeComponent();
        }

        private void tgDicColor_Toggled(object sender, RoutedEventArgs e)
        {
            WizardConfig.AddColorsDic = tgDicColor.IsOn;
        }

        private void tgDicStyle_Toggled(object sender, RoutedEventArgs e)
        {
            WizardConfig.AddStylesDic = tgDicStyle.IsOn;
        }

        private void tgDicFont_Toggled(object sender, RoutedEventArgs e)
        {
            WizardConfig.AddFontsDic = tgDicFont.IsOn;
        }

        private void tgDicConverter_Toggled(object sender, RoutedEventArgs e)
        {
            WizardConfig.AddConvertersDic = tgDicConverter.IsOn;
        }
    }
}
