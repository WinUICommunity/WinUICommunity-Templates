﻿using System.Collections.Generic;
using System.IO;

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
        
        string baseReswItemGroupCode = """<ItemGroup Label="DynamicLocalization"/>""";

        string reswItemGroupCode = """
                    <ItemGroup>
                        <Content Include="Strings\**\*.resw">
                          <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
                        </Content>
                      </ItemGroup>
                    """;
        
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
            new NormalizeAppFile(templatePath);
            new NormalizeGlobalUsingFile(UseJsonSettings, templatePath);
            UpdateCSProjFileWithResWItemGroup();
        }

        /// <summary>
        /// Get Parameters
        /// </summary>
        /// <param name="automationObject"></param>
        /// <param name="replacementsDictionary"></param>
        /// <param name="runKind"></param>
        /// <param name="customParams"></param>
        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            _dte = automationObject as _DTE;

            _shouldAddProjectItem = false;

            var inputForm = new Wizard();
            var result = inputForm.ShowDialog();
            if (result.HasValue && result.Value)
            {
                _shouldAddProjectItem = true;

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

        private void UpdateCSProjFileWithResWItemGroup()
        {
            var csprojFileContent = File.ReadAllText(project.FullName);
            if (UseDynamicLocalization)
            {
                csprojFileContent = csprojFileContent.Replace(baseReswItemGroupCode, reswItemGroupCode);
            }
            else
            {
                csprojFileContent = csprojFileContent.Replace(baseReswItemGroupCode, "");
            }
            File.WriteAllText(project.FullName, csprojFileContent);
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
