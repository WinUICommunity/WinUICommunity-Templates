using System.Windows;

namespace WinUICommunity_VS_Templates
{
    public partial class Wizard
    {
        public static bool useLocalization;
        public static bool useJsonSettings;
        public Wizard()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            useJsonSettings = tgSettings.IsOn;
            useLocalization = tgLocalization.IsOn;
            this.Close();
        }
    }
}
