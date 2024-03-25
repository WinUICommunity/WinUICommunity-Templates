﻿using System.Collections.Generic;

using EnvDTE;

using Microsoft.VisualStudio.TemplateWizard;
using WinUICommunity_VS_Templates.WizardUI;

namespace WinUICommunity_VS_Templates
{
    public class WinUIAppMVVMWizard : IWizard
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
            WizardImplementation.RunFinished(true);
        }

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            WizardImplementation = new SharedWizard();
            WizardImplementation.RunStarted(automationObject, replacementsDictionary, "WinUIApp-MVVM", false, true);
        }

        public bool ShouldAddProjectItem(string filePath)
        {
            if (!WizardImplementation.ShouldAddProjectItem())
            {
                return false;
            }

            if (!WizardConfig.UseJsonSettings && 
                (filePath.Contains("AppConfig") || 
                filePath.Contains("AppHelper")))
            {
                return false;
            }
            else if (!WizardConfig.UseColorsDic && filePath.Contains("ThemeResources.xaml"))
            {
                return false;
            }
            else if (!WizardConfig.UseDynamicLocalization && 
                filePath.Contains("Resources") && 
                !filePath.Contains("ThemeResources"))
            {
                return false;
            }
            else if (!WizardImplementation.UseDebugLogger && 
                !WizardImplementation.UseFileLogger && 
                filePath.Contains("LoggerSetup"))
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
            else
            {
                return true;
            }
        }
    }
}
