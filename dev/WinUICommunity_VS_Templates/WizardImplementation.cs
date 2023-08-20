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
        private bool AddJsonSettingsOption;
        private bool AddDynamicLocalizationOption;
        private bool AddEditorConfigOption;
        private bool AddSolutionFolderOption;
        private bool AddHomeLandingPageOption;
        private bool AddSettingsPageOption;
        private bool AddThemeSettingPageOption;
        private bool AddAppUpdatePageOption;
        private bool AddAboutPageOption;

        private string appFilePath;
        _DTE _dte;
        Solution2 _solution;
        Project project;

        public void RunFinished(string vstemplateName)
        {
            _solution = (Solution2)_dte.Solution;
            project = _dte.Solution.Projects.Item(1);

            string rootFolderPath = GetRootFolderPath(vstemplateName);
            
            appFilePath = Path.Combine(Directory.GetParent(project.FullName).FullName, "App.xaml.cs");

            if (AddJsonSettingsOption)
            {
                string jsonSettingFolder = Path.Combine(rootFolderPath, "Templates", "Common");
                AddFiles(jsonSettingFolder, "Common");
            }

            AddDynamicLocalization(vstemplateName, appFilePath);

            //if (AddEditorConfig)
            //{
            //    string editorConfig = Path.Combine(rootFolderPath, "Templates", ".editorconfig");
            //    project.ProjectItems.AddFromFileCopy(editorConfig);
            //}

            if (AddSolutionFolderOption)
            {
                var solutionFolder = _solution.AddSolutionFolder("Solution Items");
            }
        }

        public void AddPagesForNavigationTemplates(string vstemplateName, bool isMVVMTemplate)
        {
            string appFileContent = File.ReadAllText(appFilePath);

            if (AddSettingsPageOption || AddHomeLandingPageOption)
            {
                string rootFolderPath = GetRootFolderPath(vstemplateName);

                string templateFolder = Path.Combine(rootFolderPath, "Templates", "Views");

                string templatePath = Path.Combine(Directory.GetParent(project.FullName).FullName, "Views");
                if (!Directory.Exists(templatePath))
                {
                    Directory.CreateDirectory(templatePath);
                }

                foreach (string sourceFilePath in Directory.GetFiles(templateFolder))
                {
                    string fileContent = File.ReadAllText(sourceFilePath);

                    string projectName = project.Name;
                    fileContent = fileContent.Replace("$safeprojectname$", projectName);

                    string fileName = Path.GetFileName(sourceFilePath);
                    string destinationPath = Path.Combine(Directory.GetParent(project.FullName).FullName, "Views", fileName);

                    if ((sourceFilePath.Contains("HomeLandingPage") && AddHomeLandingPageOption) ||
                        (sourceFilePath.Contains("SettingsPage") && AddSettingsPageOption))
                    {
                        File.WriteAllText(destinationPath, fileContent);
                        project.ProjectItems.AddFromFile(destinationPath);
                    }
                }

                if (AddSettingsPageOption)
                {
                    string userControlFolder = Path.Combine(rootFolderPath, "Templates", "Views", "UserControls");
                    string usercontrolPath = Path.Combine(Directory.GetParent(project.FullName).FullName, Path.Combine("Views", "UserControls"));
                    if (!Directory.Exists(usercontrolPath))
                    {
                        Directory.CreateDirectory(usercontrolPath);
                    }

                    foreach (string sourceFilePath in Directory.GetFiles(userControlFolder))
                    {
                        string fileContent = File.ReadAllText(sourceFilePath);

                        string projectName = project.Name;
                        fileContent = fileContent.Replace("$safeprojectname$", projectName);

                        string fileName = Path.GetFileName(sourceFilePath);
                        string destinationPath = Path.Combine(Directory.GetParent(project.FullName).FullName, Path.Combine("Views", "UserControls"), fileName);

                        File.WriteAllText(destinationPath, fileContent);
                        project.ProjectItems.AddFromFile(destinationPath);
                    }

                    if (isMVVMTemplate == false)
                    {
                        appFileContent = appFileContent.Replace("//JsonNavigationViewService.ConfigSettingsPage(typeof(SettingsPage));", "JsonNavigationViewService.ConfigSettingsPage(typeof(SettingsPage));");
                    }
                }

                if (AddHomeLandingPageOption)
                {
                    if (isMVVMTemplate == false)
                    {
                        appFileContent = appFileContent.Replace("//JsonNavigationViewService.ConfigDefaultPage(typeof(HomeLandingPage));", "JsonNavigationViewService.ConfigDefaultPage(typeof(HomeLandingPage));");
                    }
                }

                File.WriteAllText(appFilePath, appFileContent);
            }
        }

        public void AddSettingsSubPages(string vstemplateName)
        {
            if (AddSettingsPageOption && (AddThemeSettingPageOption || AddAboutPageOption))
            {
                string rootFolderPath = GetRootFolderPath(vstemplateName);

                string templateFolder = Path.Combine(rootFolderPath, "Templates", "Views", "Settings");
                string templatePath = Path.Combine(Directory.GetParent(project.FullName).FullName, Path.Combine("Views", "Settings"));
                if (!Directory.Exists(templatePath))
                {
                    Directory.CreateDirectory(templatePath);
                }

                foreach (string sourceFilePath in Directory.GetFiles(templateFolder))
                {
                    string fileContent = File.ReadAllText(sourceFilePath);

                    string projectName = project.Name;
                    fileContent = fileContent.Replace("$safeprojectname$", projectName);

                    string fileName = Path.GetFileName(sourceFilePath);
                    string destinationPath = Path.Combine(Directory.GetParent(project.FullName).FullName, Path.Combine("Views", "Settings"), fileName);

                    if ((sourceFilePath.Contains("AboutUsSettingPage") && AddAboutPageOption) || (sourceFilePath.Contains("ThemeSettingPage") && AddThemeSettingPageOption))
                    {
                        File.WriteAllText(destinationPath, fileContent);
                        project.ProjectItems.AddFromFile(destinationPath);
                    }
                }
            }
        }

        private void AddDynamicLocalization(string vstemplateName, string appFilePath)
        {
            string csprojFileContent = File.ReadAllText(project.FullName);
            string originalText = """<ItemGroup Label="DynamicLocalization"/>""";
            string appFileContent = File.ReadAllText(appFilePath);

            if (AddDynamicLocalizationOption)
            {

                string rootFolderPath = GetRootFolderPath(vstemplateName);

                string templateFolder = Path.Combine(rootFolderPath, "Templates", "Strings", "en-US");
                AddFiles(templateFolder, Path.Combine("Strings", "en-US"));
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

        private void AddFiles(string sourceFolderPath, string rootFolderName)
        {
            string templatePath = Path.Combine(Directory.GetParent(project.FullName).FullName, rootFolderName);
            if (!Directory.Exists(templatePath))
            {
                Directory.CreateDirectory(templatePath);
            }

            foreach (string sourceFilePath in Directory.GetFiles(sourceFolderPath))
            {
                string fileContent = File.ReadAllText(sourceFilePath);

                string projectName = project.Name;
                fileContent = fileContent.Replace("$safeprojectname$", projectName);

                string fileName = Path.GetFileName(sourceFilePath);
                string destinationPath = Path.Combine(Directory.GetParent(project.FullName).FullName, rootFolderName, fileName);

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
            AddJsonSettingsOption = Wizard.AddJsonSettings;
            AddDynamicLocalizationOption = Wizard.AddDynamicLocalization;
            AddEditorConfigOption = Wizard.AddEditorConfig;
            AddSolutionFolderOption = Wizard.AddSolutionFolder;
            AddHomeLandingPageOption = Wizard.AddHomeLandingPage;
            AddSettingsPageOption = Wizard.AddSettingsPage;
            AddThemeSettingPageOption = Wizard.AddThemeSettingPage;
            AddAppUpdatePageOption = Wizard.AddAppUpdatePage;
            AddAboutPageOption = Wizard.AddAboutPage;

            replacementsDictionary.Add("$AddJsonSettings$", AddJsonSettingsOption.ToString());
            replacementsDictionary.Add("$AddDynamicLocalization$", AddDynamicLocalizationOption.ToString());
            replacementsDictionary.Add("$AddEditorConfig$", AddEditorConfigOption.ToString());
            replacementsDictionary.Add("$AddSolutionFolder$", AddSolutionFolderOption.ToString());
            replacementsDictionary.Add("$AddHomeLandingPage$", AddHomeLandingPageOption.ToString());
            replacementsDictionary.Add("$AddSettingsPage$", AddSettingsPageOption.ToString());
            replacementsDictionary.Add("$AddThemeSettingPage$", AddThemeSettingPageOption.ToString());
            replacementsDictionary.Add("$AddAppUpdatePage$", AddAppUpdatePageOption.ToString());
            replacementsDictionary.Add("$AddAboutPage$", AddAboutPageOption.ToString());
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
