using System;
using System.Collections.Generic;
using System.IO;

using EnvDTE;

using EnvDTE80;

using Microsoft.VisualStudio.TemplateWizard;

namespace WinUICommunity_VS_Templates
{
    public class WizardImplementation
    {
        private bool AddJsonSettings;
        private bool AddDynamicLocalization;
        private bool AddEditorConfig;
        private bool AddSolutionFolder;
        private bool AddHomeLandingPage;
        private bool AddSettingsPage;
        private bool AddThemeSettingPage;
        private bool AddAppUpdatePage;
        private bool AddAboutPage;

        _DTE _dte;
        Solution2 _solution;
        
        public void RunFinished(string vstemplateName)
        {
            _solution = (Solution2)_dte.Solution;
            Project project = _dte.Solution.Projects.Item(1);

            string rootFolderPath = GetRootFolderPath(vstemplateName);
            
            string originalText = """<ItemGroup Label="DynamicLocalization"/>""";
            string csprojFileContent = File.ReadAllText(project.FullName);

            var appFilePath = Path.Combine(Directory.GetParent(project.FullName).FullName, "App.xaml.cs");

            string appFileContent = File.ReadAllText(appFilePath);

            if (AddJsonSettings)
            {
                string jsonSettingFolder = Path.Combine(rootFolderPath, "Templates", "Common");
                AddFilesAndReplaceParameters(project, jsonSettingFolder, "Common");
            }

            AddDynamicLocalizationAndReplaceParameters(project, rootFolderPath, appFileContent, appFilePath, csprojFileContent, originalText);

            //if (AddEditorConfig)
            //{
            //    string editorConfig = Path.Combine(rootFolderPath, "Templates", ".editorconfig");
            //    project.ProjectItems.AddFromFileCopy(editorConfig);
            //}

            if (AddSolutionFolder)
            {
                var solutionFolder = _solution.AddSolutionFolder("Solution Items");
            }
        }

        private void AddDynamicLocalizationAndReplaceParameters(Project project, string folderPath, string appFileContent, string appFilePath, string csprojFileContent, string originalText)
        {
            if (AddDynamicLocalization)
            {
                string dynamicLocalizationFolder = Path.Combine(folderPath, "Templates", "Strings", "en-US");
                AddFilesAndReplaceParameters(project, dynamicLocalizationFolder, Path.Combine("Strings", "en-US"));
                string newText = """
                    <ItemGroup>
                        <Content Include="Strings\**\*.resw">
                          <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
                        </Content>
                      </ItemGroup>
                    """;
                csprojFileContent = csprojFileContent.Replace(originalText, newText);
                File.WriteAllText(project.FullName, csprojFileContent);

                string dynamicInitializeCode = 
"""
private async Task InitializeLocalizer(params string[] languages)
    {
        // Initialize a "Strings" folder in the "LocalFolder" for the packaged app.
        if (ApplicationHelper.IsPackaged)
        {
            // Create string resources file from app resources if doesn't exists.
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFolder stringsFolder = await localFolder.CreateFolderAsync(
                "Strings",
                CreationCollisionOption.OpenIfExists);
            string resourceFileName = "Resources.resw";
            foreach (var item in languages)
            {
                await LocalizerBuilder.CreateStringResourceFileIfNotExists(stringsFolder, item, resourceFileName);
            }

            StringsFolderPath = stringsFolder.Path;
        }
        else
        {
            // Initialize a "Strings" folder in the executables folder.
            StringsFolderPath = Path.Combine(AppContext.BaseDirectory, "Strings");
            var stringsFolder = await StorageFolder.GetFolderFromPathAsync(StringsFolderPath);
        }


        ILocalizer localizer = await new LocalizerBuilder()
            .AddStringResourcesFolderForLanguageDictionaries(StringsFolderPath)
            .SetOptions(options =>
            {
                options.DefaultLanguage = "en-US";
                options.UseUidWhenLocalizedStringNotFound = true;
            })
            .Build();
    }
""";
                appFileContent = appFileContent.Replace("private void InitializeLocalizer { };",dynamicInitializeCode);
                appFileContent = appFileContent.Replace("await InitializeLocalizer(\"en-US\");", Environment.NewLine + Environment.NewLine + "        await InitializeLocalizer(\"en-US\");");
                File.WriteAllText(appFilePath, appFileContent);
            }
            else
            {
                csprojFileContent = csprojFileContent.Replace(originalText, "");
                File.WriteAllText(project.FullName, csprojFileContent);

                appFileContent = appFileContent.Replace("using Windows.Storage;", "");
                appFileContent = appFileContent.Replace("private static string StringsFolderPath { get; set; } = string.Empty;", "");
                appFileContent = appFileContent.Replace("await InitializeLocalizer(\"en-US\");", "");
                appFileContent = appFileContent.Replace("protected async override void OnLaunched", "protected override void OnLaunched");
                appFileContent = appFileContent.Replace("private void InitializeLocalizer { };", "");
                File.WriteAllText(appFilePath, appFileContent);
            }
        }

