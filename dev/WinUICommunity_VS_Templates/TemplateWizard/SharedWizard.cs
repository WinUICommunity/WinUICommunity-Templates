using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;

using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TemplateWizard;
using WinUICommunity_VS_Templates.Options;
using WinUICommunity_VS_Templates.WizardUI;

namespace WinUICommunity_VS_Templates
{
    public class SharedWizard
    {
        private Dictionary<string, string> solutionFiles = new();
        private bool _shouldAddProjectItem;
        private _DTE _dte;

        public bool UseFileLogger;
        public bool UseDebugLogger;

        public string VSTemplateFilePath; // %AppData%\...\Extensions\Mahdi Hosseini\WinUICommunity Templates for WinUI\{Version}\ProjectTemplates\CSharp\1033\{Template}\{Template}.vstemplate
        public string ProjectTemplatesFolderPath; // %AppData%\...\Extensions\Mahdi Hosseini\WinUICommunity Templates for WinUI\{Version}\ProjectTemplates\CSharp\1033\{Template}
        public string VSIXRootFolderPath; // %AppData%\...\Extensions\Mahdi Hosseini\WinUICommunity Templates for WinUI\{Version}

        public string ProjectName; // App
        public string SafeProjectName; // App
        public string SpecifiedSolutionName; // App
        public string SolutionDirectory; // E:\\source\\App
        public string DestinationDirectory;// E:\source\App\App
        public async void RunFinished(bool isMVVMTemplate)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            var _solution = (Solution2)_dte.Solution;
            var project = _dte.Solution.Projects.Item(1);

            AddGithubActionFile(project);
            AddXamlStylerConfigFile();

            AddSolutionFolder(_solution);

            //var templatePath = Directory.GetParent(project.FullName).FullName;
            //new AppUpdateOption(isMVVMTemplate, templatePath);
            
            foreach (Document doc in _dte.Documents)
            {
                doc.Close();
            }

            if (WizardConfig.UseReBuildSolution)
            {
                _dte.ExecuteCommand("Build.RebuildSolution");
            }
        }

