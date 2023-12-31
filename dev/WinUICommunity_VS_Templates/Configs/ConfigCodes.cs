﻿using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.Text;

namespace WinUICommunity_VS_Templates
{
    public class ConfigCodes
    {
        public Dictionary<string, string> ConfigJsonDic = new();
        public Dictionary<string, string> ServiceDic = new();
        public Dictionary<string, string> SettingsPageOptionsDic = new();
        public Dictionary<string, string> GeneralSettingsPageOptionsDic = new();

        bool UseAboutPage;
        bool UseAppUpdatePage;
        bool UseGeneralSettingPage;
        bool UseHomeLandingPage;
        bool UseSettingsPage;
        bool UseThemeSettingPage;
        bool UseDeveloperModeSetting;
        bool UseJsonSetting;
        public ConfigCodes(bool UseAboutPage, bool UseAppUpdatePage, bool UseGeneralSettingPage, bool UseHomeLandingPage, bool UseSettingsPage, bool UseThemeSettingPage, bool UseDeveloperModeSetting, bool UseJsonSetting)
        {
            this.UseAboutPage = UseAboutPage;
            this.UseAppUpdatePage = UseAppUpdatePage;
            this.UseGeneralSettingPage = UseGeneralSettingPage;
            this.UseHomeLandingPage = UseHomeLandingPage;
            this.UseSettingsPage = UseSettingsPage;
            this.UseThemeSettingPage = UseThemeSettingPage;
            this.UseDeveloperModeSetting = UseDeveloperModeSetting;
            this.UseJsonSetting = UseJsonSetting;
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

        public string GetGeneralSettingsPageOptions()
        {
            if (GeneralSettingsPageOptionsDic.Count == 0)
            {
                return "";
            }

            StringBuilder outputBuilder = new StringBuilder();
            int index = 0;
            foreach (var item in GeneralSettingsPageOptionsDic.Values)
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

        public void ConfigAllMVVM()
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

        public void ConfigAll()
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

        public void ConfigGeneral()
        {
            if (UseSettingsPage && UseGeneralSettingPage && UseDeveloperModeSetting && !UseJsonSetting)
            {
                GeneralSettingsPageOptionsDic.Add(nameof(UseDeveloperModeSetting), Environment.NewLine + SettingsCardOptions.DeveloperModeSettingCode);
            }
            else if (UseSettingsPage && UseGeneralSettingPage && UseDeveloperModeSetting && UseJsonSetting)
            {
                GeneralSettingsPageOptionsDic.Add(nameof(UseDeveloperModeSetting), Environment.NewLine + SettingsCardOptions.DeveloperModeSettingCode2);
            }
        }
    }
}
