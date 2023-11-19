using System.Collections.Generic;

using EnvDTE;

using Microsoft.VisualStudio.TemplateWizard;

namespace WinUICommunity_VS_Templates
{
    public class WinUIAppMVVMWizard : IWizard
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
            WizardImplementation.RunStarted(automationObject, replacementsDictionary, false, true);
        }

        public bool ShouldAddProjectItem(string filePath)
        {
            if (!WizardImplementation.ShouldAddProjectItem())
            {
                return false;
            }

            if (!WizardImplementation.UseJsonSettings && (filePath.Contains("AppConfig") || filePath.Contains("AppHelper")))
            {
                return false;
            }
            else if (!WizardImplementation.UseDynamicLocalization && filePath.Contains("Resources"))
            {
                return false;
            }
            else if (!WizardImplementation.UseDebugLogger && !WizardImplementation.UseFileLogger && filePath.Contains("LoggerSetup"))
            {
                return false;
            }
            else if (!WizardImplementation.UseColorsDic && filePath.Contains("Colors.xaml"))
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
            else if (WizardImplementation.DotNetVersion.Contains("net7") && (filePath.Contains("win-x64.pubxml") || filePath.Contains("win-x86.pubxml") || filePath.Contains("win-arm64.pubxml")))
            {
                return false;
            }
            else if (!WizardImplementation.DotNetVersion.Contains("net7") && (filePath.Contains("win10-x64.pubxml") || filePath.Contains("win10-x86.pubxml") || filePath.Contains("win10-arm64.pubxml")))
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
