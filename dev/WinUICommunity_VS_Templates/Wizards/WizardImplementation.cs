using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using EnvDTE;

using EnvDTE80;

using Microsoft.VisualStudio.TemplateWizard;

using WinUICommunity_VS_Templates.Options;

namespace WinUICommunity_VS_Templates
{
    public class WizardImplementation
    {
        private bool _shouldAddProjectItem;
        private _DTE _dte;
        private Solution2 _solution;
        private Project project;

        public string DotNetVersion;
        public string Platforms;
        public string RuntimeIdentifiers;
        public bool UseJsonSettings;
        public bool UseDynamicLocalization;
        public bool UseEditorConfig;
        public bool UseSolutionFolder;
        public bool UseHomeLandingPage;
        public bool UseSettingsPage;
        public bool UseGeneralSettingPage;
        public bool UseThemeSettingPage;
        public bool UseAppUpdatePage;
        public bool UseAboutPage;
        public bool UseAccelerateBuilds;
        
        public void RunFinished(bool isMVVMTemplate)
        {
            _solution = (Solution2)_dte.Solution;
            project = _dte.Solution.Projects.Item(1);

            var templatePath = Directory.GetParent(project.FullName).FullName;
            new HomeLandingOption(UseHomeLandingPage, isMVVMTemplate, templatePath);
            new SettingsPageOption(UseSettingsPage, isMVVMTemplate, templatePath);
            new GeneralSettingOption(UseSettingsPage, UseGeneralSettingPage, isMVVMTemplate, templatePath);
            new ThemeSettingOption(UseSettingsPage, UseThemeSettingPage, isMVVMTemplate, templatePath);
            new AboutSettingOption(UseSettingsPage, UseAboutPage, isMVVMTemplate, templatePath);
            new DynamicLocalizationOption(UseDynamicLocalization, templatePath);
            new AppUpdateOption(UseSettingsPage, UseAppUpdatePage, UseJsonSettings, isMVVMTemplate, templatePath);
            new NormalizeAppFile(templatePath);
            new NormalizeGlobalUsingFile(UseJsonSettings, templatePath);
            new NormalizeCSProjFile(project, UseDynamicLocalization);
        }

