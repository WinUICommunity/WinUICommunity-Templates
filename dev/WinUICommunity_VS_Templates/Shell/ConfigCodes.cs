using System.Collections.Generic;
using System.Text;

namespace WinUICommunity_VS_Templates.Shell
{
    public class ConfigCodes
    {
        public Dictionary<string, string> ConfigJsonDic = new();
        public Dictionary<string, string> ServiceDic = new();
        public Dictionary<string, string> SettingsPageOptionsDic = new();

        bool UseAboutPage;
        bool UseAppUpdatePage;
        bool UseGeneralSettingPage;
        bool UseHomeLandingPage;
        bool UseSettingsPage;
        bool UseThemeSettingPage;

        public ConfigCodes(bool UseAboutPage, bool UseAppUpdatePage, bool UseGeneralSettingPage, bool UseHomeLandingPage, bool UseSettingsPage, bool UseThemeSettingPage)
        {
            this.UseAboutPage = UseAboutPage;
            this.UseAppUpdatePage = UseAppUpdatePage;
            this.UseGeneralSettingPage = UseGeneralSettingPage;
            this.UseHomeLandingPage = UseHomeLandingPage;
            this.UseSettingsPage = UseSettingsPage;
            this.UseThemeSettingPage = UseThemeSettingPage;
        }

        public string GetConfigJson()
        {
            StringBuilder outputBuilder = new StringBuilder();
            foreach (var item in ConfigJsonDic.Values)
            {
                outputBuilder.AppendLine(item);
            }

            return outputBuilder.ToString();
        }

        public string GetServices()
        {
            StringBuilder outputBuilder = new StringBuilder();
            foreach (var item in ServiceDic.Values)
            {
                outputBuilder.AppendLine(item);
            }

            return outputBuilder.ToString();
        }

        public string GetSettingsPageOptions()
        {
            StringBuilder outputBuilder = new StringBuilder();
            foreach (var item in SettingsPageOptionsDic.Values)
            {
                outputBuilder.AppendLine(item);
            }

            return outputBuilder.ToString();
        }

        public void ConfigMVVM()
        {
            if (UseAboutPage)
            {
                SettingsPageOptionsDic.Add(nameof(UseAboutPage), SettingsCardOptions.AboutSettingMVVMCode);
                ServiceDic.Add(nameof(UseAboutPage), "services.AddTransient<AboutUsSettingViewModel>();");
            }

            if (UseAppUpdatePage)
            {
                SettingsPageOptionsDic.Add(nameof(UseAppUpdatePage), SettingsCardOptions.AppUpdateSettingMVVMCode);

                ServiceDic.Add(nameof(UseAppUpdatePage), "services.AddTransient<AppUpdateSettingViewModel>();");
            }

            if (UseGeneralSettingPage)
            {
                SettingsPageOptionsDic.Add(nameof(UseGeneralSettingPage), SettingsCardOptions.GeneralSettingMVVMCode);

                ServiceDic.Add(nameof(UseGeneralSettingPage), "services.AddTransient<GeneralSettingViewModel>();");
            }

            if (UseHomeLandingPage)
            {
                ConfigJsonDic.Add(nameof(UseHomeLandingPage), "json.ConfigDefaultPage(typeof(HomeLandingPage));");
                ServiceDic.Add(nameof(UseHomeLandingPage), "services.AddTransient<HomeLandingViewModel>();");
            }

            if (UseSettingsPage)
            {
                ConfigJsonDic.Add(nameof(UseSettingsPage), "json.ConfigSettingsPage(typeof(SettingsPage));");
                ServiceDic.Add(nameof(UseSettingsPage), "services.AddTransient<SettingsViewModel>();");
                ServiceDic.Add(nameof(UseSettingsPage) + "Bread", "services.AddTransient<BreadCrumbBarViewModel>();");
            }

            if (UseThemeSettingPage)
            {
                SettingsPageOptionsDic.Add(nameof(UseThemeSettingPage), SettingsCardOptions.ThemeSettingMVVMCode);
                ServiceDic.Add(nameof(UseThemeSettingPage), "services.AddTransient<ThemeSettingViewModel>();");
            }

            if (SettingsPageOptionsDic.Count == 0)
            {
                SettingsPageOptionsDic.Add("comment", SettingsCardOptions.SettingsCardMVVMCommentCode);
            }
        }

        public void Config()
        {
            if (UseAboutPage)
            {
                SettingsPageOptionsDic.Add(nameof(UseAboutPage), SettingsCardOptions.AboutSettingCode);
            }

            if (UseAppUpdatePage)
            {
                SettingsPageOptionsDic.Add(nameof(UseAppUpdatePage), SettingsCardOptions.AppUpdateSettingCode);
            }

            if (UseGeneralSettingPage)
            {
                SettingsPageOptionsDic.Add(nameof(UseGeneralSettingPage), SettingsCardOptions.GeneralSettingCode);
            }

            if (UseHomeLandingPage)
            {
                ConfigJsonDic.Add(nameof(UseHomeLandingPage), "JsonNavigationViewService.ConfigDefaultPage(typeof(HomeLandingPage));");
            }

            if (UseSettingsPage)
            {
                ConfigJsonDic.Add(nameof(UseSettingsPage), "JsonNavigationViewService.ConfigSettingsPage(typeof(SettingsPage));");
            }

            if (UseThemeSettingPage)
            {
                SettingsPageOptionsDic.Add(nameof(UseThemeSettingPage), SettingsCardOptions.ThemeSettingCode);
            }

            if (SettingsPageOptionsDic.Count == 0)
            {
                SettingsPageOptionsDic.Add("comment", SettingsCardOptions.SettingsCardCommentCode);
            }
        }
    }
}
