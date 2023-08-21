using System.Collections.Generic;
using System.IO;

using EnvDTE;

using EnvDTE80;

using Microsoft.VisualStudio.TemplateWizard;

using WinUICommunity_VS_Templates.Options;

namespace WinUICommunity_VS_Templates
{
    public class WizardImplementation
    {
        string baseReswItemGroupCode = """<ItemGroup Label="DynamicLocalization"/>""";

        string reswItemGroupCode = """
                    <ItemGroup>
                        <Content Include="Strings\**\*.resw">
                          <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
                        </Content>
                      </ItemGroup>
                    """;

        _DTE _dte;
        Solution2 _solution;
        Project project;
        public Wizard Wizard { get; set; }
        public void RunFinished(bool isMVVMTemplate)
        {
            _solution = (Solution2)_dte.Solution;
            project = _dte.Solution.Projects.Item(1);

            var templatePath = Directory.GetParent(project.FullName).FullName;
            new HomeLandingOption(Wizard, isMVVMTemplate, templatePath);
            new SettingsPageOption(Wizard, isMVVMTemplate, templatePath);
            new GeneralSettingOption(Wizard, isMVVMTemplate, templatePath);
            new ThemeSettingOption(Wizard, isMVVMTemplate, templatePath);
            new AboutSettingOption(Wizard, isMVVMTemplate, templatePath);
            new DynamicLocalizationOption(Wizard, templatePath);
            new NormalizeAppFile(templatePath);
            new NormalizeGlobalUsingFile(Wizard, templatePath);
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
            Wizard = new Wizard();
            Wizard.ShowDialog();
            
            replacementsDictionary.Add("$AddJsonSettings$", Wizard.AddJsonSettings.ToString());
            replacementsDictionary.Add("$AddDynamicLocalization$", Wizard.AddDynamicLocalization.ToString());
            replacementsDictionary.Add("$AddEditorConfig$", Wizard.AddEditorConfig.ToString());
            replacementsDictionary.Add("$AddSolutionFolder$", Wizard.AddSolutionFolder.ToString());
            replacementsDictionary.Add("$AddHomeLandingPage$", Wizard.AddHomeLandingPage.ToString());
            replacementsDictionary.Add("$AddSettingsPage$", Wizard.AddSettingsPage.ToString());
            replacementsDictionary.Add("$AddThemeSettingPage$", Wizard.AddThemeSettingPage.ToString());
            replacementsDictionary.Add("$AddAppUpdatePage$", Wizard.AddAppUpdatePage.ToString());
            replacementsDictionary.Add("$AddAboutPage$", Wizard.AddAboutPage.ToString());
        }

        private void UpdateCSProjFileWithResWItemGroup()
        {
            var csprojFileContent = File.ReadAllText(project.FullName);
            if (Wizard.AddDynamicLocalization)
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
            if (Wizard.AddSolutionFolder)
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