        /// <summary>
        /// Get Parameters
        /// </summary>
        /// <param name="automationObject"></param>
        /// <param name="replacementsDictionary"></param>
        /// <param name="runKind"></param>
        /// <param name="customParams"></param>
        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, bool isMVVMTemplate = false)
        {
            _dte = automationObject as _DTE;

            _shouldAddProjectItem = false;

            var inputForm = new Wizard();
            var result = inputForm.ShowDialog();
            if (result.HasValue && result.Value)
            {
                _shouldAddProjectItem = true;

                // Add Base Library Versions
                replacementsDictionary.Add("$WASDKVersion$", "1.4.230822000");
                replacementsDictionary.Add("$WASDKBuildToolsVersion$", "10.0.22621.756");
                replacementsDictionary.Add("$WinUICommunityComponentsVersion$", "5.1.1");
                replacementsDictionary.Add("$WinUICommunityCoreVersion$", "5.1.1");
                replacementsDictionary.Add("$WinUICommunityLandingPagesVersion$", "5.1.1");
                replacementsDictionary.Add("$CommunityToolkitMvvmVersion$", "8.2.1");
                replacementsDictionary.Add("$DependencyInjectionVersion$", "7.0.0");
                replacementsDictionary.Add("$WinUIManagedVersion$", "2.0.9");

                // Add Extra Libs
                var libs = inputForm.LibraryDic;
                StringBuilder outputBuilder = new StringBuilder();
                foreach (var lib in libs.Values)
                {
                    if (isMVVMTemplate && lib.CheckBeforeInsert)
                    {
                        continue;
                    }

                    outputBuilder.AppendLine(lib.Package);
                }

                string outputText = outputBuilder.ToString();

                if (libs.Count > 0)
                {
                    replacementsDictionary.Add("$ExtraLibs$", Environment.NewLine + outputText);
                }
                else
                {
                    replacementsDictionary.Add("$ExtraLibs$", "");
                }

                replacementsDictionary.Add("$DotNetVersion$", inputForm.DotNetVersion.ToString());
                replacementsDictionary.Add("$Platforms$", inputForm.Platforms.ToString());
                replacementsDictionary.Add("$RuntimeIdentifiers$", inputForm.RuntimeIdentifiers.ToString());
                replacementsDictionary.Add("$AddJsonSettings$", inputForm.AddJsonSettings.ToString());
                replacementsDictionary.Add("$AddDynamicLocalization$", inputForm.AddDynamicLocalization.ToString());
                replacementsDictionary.Add("$AddEditorConfig$", inputForm.AddEditorConfig.ToString());
                replacementsDictionary.Add("$AddSolutionFolder$", inputForm.AddSolutionFolder.ToString());
                replacementsDictionary.Add("$AddHomeLandingPage$", inputForm.AddHomeLandingPage.ToString());
                replacementsDictionary.Add("$AddSettingsPage$", inputForm.AddSettingsPage.ToString());
                replacementsDictionary.Add("$AddGeneralSettingPage$", inputForm.AddGeneralSettingPage.ToString());
                replacementsDictionary.Add("$AddThemeSettingPage$", inputForm.AddThemeSettingPage.ToString());
                replacementsDictionary.Add("$AddAppUpdatePage$", inputForm.AddAppUpdatePage.ToString());
                replacementsDictionary.Add("$AddAboutPage$", inputForm.AddAboutPage.ToString());
                replacementsDictionary.Add("$UseAccelerateBuilds$", inputForm.AddAccelerateBuilds.ToString());
                
                DotNetVersion = inputForm.DotNetVersion;
                Platforms = inputForm.Platforms;
                RuntimeIdentifiers = inputForm.RuntimeIdentifiers;
                UseJsonSettings = inputForm.AddJsonSettings;
                UseDynamicLocalization = inputForm.AddDynamicLocalization;
                UseEditorConfig = inputForm.AddEditorConfig;
                UseSolutionFolder = inputForm.AddSolutionFolder;
                UseHomeLandingPage = inputForm.AddHomeLandingPage;
                UseSettingsPage = inputForm.AddSettingsPage;
                UseGeneralSettingPage = inputForm.AddGeneralSettingPage;
                UseThemeSettingPage = inputForm.AddThemeSettingPage;
                UseAppUpdatePage = inputForm.AddAppUpdatePage;
                UseAboutPage = inputForm.AddAboutPage;
                UseAccelerateBuilds = inputForm.AddAccelerateBuilds;
            }
            else
            {
                inputForm.Close();
                throw new WizardBackoutException();
            }
        }

        public bool ShouldAddProjectItem()
        {
            return _shouldAddProjectItem;
        }

        public void AddSolutionFolder()
        {
            if (UseSolutionFolder)
            {
                var solutionFolder = _solution.AddSolutionFolder("Solution Items");
            }
        }

        /// <summary>
        /// Get VSIX Root Folder Path: AppData\Local\Microsoft\VisualStudio\17.0_b6438676Exp\Extensions\{UserName}\WinUICommunity Templates for WinUI\{Version}
        /// </summary>
        /// <param name="vstemplateName"></param>
        /// <returns></returns>
        public (string VSIXRootFolder, string ProjectTemplatesFolder) GetRootFolderPath(string vstemplateName)
        {
            _solution = (Solution2)_dte.Solution;
            Solution2 soln = (Solution2)_dte.Solution;
            var vstemplateFileName = soln.GetProjectTemplate($"{vstemplateName}.vstemplate", "CSharp");

            string folderPath = Path.GetDirectoryName(vstemplateFileName);
            string projectTemplatesFolder = folderPath;
            while (folderPath.Contains("ProjectTemplates"))
            {
                folderPath = Directory.GetParent(folderPath).FullName;
            }
            return (folderPath, projectTemplatesFolder);
        }
    }
}
