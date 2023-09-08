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
            int index = 0;
            foreach (var item in SettingsPageOptionsDic.Values)
            {
                if (index == 0)
                {
                    outputBuilder.AppendLine(item);
                }
                else
                {
                    outputBuilder.AppendLine($"            {item}");
                }
                index++;
            }

            return outputBuilder.ToString();
        }

        public void ConfigMVVM()
        {
            if (UseGeneralSettingPage)
            {
                SettingsPageOptionsDic.Add(nameof(UseGeneralSettingPage), SettingsCardOptions.GeneralSettingMVVMCode);

                ServiceDic.Add(nameof(UseGeneralSettingPage), "services.AddTransient<GeneralSettingViewModel>();");
            }

            if (UseThemeSettingPage)
            {
                SettingsPageOptionsDic.Add(nameof(UseThemeSettingPage), SettingsCardOptions.ThemeSettingMVVMCode);
                ServiceDic.Add(nameof(UseThemeSettingPage), "services.AddTransient<ThemeSettingViewModel>();");
            }

            if (UseAppUpdatePage)
            {
                SettingsPageOptionsDic.Add(nameof(UseAppUpdatePage), SettingsCardOptions.AppUpdateSettingMVVMCode);

                ServiceDic.Add(nameof(UseAppUpdatePage), "services.AddTransient<AppUpdateSettingViewModel>();");
            }

            if (UseAboutPage)
            {
                SettingsPageOptionsDic.Add(nameof(UseAboutPage), SettingsCardOptions.AboutSettingMVVMCode);
                ServiceDic.Add(nameof(UseAboutPage), "services.AddTransient<AboutUsSettingViewModel>();");
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

            if (SettingsPageOptionsDic.Count == 0)
            {
                SettingsPageOptionsDic.Add("comment", SettingsCardOptions.SettingsCardMVVMCommentCode);
            }
        }

        public void Config()
        {
            if (UseGeneralSettingPage)
            {
                SettingsPageOptionsDic.Add(nameof(UseGeneralSettingPage), SettingsCardOptions.GeneralSettingCode);
            }

            if (UseThemeSettingPage)
            {
                SettingsPageOptionsDic.Add(nameof(UseThemeSettingPage), SettingsCardOptions.ThemeSettingCode);
            }

            if (UseAppUpdatePage)
            {
                SettingsPageOptionsDic.Add(nameof(UseAppUpdatePage), SettingsCardOptions.AppUpdateSettingCode);
            }

            if (UseAboutPage)
            {
                SettingsPageOptionsDic.Add(nameof(UseAboutPage), SettingsCardOptions.AboutSettingCode);
            }

            if (UseHomeLandingPage)
            {
                ConfigJsonDic.Add(nameof(UseHomeLandingPage), "JsonNavigationViewService.ConfigDefaultPage(typeof(HomeLandingPage));");
            }

            if (UseSettingsPage)
            {
                ConfigJsonDic.Add(nameof(UseSettingsPage), "JsonNavigationViewService.ConfigSettingsPage(typeof(SettingsPage));");
            }

            if (SettingsPageOptionsDic.Count == 0)
            {
                SettingsPageOptionsDic.Add("comment", SettingsCardOptions.SettingsCardCommentCode);
            }
        }
    }
}
