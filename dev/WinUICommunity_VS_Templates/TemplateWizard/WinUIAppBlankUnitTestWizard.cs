using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinUICommunity_VS_Templates.WizardUI;

namespace WinUICommunity_VS_Templates
{
    public class WinUIAppBlankUnitTestWizard : IWizard
    {
        SharedWizard WizardImplementation;

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
        }

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            WizardImplementation = new SharedWizard();
            WizardImplementation.RunStarted(automationObject, replacementsDictionary, "WinUIApp-Blank-UnitTest", false, false, false, true);
        }

        public bool ShouldAddProjectItem(string filePath)
        {
            if (!WizardImplementation.ShouldAddProjectItem())
            {
                return false;
            }

            else if (!WizardConfig.UseColorsDic && filePath.Contains("ThemeResources.xaml"))
            {
                return false;
            }
            else if (!WizardConfig.UseStylesDic && filePath.Contains("Styles.xaml"))
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