        private void AddFilesAndReplaceParameters(Project project, string sourceFolderPath, string folderName)
        {
            string dirPath = Path.Combine(Directory.GetParent(project.FullName).FullName, folderName);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            foreach (string sourceFilePath in Directory.GetFiles(sourceFolderPath))
            {
                string fileContent = File.ReadAllText(sourceFilePath);

                string projectName = project.Name;
                fileContent = fileContent.Replace("$safeprojectname$", projectName);

                string fileName = Path.GetFileName(sourceFilePath);
                string destinationPath = Path.Combine(Directory.GetParent(project.FullName).FullName, folderName, fileName);

                File.WriteAllText(destinationPath, fileContent);
                project.ProjectItems.AddFromFile(destinationPath);
            }
        }

        private void OldMethods()
        {
            //project.ProjectItems.AddFromDirectory(jsonSettingFolder);

            //string jsonSettingFile1 = Path.Combine(folderPath, "Templates", "Common", "AppConfig.cs");
            //string jsonSettingFile2 = Path.Combine(folderPath, "Templates", "Common", "AppHelper.cs");

            //if (File.Exists(jsonSettingFile1) && File.Exists(jsonSettingFile2))
            //{
            //    //project.ProjectItems.AddFromTemplate(jsonSettingFile1, Path.Combine("Common", "AppConfig.cs"));
            //    //project.ProjectItems.AddFromTemplate(jsonSettingFile2, Path.Combine("Common", "AppHelper.cs"));
            //}
        }

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            _dte = automationObject as _DTE;
            var window = new Wizard();
            window.ShowDialog();
            AddJsonSettings = Wizard.AddJsonSettings;
            AddDynamicLocalization = Wizard.AddDynamicLocalization;
            AddEditorConfig = Wizard.AddEditorConfig;
            AddSolutionFolder = Wizard.AddSolutionFolder;
            AddHomeLandingPage = Wizard.AddHomeLandingPage;
            AddSettingsPage = Wizard.AddSettingsPage;
            AddThemeSettingPage = Wizard.AddThemeSettingPage;
            AddAppUpdatePage = Wizard.AddAppUpdatePage;
            AddAboutPage = Wizard.AddAboutPage;

            replacementsDictionary.Add("$AddJsonSettings$", AddJsonSettings.ToString());
            replacementsDictionary.Add("$AddDynamicLocalization$", AddDynamicLocalization.ToString());
            replacementsDictionary.Add("$AddEditorConfig$", AddEditorConfig.ToString());
            replacementsDictionary.Add("$AddSolutionFolder$", AddSolutionFolder.ToString());
            replacementsDictionary.Add("$AddHomeLandingPage$", AddHomeLandingPage.ToString());
            replacementsDictionary.Add("$AddSettingsPage$", AddSettingsPage.ToString());
            replacementsDictionary.Add("$AddThemeSettingPage$", AddThemeSettingPage.ToString());
            replacementsDictionary.Add("$AddAppUpdatePage$", AddAppUpdatePage.ToString());
            replacementsDictionary.Add("$AddAboutPage$", AddAboutPage.ToString());
        }

        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }

        public string GetRootFolderPath(string vstemplateName)
        {
            _solution = (Solution2)_dte.Solution;
            Solution2 soln = (Solution2)_dte.Solution;
            var vstemplateFileName = soln.GetProjectTemplate($"{vstemplateName}.vstemplate", "CSharp");

            string folderPath = Path.GetDirectoryName(vstemplateFileName);
            while (folderPath.Contains("ProjectTemplates"))
            {
                folderPath = Directory.GetParent(folderPath).FullName;
            }
            return folderPath;
        }
    }
}
