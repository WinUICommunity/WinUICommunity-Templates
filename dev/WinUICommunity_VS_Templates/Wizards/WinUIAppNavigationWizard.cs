using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;
using System.Collections.Generic;

namespace WinUICommunity_VS_Templates
{
    public class WinUIAppNavigationWizard : IWizard
    {
        WizardImplementation WizardImplementation;

        public void BeforeOpeningFile(ProjectItem projectItem)
        {
        }

        public void ProjectFinishedGenerating(Project project)
        {
        }

        public void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {
        }

        public void RunFinished()
        {
            WizardImplementation.RunFinished(false);
            WizardImplementation.AddSolutionFolder();
        }

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            WizardImplementation = new WizardImplementation();
            WizardImplementation.RunStarted(automationObject, replacementsDictionary, true, false, true);
        }

        public bool ShouldAddProjectItem(string filePath)
        {
            if (!WizardImplementation.ShouldAddProjectItem())
            {
                return false;
            }

            if (!WizardImplementation.UseHomeLandingPage && 
                filePath.Contains("HomeLanding"))
            {
                return false;
            }
            else if (!WizardImplementation.UseSettingsPage && 
                (filePath.Contains("SettingsPage.xaml") || 
                filePath.Contains("BreadcrumbBarUserControl") || 
                filePath.Contains("AboutUsSettingPage") || 
                filePath.Contains("ThemeSettingPage") || 
                filePath.Contains("GeneralSettingPage") || 
                filePath.Contains("AppUpdateSettingPage") ||
                filePath.Contains("backdrop.png") ||
                filePath.Contains("tint.png") ||
                filePath.Contains("color.png") ||
                filePath.Contains("external.png") ||
                filePath.Contains("info.png") ||
                filePath.Contains("settings.png") ||
                filePath.Contains("theme.png") ||
                filePath.Contains("devMode.png") ||
                filePath.Contains("update.png")))
            {
                return false;
            }
            else if (WizardImplementation.UseSettingsPage && 
                !WizardImplementation.UseAboutPage &&
                (filePath.Contains("AboutUsSettingPage") ||
                filePath.Contains("info.png")))
            {
                return false;
            }
            else if (WizardImplementation.UseSettingsPage &&
                !WizardImplementation.UseThemeSettingPage &&
                (filePath.Contains("ThemeSettingPage") ||
                filePath.Contains("backdrop.png") ||
                filePath.Contains("tint.png") ||
                filePath.Contains("color.png") ||
                filePath.Contains("external.png") ||
                filePath.Contains("theme.png")))
            {
                return false;
            }
            else if (WizardImplementation.UseSettingsPage &&
                !WizardImplementation.UseGeneralSettingPage &&
                (filePath.Contains("GeneralSettingPage") ||
                filePath.Contains("settings.png")))
            {
                return false;
            }
            else if (WizardImplementation.UseSettingsPage &&
                !WizardImplementation.UseAppUpdatePage &&
                (filePath.Contains("AppUpdateSettingPage") ||
                filePath.Contains("update.png")))
            {
                return false;
            }
            else if (!WizardImplementation.UseJsonSettings &&
                (filePath.Contains("AppConfig") || 
                filePath.Contains("AppHelper")))
            {
                return false;
            }
            else if (!WizardImplementation.UseHomeLandingPage && 
                !WizardImplementation.UseColorsDic && 
                filePath.Contains("ThemeResources.xaml"))
            {
                return false;
            }
            else if (!WizardImplementation.UseDynamicLocalization &&
                filePath.Contains("Resources") && 
                !filePath.Contains("ThemeResources"))
            {
                return false;
            }
            else if (!WizardImplementation.UseDeveloperModeSetting && filePath.Contains("devMode.png"))
            {
                return false;
            }
            else if (!WizardImplementation.UseDebugLogger && 
                !WizardImplementation.UseFileLogger && 
                filePath.Contains("LoggerSetup"))
            {
                return false;
            }
            else if (!WizardImplementation.UseConvertersDic && filePath.Contains("Converters.xaml"))
            {
                return false;
            }
            else if (!WizardImplementation.UseFontsDic && filePath.Contains("Fonts.xaml"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