        /// <summary>
        /// Get Parameters
        /// </summary>
        /// <param name="automationObject"></param>
        /// <param name="replacementsDictionary"></param>
        /// <param name="runKind"></param>
        /// <param name="customParams"></param>
        public async void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, string templateName, bool hasPages, bool isMVVMTemplate = false, bool hasNavigationView = false, bool isBlank = false)
        {
            ProjectName = replacementsDictionary["$projectname$"];
            SafeProjectName = replacementsDictionary["$safeprojectname$"];
            SpecifiedSolutionName = replacementsDictionary["$specifiedsolutionname$"];
            SolutionDirectory = replacementsDictionary["$solutiondirectory$"];
            DestinationDirectory = replacementsDictionary["$destinationdirectory$"];

            _shouldAddProjectItem = false;
            WizardConfig.HasPages = hasPages;
            WizardConfig.IsBlank = isBlank;

            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            _dte = automationObject as _DTE;
            
            var inputForm = new MainWindow();
            var result = inputForm.ShowDialog();
            
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            if (result.HasValue && result.Value)
            {
                var vsix = await GetVSIXPathAsync(templateName);
                VSTemplateFilePath = vsix.VSTemplatePath;
                ProjectTemplatesFolderPath = vsix.ProjectTemplatesFolder;
                VSIXRootFolderPath = vsix.VSIXRootFolder;

                _shouldAddProjectItem = true;
                
                string wasdkVersion = "1.5.240311000";
                string wasdkBuildToolsVersion = "10.0.22621.3233";
                string winUICommunityComponentsVersion = "6.6.0";
                string winUICommunityCoreVersion = "6.6.0";
                string winUICommunityLandingPagesVersion = "6.6.0";
                string communityToolkitMvvmVersion = "8.2.2";
                string dependencyInjectionVersion = "8.0.0";
                string winUIManagedVersion = "2.0.9";

                AddEditorConfigFile();

                if (WizardConfig.UseAlwaysLatestVersion)
                {
                    wasdkVersion = "*";
                    wasdkBuildToolsVersion = "*";
                    winUICommunityComponentsVersion = "*";
                    winUICommunityCoreVersion = "*";
                    winUICommunityLandingPagesVersion = "*";
                    communityToolkitMvvmVersion = "*";
                    dependencyInjectionVersion = "*";
                    winUIManagedVersion = "*";
                }

                // Add Base Library Versions
                replacementsDictionary.Add("$WASDKVersion$", wasdkVersion);
                replacementsDictionary.Add("$WASDKBuildToolsVersion$", wasdkBuildToolsVersion);
                replacementsDictionary.Add("$WinUICommunityComponentsVersion$", winUICommunityComponentsVersion);
                replacementsDictionary.Add("$WinUICommunityCoreVersion$", winUICommunityCoreVersion);
                replacementsDictionary.Add("$WinUICommunityLandingPagesVersion$", winUICommunityLandingPagesVersion);
                replacementsDictionary.Add("$CommunityToolkitMvvmVersion$", communityToolkitMvvmVersion);
                replacementsDictionary.Add("$DependencyInjectionVersion$", dependencyInjectionVersion);
                replacementsDictionary.Add("$WinUIManagedVersion$", winUIManagedVersion);

                replacementsDictionary.Add("$DotNetVersion$", WizardConfig.DotNetVersion.ToString());
                replacementsDictionary.Add("$TargetFrameworkVersion$", WizardConfig.TargetFrameworkVersion.ToString());
                replacementsDictionary.Add("$Platforms$", WizardConfig.Platforms.ToString());
                replacementsDictionary.Add("$RuntimeIdentifiers$", WizardConfig.RuntimeIdentifiers.ToString());

                replacementsDictionary.Add("$AddJsonSettings$", WizardConfig.UseJsonSettings.ToString());
                replacementsDictionary.Add("$AddDynamicLocalization$", WizardConfig.UseDynamicLocalization.ToString());
                replacementsDictionary.Add("$AddEditorConfig$", WizardConfig.UseEditorConfigFile.ToString());
                replacementsDictionary.Add("$AddSolutionFolder$", WizardConfig.UseSolutionFolder.ToString());
                replacementsDictionary.Add("$AddHomeLandingPage$", WizardConfig.UseHomeLandingPage.ToString());
                replacementsDictionary.Add("$AddSettingsPage$", WizardConfig.UseSettingsPage.ToString());
                replacementsDictionary.Add("$AddGeneralSettingPage$", WizardConfig.UseGeneralSettingPage.ToString());
                replacementsDictionary.Add("$AddThemeSettingPage$", WizardConfig.UseThemeSettingPage.ToString());
                replacementsDictionary.Add("$AddAppUpdatePage$", WizardConfig.UseAppUpdatePage.ToString());
                replacementsDictionary.Add("$AddAboutPage$", WizardConfig.UseAboutPage.ToString());

                #region CSProjectElements
                // Add CSProjectElements
                if (WizardConfig.CSProjectElements != null && WizardConfig.CSProjectElements.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();

                    foreach (var entity in WizardConfig.CSProjectElements)
                    {
                        sb.AppendLine($"    {entity.Value}");
                    }

                    WizardConfig.CSProjectElements?.Clear();

                    replacementsDictionary.Add("$CustomCSProjectElement$", Environment.NewLine + $"    {sb.ToString().Trim()}");
                }
                else
                {
                    replacementsDictionary.Add("$CustomCSProjectElement$", "");
                }

                #endregion

                #region Extra Libs
                // Add Extra Libs
                var libs = WizardConfig.LibraryDic;
                StringBuilder outputBuilder = new StringBuilder();
                if (libs != null && libs.Count > 0)
                {
                    foreach (var lib in libs.Values)
                    {
                        if (isMVVMTemplate && lib.CheckBeforeInsert)
                        {
                            continue;
                        }

                        outputBuilder.AppendLine(lib.Package);
                    }

                    string outputText = outputBuilder.ToString();

                    replacementsDictionary.Add("$ExtraLibs$", Environment.NewLine + outputText);
                }
                else
                {
                    replacementsDictionary.Add("$ExtraLibs$", "");
                }

                WizardConfig.LibraryDic?.Clear();
                #endregion

                #region Add Xaml Dictionary if User Use Extra Lib
                #region Blank
                if (isBlank)
                {
                    if (libs != null && libs.ContainsKey("WinUICommunity.Components"))
                    {
                        replacementsDictionary.Add("$WinUICommunity.Components$", Environment.NewLine + "                <ResourceDictionary Source=\"ms-appx:///WinUICommunity.Components/Themes/Generic.xaml\" />");
                    }
                    else
                    {
                        replacementsDictionary.Add("$WinUICommunity.Components$", "");
                    }
                    
                    if (libs != null && libs.ContainsKey("WinUICommunity.LandingPages"))
                    {
                        replacementsDictionary.Add("$WinUICommunity.LandingPages$", Environment.NewLine + "                <ResourceDictionary Source=\"ms-appx:///WinUICommunity.LandingPages/Themes/Generic.xaml\" />");
                        replacementsDictionary.Add("$WinUICommunity.LandingPagesItemTemplate$", Environment.NewLine + "                <wuc:ItemTemplates />");
                        replacementsDictionary.Add("$WinUICommunityRefrence$", Environment.NewLine + "             xmlns:wuc=\"using:WinUICommunity\"");
                    }
                    else
                    {
                        replacementsDictionary.Add("$WinUICommunity.LandingPages$", "");
                        replacementsDictionary.Add("$WinUICommunity.LandingPagesItemTemplate$", "");
                        replacementsDictionary.Add("$WinUICommunityRefrence$", "");
                    }
                }

                #endregion


                if (libs != null && libs.ContainsKey("WinUICommunity.Win2D"))
                {
                    replacementsDictionary.Add("$WinUICommunity.Win2D$", Environment.NewLine + "                <ResourceDictionary Source=\"ms-appx:///WinUICommunity.Win2D/Themes/Generic.xaml\" />");
                }
                else
                {
                    replacementsDictionary.Add("$WinUICommunity.Win2D$", "");
                }

                #endregion

                #region IsUnPackaged
                if (WizardConfig.IsUnPackagedMode)
                {
                    replacementsDictionary.Add("$WindowsPackageType$", "None");
                }
                else
                {
                    replacementsDictionary.Add("$WindowsPackageType$", "MSIX");
                }
                #endregion

                if (hasNavigationView)
                {
                    new ColorsDicOption().ConfigColorsDic(replacementsDictionary, WizardConfig.UseHomeLandingPage);
                }

                // Add Xaml Dictionary
                new DictionaryOption().ConfigDictionary(replacementsDictionary, hasNavigationView, WizardConfig.UseHomeLandingPage, WizardConfig.UseColorsDic, WizardConfig.UseStylesDic, WizardConfig.UseConvertersDic, WizardConfig.UseFontsDic);

                #region Codes
                var configCodes = new ConfigCodes(WizardConfig.UseAboutPage, WizardConfig.UseAppUpdatePage, WizardConfig.UseGeneralSettingPage, WizardConfig.UseHomeLandingPage, WizardConfig.UseSettingsPage, WizardConfig.UseThemeSettingPage, WizardConfig.UseDeveloperModeSetting, WizardConfig.UseJsonSettings);

                if (isMVVMTemplate)
                {
                    configCodes.ConfigAllMVVM(SafeProjectName);
                }
                else
                {
                    configCodes.ConfigAll(SafeProjectName);
                }

                configCodes.ConfigGeneral();

                var configs = configCodes.GetConfigJson();
                var services = configCodes.GetServices();
                var settingsCards = configCodes.GetSettingsPageOptions();
                var generalSettingsCards = configCodes.GetGeneralSettingsPageOptions();

                if (configCodes.ConfigJsonDic.Count > 0)
                {
                    replacementsDictionary.Add("$Configs$", Environment.NewLine + configs);
                }
                else
                {
                    replacementsDictionary.Add("$Configs$", "");
                }

                if (configCodes.ServiceDic.Count > 0)
                {
                    replacementsDictionary.Add("$Services$", Environment.NewLine + services);
                }
                else
                {
                    replacementsDictionary.Add("$Services$", "");
                }

                replacementsDictionary.Add("$SettingsCards$", settingsCards);

                #endregion

                #region Serilog
                var serilog = new SerilogOption();
                serilog.ConfigSerilog(replacementsDictionary, libs, WizardConfig.UseJsonSettings, WizardConfig.UseDeveloperModeSetting);
                UseFileLogger = serilog.UseFileLogger;
                UseDebugLogger = serilog.UseDebugLogger;

                if (libs.ContainsKey("Serilog.Sinks.Debug") || libs.ContainsKey("Serilog.Sinks.File"))
                {
                    replacementsDictionary.AddIfNotExists("$GeneralSettingsCards$", generalSettingsCards);

                    if (WizardConfig.UseJsonSettings && WizardConfig.UseDeveloperModeSetting && WizardConfig.UseSettingsPage && WizardConfig.UseGeneralSettingPage)
                    {
                        replacementsDictionary.AddIfNotExists("$GoToLogPathEvent$", Environment.NewLine + Environment.NewLine + PredefinedCodes.GoToLogPathEvent);
                        replacementsDictionary.AddIfNotExists("$DeveloperModeConfig$", Environment.NewLine + "public virtual bool UseDeveloperMode { get; set; }");
                    }
                    else
                    {
                        replacementsDictionary.AddIfNotExists("$GoToLogPathEvent$", "");
                        replacementsDictionary.AddIfNotExists("$DeveloperModeConfig$", "");
                    }
                }
                else
                {
                    replacementsDictionary.AddIfNotExists("$GoToLogPathEvent$", "");
                    replacementsDictionary.AddIfNotExists("$GeneralSettingsCards$", "");
                    replacementsDictionary.AddIfNotExists("$DeveloperModeConfig$", "");
                }
                #endregion

                #region Json Settings
                if (WizardConfig.UseJsonSettings)
                {
                    if (isMVVMTemplate)
                    {
                        replacementsDictionary.Add("$AppUpdateMVVMGetDateTime$", Environment.NewLine + """LastUpdateCheck = Settings.LastUpdateCheck;""");
                        replacementsDictionary.Add("$AppUpdateMVVMSetDateTime$", Environment.NewLine + """Settings.LastUpdateCheck = DateTime.Now.ToShortDateString();""");
                    }
                    else
                    {
                        replacementsDictionary.Add("$AppUpdateGetDateTime$", Environment.NewLine + Environment.NewLine + """TxtLastUpdateCheck.Text = Settings.LastUpdateCheck;""");
                        replacementsDictionary.Add("$AppUpdateSetDateTime$", Environment.NewLine + """Settings.LastUpdateCheck = DateTime.Now.ToShortDateString();""");
                    }

                    replacementsDictionary.Add("$AppConfigFilePath$", Environment.NewLine + """public static readonly string AppConfigPath = Path.Combine(RootDirectoryPath, "AppConfig.json");""");

                    if (WizardConfig.UseAppUpdatePage && WizardConfig.UseSettingsPage)
                    {
                        replacementsDictionary.Add("$AppUpdateConfig$", Environment.NewLine + """public virtual string LastUpdateCheck { get; set; }""");
                    }
                    else
                    {
                        replacementsDictionary.Add("$AppUpdateConfig$", "");
                    }
                }
                else
                {
                    replacementsDictionary.Add("$AppUpdateMVVMGetDateTime$", "");
                    replacementsDictionary.Add("$AppUpdateMVVMSetDateTime$", "");
                    replacementsDictionary.Add("$AppConfigFilePath$", "");
                    replacementsDictionary.Add("$AppUpdateConfig$", "");
                }
                #endregion

                #region Dynamic Localization
                if (WizardConfig.UseDynamicLocalization)
                {
                    replacementsDictionary.Add("$LocalizerAsyncKeyword$", "async ");
                    replacementsDictionary.Add("$LocalizerActivate$", Environment.NewLine + PredefinedCodes.LocalizerActivateCode);
                    replacementsDictionary.Add("$LocalizerItemGroup$", Environment.NewLine + Environment.NewLine + PredefinedCodes.LocalizerItemGroupCode);
                    replacementsDictionary.Add("$Localizer$", PredefinedCodes.LocalizerInitializeCode);
                }
                else
                {
                    replacementsDictionary.Add("$LocalizerAsyncKeyword$", "");
                    replacementsDictionary.Add("$LocalizerActivate$", "");
                    replacementsDictionary.Add("$LocalizerItemGroup$", "");
                    replacementsDictionary.Add("$Localizer$", "");
                }
                #endregion

                new GlobalUsingOption(replacementsDictionary, SafeProjectName, UseFileLogger, UseDebugLogger);

                //if (UseSettingsPage && UseThemeSettingPage)
                //{
                //    replacementsDictionary.Add("$BackdropTintColorViewModel$", Environment.NewLine + "themeService.ConfigBackdropTintColor();");
                //    replacementsDictionary.Add("$BackdropTintColor$", Environment.NewLine + "ThemeService.ConfigBackdropTintColor();");
                //}
                //else
                //{
                //    replacementsDictionary.Add("$BackdropTintColorViewModel$", "");
                //    replacementsDictionary.Add("$BackdropTintColor$", "");
                //}
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

        public async void AddSolutionFolder(Solution2 solution)
        {
            if (WizardConfig.UseSolutionFolder)
            {
                var solutionFolder = solution.AddSolutionFolder(WizardConfig.SolutionFolderName);
                if (solutionFolder != null)
                {
                    await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

                    foreach (var item in solutionFiles)
                    {
                        solutionFolder.ProjectItems.AddFromFile(item.Value);
                    }

                    solutionFiles.Clear();
                }
            }
        }
        public void AddEditorConfigFile()
        {
            if (WizardConfig.UseEditorConfigFile)
            {
                var inputFile = VSIXRootFolderPath + @"\Files\.editorconfig";

                var outputFile = SolutionDirectory + @"\.editorconfig";
                solutionFiles.AddIfNotExists("EditorConfig", outputFile);
                CopyFileToDestination(inputFile, outputFile);
            }
        }
        public async void AddGithubActionFile(Project project)
        {
            if (WizardConfig.UseGithubWorkflowFile)
            {
                var inputFile = VSIXRootFolderPath + @"\Files\dotnet-release.yml";
                string outputDir = SolutionDirectory + @"\.github\workflows\";

                if (!Directory.Exists(outputDir))
                {
                    Directory.CreateDirectory(outputDir);
                }

                var outputFile = outputDir + "dotnet-release.yml";
                solutionFiles.AddIfNotExists("workflow", outputFile);
                CopyFileToDestination(inputFile, outputFile);

                if (File.Exists(outputFile))
                {
                    await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
                    var fileContent = File.ReadAllText(outputFile);
                    fileContent = fileContent.Replace("YOUR_Folder/YOUR_APP_NAME.csproj", project.UniqueName);
                    fileContent = fileContent.Replace("YOUR_APP_NAME", project.Name);
                    var platforms = WizardConfig.Platforms.Replace(";", ", ");
                    fileContent = fileContent.Replace("[x64, x86, arm64]", $"[{platforms}]");

                    File.WriteAllText(outputFile, fileContent);
                }
            }
        }
        public void AddXamlStylerConfigFile()
        {
            if (WizardConfig.UseXamlStylerFile)
            {
                var inputFile = VSIXRootFolderPath + @"\Files\settings.xamlstyler";

                var outputFile = SolutionDirectory + @"\settings.xamlstyler";
                solutionFiles.AddIfNotExists("XamlStyler", outputFile);
                CopyFileToDestination(inputFile, outputFile);
            }
        }
        public async void CopyFileToDestination(string inputfile, string outputfile)
        {
            try
            {
                // Check if the file exists
                if (File.Exists(inputfile))
                {
                    // Assuming 'outputfile' is the destination path
                    string destinationPath = outputfile;

                    // Copy the file
                    File.Copy(inputfile, destinationPath, true);

                    await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

                    // Refresh the solution explorer to make sure the new file is visible
                    _dte.ExecuteCommand("View.Refresh");
                }
                else
                {
                    // Handle the case where the source file doesn't exist
                    // Log or show an error message
                }
            }
            catch (Exception)
            {
                // Handle exceptions
                // Log or show an error message
            }
        }

        /// <summary>
        /// VSIXRootFolder: %AppData%\...\EXTENSIONS\Mahdi Hosseini\WinUICommunity Templates for WinUI\{Version}
        /// ProjectTemplatesFolder: %AppData%\...\EXTENSIONS\Mahdi Hosseini\WINUICOMMUNITY TEMPLATES FOR WINUI\{Version}\ProjectTemplates\CSharp\1033\{Template}
        /// ProjectTemplatesFolder: // %AppData%\...\Extensions\Mahdi Hosseini\WinUICommunity Templates for WinUI\{Version}\ProjectTemplates\CSharp\1033\{Template}\{Template}.vstemplate
        /// </summary>
        /// <param name="vstemplateName">WinUIApp</param>
        /// <returns></returns>
        public async Task<(string VSIXRootFolder, string ProjectTemplatesFolder, string VSTemplatePath)> GetVSIXPathAsync(string vstemplateName)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            Solution2 soln = (Solution2)_dte.Solution;
            var vstemplateFileName = soln.GetProjectTemplate($"{vstemplateName}.vstemplate", "CSharp");

            string folderPath = Path.GetDirectoryName(vstemplateFileName);
            string projectTemplatesFolder = folderPath;
            while (folderPath.Contains("ProjectTemplates"))
            {
                folderPath = Directory.GetParent(folderPath).FullName;
            }

            return (folderPath, projectTemplatesFolder, vstemplateFileName);
        }
    }
}
