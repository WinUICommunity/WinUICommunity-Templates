using System;

namespace WinUICommunity_VS_Templates.Options
{
    public class SettingsPageOption
    {
        public SettingsPageOption(bool useSettingsPage, bool isMVVMTemplate, string templatePath)
        {
            if (useSettingsPage)
            {
                string appFileContent = WizardHelper.ReadAppFileContent(templatePath);

                if (isMVVMTemplate)
                {
                    string SERVICE_KEY = "//SERVICE";
                    string JSONCONFIG_KEY = "//JSONCONFIGMVVM";

                    //Add Service 
                    string serviceConfig = SERVICE_KEY + Environment.NewLine + "services.AddTransient<SettingsViewModel>();";
                    appFileContent = appFileContent.Replace(SERVICE_KEY, serviceConfig);

                    //Add Service
                    serviceConfig = SERVICE_KEY + Environment.NewLine + "services.AddTransient<BreadCrumbBarViewModel>();";
                    appFileContent = appFileContent.Replace(SERVICE_KEY, serviceConfig);

                    //Add Config
                    string jsonConfig = JSONCONFIG_KEY + Environment.NewLine + "json.ConfigSettingsPage(typeof(SettingsPage));";
                    appFileContent = appFileContent.Replace(JSONCONFIG_KEY, jsonConfig);
                }
                else
                {
                    string JSONCONFIG_KEY = "//JSONCONFIG";

                    string jsonConfig = JSONCONFIG_KEY + Environment.NewLine + "JsonNavigationViewService.ConfigSettingsPage(typeof(SettingsPage));";
                    appFileContent = appFileContent.Replace(JSONCONFIG_KEY, jsonConfig);
                }

                WizardHelper.SaveAppFileContent(templatePath, appFileContent);
            }
        }
    }
}
