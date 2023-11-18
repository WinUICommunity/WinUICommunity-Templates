using System;

namespace iNKORE.UI.WPF.Modern
{
    internal static class PackUriHelper
    {
        public static Uri GetAbsoluteUri(string path)
        {
            return new Uri($"pack://application:,,,/WinUICommunity_VS_Templates;component/WizardShell/{path}");
        }
    }
}
