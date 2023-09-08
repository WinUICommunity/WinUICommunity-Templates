using System.Collections.Generic;

using EnvDTE;

using Microsoft.VisualStudio.TemplateWizard;

namespace WinUICommunity_VS_Templates
{
    public class WinUIAppMVVMNavigationWizard : IWizard
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
            WizardImplementation.RunFinished(true);
            WizardImplementation.AddSolutionFolder();
        }

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            WizardImplementation = new WizardImplementation();
            WizardImplementation.RunStarted(automationObject, replacementsDictionary, true);
        }

        public bool ShouldAddProjectItem(string filePath)
        {
            if (!WizardImplementation.ShouldAddProjectItem())
            {
                return false;
            }

            if (!WizardImplementation.UseHomeLandingPage &&
                (filePath.Contains("HomeLanding") || 
                filePath.Contains("HomeLandingViewModel")))
            {
                return false;
            }
            else if (!WizardImplementation.UseSettingsPage &&
                (filePath.Contains("SettingsPage.xaml") ||
                filePath.Contains("SettingsViewModel") ||
                filePath.Contains("BreadCrumbBarViewModel") ||
                filePath.Contains("BreadcrumbBarUserControl") ||
                filePath.Contains("AboutUsSettingPage") ||
                filePath.Contains("ThemeSettingPage") ||
                filePath.Contains("AboutUsSettingViewModel") ||
                filePath.Contains("ThemeSettingViewModel") ||
                filePath.Contains("GeneralSettingPage") ||
                filePath.Contains("GeneralSettingViewModel") ||
                filePath.Contains("AppUpdateSettingPage") ||
                filePath.Contains("AppUpdateSettingViewModel") ||
                filePath.Contains("backdrop.png") ||
                filePath.Contains("color.png") ||
                filePath.Contains("external.png") ||
                filePath.Contains("info.png") ||
                filePath.Contains("settings.png") ||
                filePath.Contains("theme.png") ||
                filePath.Contains("update.png")))
            {
                return false;
            }
            else if (WizardImplementation.UseSettingsPage &&
                !WizardImplementation.UseAboutPage &&
                (filePath.Contains("AboutUsSettingPage") ||
                filePath.Contains("AboutUsSettingViewModel") ||
                filePath.Contains("info.png")))
            {
                return false;
            }
            else if (WizardImplementation.UseSettingsPage &&
                !WizardImplementation.UseThemeSettingPage &&
                (filePath.Contains("ThemeSettingPage") ||
                filePath.Contains("ThemeSettingViewModel") ||
                filePath.Contains("backdrop.png") ||
                filePath.Contains("color.png") ||
                filePath.Contains("external.png") ||
                filePath.Contains("theme.png")))
            {
                return false;
            }
            else if (WizardImplementation.UseSettingsPage &&
                !WizardImplementation.UseGeneralSettingPage &&
                (filePath.Contains("GeneralSettingPage") ||
                filePath.Contains("GeneralSettingViewModel") ||
                filePath.Contains("settings.png")))
            {
                return false;
            }
            else if (WizardImplementation.UseSettingsPage &&
                !WizardImplementation.UseAppUpdatePage &&
                (filePath.Contains("AppUpdateSettingPage") ||
                filePath.Contains("AppUpdateSettingViewModel") ||
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
            else if (!WizardImplementation.UseDynamicLocalization &&
                filePath.Contains("Resources"))
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
