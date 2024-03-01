using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;
using System.Collections.Generic;

namespace WinUICommunity_VS_Templates
{
    public class WinUIAppBlankWizard : IWizard
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
            WizardImplementation.AddEditorConfigFile("WinUIApp-Blank");
            WizardImplementation.AddGithubActionFile("WinUIApp-Blank");
        }

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            WizardImplementation = new WizardImplementation();
            WizardImplementation.RunStarted(automationObject, replacementsDictionary, false, false, false, true);
        }

        public bool ShouldAddProjectItem(string filePath)
        {
            if (!WizardImplementation.ShouldAddProjectItem())
            {
                return false;
            }

            else if (!WizardImplementation.UseColorsDic && filePath.Contains("ThemeResources.xaml"))
            {
                return false;
            }
            else if (!WizardImplementation.UseStylesDic && filePath.Contains("Styles.xaml"))
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
