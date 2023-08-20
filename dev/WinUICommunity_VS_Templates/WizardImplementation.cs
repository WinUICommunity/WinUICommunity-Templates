﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

using EnvDTE;

using EnvDTE80;

using Microsoft.VisualStudio.TemplateWizard;

namespace WinUICommunity_VS_Templates
{
    public class WizardImplementation
    {
        string oldThemeSettingOptionContent =
"""
        <!--<wuc:SettingsCard x:Name="ThemeSetting"
        Click="OnSettingCard_Click"
        Description="Select your Theme and Material"
        Header="Appearance &amp; behavior"
        HeaderIcon="{wuc:BitmapIcon Source=Assets/Fluent/theme.png}"
        IsClickEnabled="True"
        Tag="ThemeSettingPage" />-->
""";
        string newThemeSettingOptionContent =
"""
        <wuc:SettingsCard x:Name="ThemeSetting"
        Click="OnSettingCard_Click"
        Description="Select your Theme and Material"
        Header="Appearance &amp; behavior"
        HeaderIcon="{wuc:BitmapIcon Source=Assets/Fluent/theme.png}"
        IsClickEnabled="True"
        Tag="ThemeSettingPage" />
""";
        string oldAboutSettingOptionContent =
"""
        <!--<wuc:SettingsCard x:Name="AboutSetting"
        Click="OnSettingCard_Click"
        Description="About $safeprojectname$ and Developer"
        Header="About us"
        HeaderIcon="{wuc:BitmapIcon Source=Assets/Fluent/info.png}"
        IsClickEnabled="True"
        Tag="AboutUsSettingPage" />-->
""";
        string newAboutSettingOptionContent =
"""
        <wuc:SettingsCard x:Name="AboutSetting"
        Click="OnSettingCard_Click"
        Description="About $safeprojectname$ and Developer"
        Header="About us"
        HeaderIcon="{wuc:BitmapIcon Source=Assets/Fluent/info.png}"
        IsClickEnabled="True"
        Tag="AboutUsSettingPage" />
""";

        string oldResWItemGroupContent = """<ItemGroup Label="DynamicLocalization"/>""";

        string newResWItemGroupContent = """
                    <ItemGroup>
                        <Content Include="Strings\**\*.resw">
                          <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
                        </Content>
                      </ItemGroup>
                    """;

        string dynamicLocalizationInitializeCodeContent =
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
        public bool AddJsonSettingsOption;
        public bool AddDynamicLocalizationOption;
        public bool AddEditorConfigOption;
        public bool AddSolutionFolderOption;
        public bool AddHomeLandingPageOption;
        public bool AddSettingsPageOption;
        public bool AddThemeSettingPageOption;
        public bool AddAppUpdatePageOption;
        public bool AddAboutPageOption;

        _DTE _dte;
        Solution2 _solution;
        Project project;

