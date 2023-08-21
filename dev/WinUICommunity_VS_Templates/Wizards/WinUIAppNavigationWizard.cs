using System.Collections.Generic;
using System.Threading.Tasks;

using EnvDTE;

using Microsoft.VisualStudio.TemplateWizard;

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
            WizardImplementation.RunStarted(automationObject, replacementsDictionary, runKind, customParams);
        }

        public bool ShouldAddProjectItem(string filePath)
        {
            if (!WizardImplementation.Wizard.AddHomeLandingPage && filePath.Contains("HomeLanding"))
            {
                return false;
            }
            else if (!WizardImplementation.Wizard.AddSettingsPage && (filePath.Contains("SettingsPage.xaml") || filePath.Contains("BreadcrumbBarUserControl") || filePath.Contains("AboutUsSettingPage") || filePath.Contains("ThemeSettingPage") || filePath.Contains("GeneralSettingPage")))
            {
                return false;
            }
            else if (WizardImplementation.Wizard.AddSettingsPage && !WizardImplementation.Wizard.AddAboutPage && filePath.Contains("AboutUsSettingPage"))
            {
                return false;
            }
            else if (WizardImplementation.Wizard.AddSettingsPage && !WizardImplementation.Wizard.AddThemeSettingPage && filePath.Contains("ThemeSettingPage"))
            {
                return false;
            }
            else if (WizardImplementation.Wizard.AddSettingsPage && !WizardImplementation.Wizard.AddGeneralSettingPage && filePath.Contains("GeneralSettingPage"))
            {
                return false;
            }
            else if (!WizardImplementation.Wizard.AddJsonSettings && (filePath.Contains("AppConfig") || filePath.Contains("AppHelper")))
            {
                return false;
            }
            else if (!WizardImplementation.Wizard.AddDynamicLocalization && filePath.Contains("Resources"))
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
