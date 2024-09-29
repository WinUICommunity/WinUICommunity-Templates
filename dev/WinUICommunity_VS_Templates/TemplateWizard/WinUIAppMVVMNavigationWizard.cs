using System.Collections.Generic;

using EnvDTE;

using Microsoft.VisualStudio.TemplateWizard;
using WinUICommunity_VS_Templates.WizardUI;

namespace WinUICommunity_VS_Templates
{
    public class WinUIAppMVVMNavigationWizard : IWizard
    {
        SharedWizard WizardImplementation;

        public void BeforeOpeningFile(ProjectItem projectItem)
        {
        }

        public void ProjectFinishedGenerating(Project project)
        {
            WizardImplementation.ProjectFinishedGenerating(project);
        }

        public void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {
        }

        public void RunFinished()
        {
            WizardImplementation.RunFinished();
        }

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            WizardImplementation = new SharedWizard();
            WizardImplementation.RunStarted(automationObject, replacementsDictionary, "WinUIApp-MVVM-NavigationView", true, true, true);
        }

        public bool ShouldAddProjectItem(string filePath)
        {
            if (!WizardImplementation.ShouldAddProjectItem())
            {
                return false;
            }

            if (!WizardConfig.UseHomeLandingPage &&
                (filePath.Contains("HomeLanding") || 
                filePath.Contains("HomeLandingViewModel")))
            {
                return false;
            }
            else if (!WizardConfig.UseSettingsPage &&
                (filePath.Contains("SettingsPage.xaml") ||
                filePath.Contains("SettingsViewModel") ||
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
                filePath.Contains("tint.png") ||
                filePath.Contains("external.png") ||
                filePath.Contains("info.png") ||
                filePath.Contains("settings.png") ||
                filePath.Contains("theme.png") ||
                filePath.Contains("devMode.png") ||
                filePath.Contains("update.png")))
            {
                return false;
            }
            else if (WizardConfig.UseSettingsPage &&
                !WizardConfig.UseAboutPage &&
                (filePath.Contains("AboutUsSettingPage") ||
                filePath.Contains("AboutUsSettingViewModel") ||
                filePath.Contains("info.png")))
            {
                return false;
            }
            else if (WizardConfig.UseSettingsPage &&
                !WizardConfig.UseThemeSettingPage &&
                (filePath.Contains("ThemeSettingPage") ||
                filePath.Contains("ThemeSettingViewModel") ||
                filePath.Contains("backdrop.png") ||
                filePath.Contains("color.png") ||
                filePath.Contains("tint.png") ||
                filePath.Contains("external.png") ||
                filePath.Contains("theme.png")))
            {
                return false;
            }
            else if (WizardConfig.UseSettingsPage &&
                !WizardConfig.UseGeneralSettingPage &&
                (filePath.Contains("GeneralSettingPage") ||
                filePath.Contains("GeneralSettingViewModel") ||
                filePath.Contains("settings.png")))
            {
                return false;
            }
            else if (WizardConfig.UseSettingsPage &&
                !WizardConfig.UseAppUpdatePage &&
                (filePath.Contains("AppUpdateSettingPage") ||
                filePath.Contains("AppUpdateSettingViewModel") ||
                filePath.Contains("update.png")))
            {
                return false;
            }
            else if (!WizardConfig.UseJsonSettings &&
                (filePath.Contains("AppConfig") ||
                filePath.Contains("AppHelper")))
            {
                return false;
            }
            else if (!WizardConfig.UseHomeLandingPage && 
                !WizardConfig.UseColorsDic && 
                filePath.Contains("ThemeResources.xaml"))
            {
                return false;
            }
            else if (filePath.Contains("Resources") && 
                !filePath.Contains("ThemeResources"))
            {
                return false;
            }
            else if (!WizardConfig.UseDeveloperModeSetting && filePath.Contains("devMode.png"))
            {
                return false;
            }
            else if (!WizardImplementation.UseDebugLogger && 
                !WizardImplementation.UseFileLogger && 
                filePath.Contains("LoggerSetup"))
            {
                return false;
            }
            else if (!WizardConfig.UseConvertersDic && filePath.Contains("Converters.xaml"))
            {
                return false;
            }
            else if (!WizardConfig.UseFontsDic && filePath.Contains("Fonts.xaml"))
            {
                return false;
            }
            else if (!WizardConfig.UseWindow11ContextMenu && filePath.Contains("Package-managed.WinContextMenu.appxmanifest"))
            {
                return false;
            }
            else if (WizardConfig.UseWindow11ContextMenu && filePath.Contains("Package-managed.appxmanifest"))
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
