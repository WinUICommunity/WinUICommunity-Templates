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
            WizardImplementation.RunStarted(automationObject, replacementsDictionary, runKind, customParams);
        }

        public bool ShouldAddProjectItem(string filePath)
        {
            if (!WizardImplementation.AddHomeLandingPageOption && (filePath.Contains("HomeLanding") || filePath.Contains("HomeLandingViewModel")))
            {
                return false;
            }
            else if (!WizardImplementation.AddSettingsPageOption && (filePath.Contains("SettingsPage.xaml") || filePath.Contains("SettingsViewModel") || filePath.Contains("BreadCrumbBarViewModel") || filePath.Contains("BreadcrumbBarUserControl") || filePath.Contains("AboutUsSettingPage") || filePath.Contains("ThemeSettingPage") || filePath.Contains("AboutUsSettingViewModel") || filePath.Contains("ThemeSettingViewModel") || filePath.Contains("GeneralSettingPage") || filePath.Contains("GeneralSettingViewModel")))
            {
                return false;
            }
            else if (WizardImplementation.AddSettingsPageOption && !WizardImplementation.AddAboutPageOption && (filePath.Contains("AboutUsSettingPage") || filePath.Contains("AboutUsSettingViewModel")))
            {
                return false;
            }
            else if (WizardImplementation.AddSettingsPageOption && !WizardImplementation.AddThemeSettingPageOption && (filePath.Contains("ThemeSettingPage") || filePath.Contains("ThemeSettingViewModel")))
            {
                return false;
            }
            else if (WizardImplementation.AddSettingsPageOption && !WizardImplementation.AddGeneralSettingPageOption && (filePath.Contains("GeneralSettingPage") || filePath.Contains("GeneralSettingViewModel")))
            {
                return false;
            }
            else if (!WizardImplementation.AddJsonSettingsOption && (filePath.Contains("AppConfig") || filePath.Contains("AppHelper")))
            {
                return false;
            }
            else if (!WizardImplementation.AddDynamicLocalizationOption && filePath.Contains("Resources"))
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