        public void RunFinished(bool isMVVMTemplate)
        {
            _solution = (Solution2)_dte.Solution;
            project = _dte.Solution.Projects.Item(1);

            var templatePath = Directory.GetParent(project.FullName).FullName;
            AddHomeLandingPageConfig(isMVVMTemplate, templatePath);
            AddSettingsPageConfig(isMVVMTemplate, templatePath);
            AddDynamicLocalization(templatePath);
            UpdateAppFileRemoveBlankLines(templatePath);
            UpdateCSProjFileWithResWItemGroup(templatePath);
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

        private void AddDynamicLocalization(string templatePath)
        {
            var appFilePath = Path.Combine(templatePath, "App.xaml.cs");
            string appFileContent = File.ReadAllText(appFilePath);

            if (AddDynamicLocalizationOption)
            {
                appFileContent = appFileContent.Replace("private void InitializeLocalizer { };", dynamicLocalizationInitializeCodeContent);
                appFileContent = appFileContent.Replace("await InitializeLocalizer(\"en-US\");", Environment.NewLine + Environment.NewLine + "        await InitializeLocalizer(\"en-US\");");
                File.WriteAllText(appFilePath, appFileContent);
            }
            else
            {
                appFileContent = appFileContent.Replace("using Windows.Storage;", "");
                appFileContent = appFileContent.Replace("private static string StringsFolderPath { get; set; } = string.Empty;", "");
                appFileContent = appFileContent.Replace("await InitializeLocalizer(\"en-US\");", "");
                appFileContent = appFileContent.Replace("protected async override void OnLaunched", "protected override void OnLaunched");
                appFileContent = appFileContent.Replace("private void InitializeLocalizer { };", "");
                File.WriteAllText(appFilePath, appFileContent);
            }
        }

        private void AddSettingsPageConfig(bool isMVVMTemplate, string templatePath)
        {
            if (AddSettingsPageOption)
            {
                var appFilePath = Path.Combine(templatePath, "App.xaml.cs");

                string appFileContent = File.ReadAllText(appFilePath);

                // We Should Add Config in JsonNavigationViewService
                if (isMVVMTemplate == false)
                {
                    appFileContent = appFileContent.Replace("//JsonNavigationViewService.ConfigSettingsPage(typeof(SettingsPage));", "JsonNavigationViewService.ConfigSettingsPage(typeof(SettingsPage));");
                    File.WriteAllText(appFilePath, appFileContent);
                }

                if (AddAboutPageOption || AddThemeSettingPageOption)
                {
                    var settingsPageFilePath = Path.Combine(templatePath, "Views", "SettingsPage.xaml");
                    string settingsPageFileContent = File.ReadAllText(settingsPageFilePath);

                    if (AddAboutPageOption)
                    {
                        settingsPageFileContent = settingsPageFileContent.Replace(oldAboutSettingOptionContent, newAboutSettingOptionContent);
                    }
                    if (AddThemeSettingPageOption)
                    {
                        settingsPageFileContent = settingsPageFileContent.Replace(oldThemeSettingOptionContent, newThemeSettingOptionContent);
                    }
                    File.WriteAllText(settingsPageFilePath, settingsPageFileContent);
                }
            }
        }

        private void AddHomeLandingPageConfig(bool isMVVMTemplate, string templatePath)
        {
            if (AddHomeLandingPageOption)
            {
                var appFilePath = Path.Combine(templatePath, "App.xaml.cs");

                string appFileContent = File.ReadAllText(appFilePath);

                // We Should Add Config in JsonNavigationViewService
                if (isMVVMTemplate == false)
                {
                    appFileContent = appFileContent.Replace("//JsonNavigationViewService.ConfigDefaultPage(typeof(HomeLandingPage));", "JsonNavigationViewService.ConfigDefaultPage(typeof(HomeLandingPage));");
                    File.WriteAllText(appFilePath, appFileContent);
                }
            }
        }

        private void UpdateCSProjFileWithResWItemGroup(string templatePath)
        {
            var csprojFilePath = Path.Combine(templatePath, "ProjectTemplate.csproj");
            var csprojFileContent = File.ReadAllText(csprojFilePath);
            if (AddDynamicLocalizationOption)
            {
                csprojFileContent = csprojFileContent.Replace(oldResWItemGroupContent, newResWItemGroupContent);
            }
            else
            {
                csprojFileContent = csprojFileContent.Replace(oldResWItemGroupContent, "");
            }
            File.WriteAllText(csprojFilePath, csprojFileContent);
        }

        private void UpdateAppFileRemoveBlankLines(string templatePath)
        {
            var appFilePath = Path.Combine(templatePath, "App.xaml.cs");
            string appFileContent = File.ReadAllText(appFilePath);

            string pattern = @"(\r\n|\r|\n){2,}";

            string result = Regex.Replace(appFileContent, pattern, "\n");

            // Remove blank lines between two closing curly braces
            result = Regex.Replace(result, @"(\}\s*)\n+(\s*\})", "$1$2");
            File.WriteAllText(appFilePath, appFileContent);
        }
        public void AddSolutionFolder()
        {
            if (AddSolutionFolderOption)
            {
                var solutionFolder = _solution.AddSolutionFolder("Solution Items");
            }
        }

        public void AddEditorConfig()
        {
            //if (AddEditorConfig)
            //{
            //    string editorConfig = Path.Combine(rootFolderPath, "Templates", ".editorconfig");
            //    project.ProjectItems.AddFromFileCopy(editorConfig);
            //}
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
    }
}
