using System;

namespace WinUICommunity_VS_Templates.Options
{
    public class HomeLandingOption
    {
        public HomeLandingOption(bool useHomeLandingPage, bool isMVVMTemplate, string templatePath)
        {
            if (useHomeLandingPage)
            {
                string appFileContent = WizardHelper.ReadAppFileContent(templatePath);

                // We Should Add Config in JsonNavigationViewService
                if (isMVVMTemplate)
                {
                    string SERVICE_KEY = "//SERVICE";
                    string JSONCONFIG_KEY = "//JSONCONFIGMVVM";

                    // Add Service
                    string serviceConfig = SERVICE_KEY + Environment.NewLine + "services.AddTransient<HomeLandingViewModel>();";
                    appFileContent = appFileContent.Replace(SERVICE_KEY, serviceConfig);
                    
                    //Add Config
                    string jsonConfig = JSONCONFIG_KEY + Environment.NewLine + "json.ConfigDefaultPage(typeof(HomeLandingPage));";
                    appFileContent = appFileContent.Replace(JSONCONFIG_KEY, jsonConfig);
                }
                else
                {
                    string JSONCONFIG_KEY = "//JSONCONFIG";

                    string jsonConfig = JSONCONFIG_KEY + Environment.NewLine + "JsonNavigationViewService.ConfigDefaultPage(typeof(HomeLandingPage));";
                    appFileContent = appFileContent.Replace(JSONCONFIG_KEY, jsonConfig);
                }

                WizardHelper.SaveAppFileContent(templatePath, appFileContent);
            }
        }
    }
}
