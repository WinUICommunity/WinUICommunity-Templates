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
    public class WizardImplementation
    {
        private bool _shouldAddProjectItem;
        private _DTE _dte;

        public Dictionary<string, string> CSProjectElements;
        public bool UseFileLogger;
        public bool UseDebugLogger;
        public bool UseAppCenter;

        // we can use WinUIApp or other template name to get VSIXRootFolder
        string _vstemplateName = "WinUIApp";

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

            AddSolutionFolder(_solution);

            AddGithubActionFile(project);

            var templatePath = Directory.GetParent(project.FullName).FullName;
            new DynamicLocalizationOption(WizardConfig.UseDynamicLocalization, templatePath);
            new AppUpdateOption(WizardConfig.UseSettingsPage, WizardConfig.UseAppUpdatePage, WizardConfig.UseJsonSettings, isMVVMTemplate, templatePath);
            new NormalizeAppFile(templatePath);
            new NormalizeGlobalUsingFile(WizardConfig.UseJsonSettings, UseFileLogger, UseDebugLogger, templatePath);
            new NormalizeGeneralSettingFile(WizardConfig.UseJsonSettings, WizardConfig.UseSettingsPage, WizardConfig.UseDeveloperModeSetting, WizardConfig.UseGeneralSettingPage, templatePath);
            new NormalizeCSProjFile(project, WizardConfig.UseDynamicLocalization);
        }

        /// <summary>
        /// Get Parameters
        /// </summary>
        /// <param name="automationObject"></param>
        /// <param name="replacementsDictionary"></param>
        /// <param name="runKind"></param>
        /// <param name="customParams"></param>
        public async void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, bool hasPages, bool isMVVMTemplate = false, bool hasNavigationView = false, bool isBlank = false)
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
            
            var inputForm = new MainWindowWizard();
            var result = inputForm.ShowDialog();
            
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            if (result.HasValue && result.Value)
            {
                _shouldAddProjectItem = true;
                
                string wasdkVersion = "1.5.240227000";
                string wasdkBuildToolsVersion = "10.0.22621.3233";
                string winUICommunityComponentsVersion = "6.5.0";
                string winUICommunityCoreVersion = "6.5.0";
                string winUICommunityLandingPagesVersion = "6.5.0";
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

                // Add CSProjectElements
                if (CSProjectElements != null && CSProjectElements.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();

                    foreach (var entity in CSProjectElements)
                    {
                        sb.AppendLine($"    {entity.Value}");
                    }

                    replacementsDictionary.Add("$CustomCSProjectElement$", Environment.NewLine + $"    {sb.ToString().Trim()}");
                }
                else
                {
                    replacementsDictionary.Add("$CustomCSProjectElement$", "");
                }

                // Add Extra Libs
                var libs = WizardConfig.LibraryDic;
                StringBuilder outputBuilder = new StringBuilder();
                if (libs != null)
                {
                    foreach (var lib in libs.Values)
                    {
                        if (isMVVMTemplate && lib.CheckBeforeInsert)
                        {
                            continue;
                        }

                        if (lib.Package.Contains("Microsoft.AppCenter"))
                        {
                            lib.Package = lib.Package.Replace("Microsoft.AppCenter", "Microsoft.AppCenter.Crashes");
                            outputBuilder.AppendLine(lib.Package);

                            lib.Package = lib.Package.Replace("Microsoft.AppCenter.Crashes", "Microsoft.AppCenter.Analytics");
                            outputBuilder.AppendLine(lib.Package);
                            UseAppCenter = true;
                        }
                        else
                        {
                            outputBuilder.AppendLine(lib.Package);
                        }
                    }
                }

                new AppCenterOption().ConfigAppCenter(UseAppCenter, replacementsDictionary);

                string outputText = outputBuilder.ToString();

                if (libs != null)
                {
                    if (libs.Count > 0)
                    {
                        replacementsDictionary.Add("$ExtraLibs$", Environment.NewLine + outputText);
                    }
                    else
                    {
                        replacementsDictionary.Add("$ExtraLibs$", "");
                    }
                }
                else
                {
                    replacementsDictionary.Add("$ExtraLibs$", "");
                }

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

                if (libs != null && libs.ContainsKey("WinUICommunity.Win2D"))
                {
                    replacementsDictionary.Add("$WinUICommunity.Win2D$", Environment.NewLine + "                <ResourceDictionary Source=\"ms-appx:///WinUICommunity.Win2D/Themes/Generic.xaml\" />");
                }
                else
                {
                    replacementsDictionary.Add("$WinUICommunity.Win2D$", "");
                }

                replacementsDictionary.Add("$DotNetVersion$", WizardConfig.DotNetVersion.ToString());
                replacementsDictionary.Add("$TargetFrameworkVersion$", WizardConfig.TargetFrameworkVersion.ToString());
                replacementsDictionary.Add("$Platforms$", WizardConfig.Platforms.ToString());
                replacementsDictionary.Add("$RuntimeIdentifiers$", WizardConfig.RuntimeIdentifiers.ToString());

                replacementsDictionary.Add("$AddJsonSettings$", WizardConfig.UseJsonSettings.ToString());
                replacementsDictionary.Add("$AddDynamicLocalization$", WizardConfig.UseDynamicLocalization.ToString());
                replacementsDictionary.Add("$AddEditorConfig$", WizardConfig.UseEditorConfig.ToString());
                replacementsDictionary.Add("$AddSolutionFolder$", WizardConfig.UseSolutionFolder.ToString());
                replacementsDictionary.Add("$AddHomeLandingPage$", WizardConfig.UseHomeLandingPage.ToString());
                replacementsDictionary.Add("$AddSettingsPage$", WizardConfig.UseSettingsPage.ToString());
                replacementsDictionary.Add("$AddGeneralSettingPage$", WizardConfig.UseGeneralSettingPage.ToString());
                replacementsDictionary.Add("$AddThemeSettingPage$", WizardConfig.UseThemeSettingPage.ToString());
                replacementsDictionary.Add("$AddAppUpdatePage$", WizardConfig.UseAppUpdatePage.ToString());
                replacementsDictionary.Add("$AddAboutPage$", WizardConfig.UseAboutPage.ToString());
                replacementsDictionary.Add("$UseAccelerateBuilds$", WizardConfig.UseAccelerateBuilds.ToString());

                if (WizardConfig.IsUnPackagedMode)
                {
                    replacementsDictionary.Add("$WindowsPackageType$", Environment.NewLine + "    <WindowsPackageType>None</WindowsPackageType>");
                }
                else
                {
                    replacementsDictionary.Add("$WindowsPackageType$", "");
                }

                if (hasNavigationView)
                {
                    new ColorsDicOption().ConfigColorsDic(replacementsDictionary, WizardConfig.UseHomeLandingPage);
                }

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

                new DictionaryOption().ConfigDictionary(replacementsDictionary, hasNavigationView, WizardConfig.UseHomeLandingPage, WizardConfig.UseColorsDic, WizardConfig.UseStylesDic, WizardConfig.UseConvertersDic, WizardConfig.UseFontsDic);
                var serilog = new SerilogOption();
                serilog.ConfigSerilog(replacementsDictionary, libs, WizardConfig.UseJsonSettings, WizardConfig.UseDeveloperModeSetting);
                UseFileLogger = serilog.UseFileLogger;
                UseDebugLogger = serilog.UseDebugLogger;

                var configCodes = new ConfigCodes(WizardConfig.UseAboutPage, WizardConfig.UseAppUpdatePage, WizardConfig.UseGeneralSettingPage, WizardConfig.UseHomeLandingPage, WizardConfig.UseSettingsPage, WizardConfig.UseThemeSettingPage, WizardConfig.UseDeveloperModeSetting, WizardConfig.UseJsonSettings);

                if (isMVVMTemplate)
                {
                    configCodes.ConfigAllMVVM();
                }
                else
                {
                    configCodes.ConfigAll();
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
                    replacementsDictionary.Add("$Configs$", configs);
                }

                if (configCodes.ServiceDic.Count > 0)
                {
                    replacementsDictionary.Add("$Services$", Environment.NewLine + services);
                }
                else
                {
                    replacementsDictionary.Add("$Services$", services);
                }

                replacementsDictionary.Add("$SettingsCards$", settingsCards);

                if (libs.ContainsKey("Serilog.Sinks.Debug") || libs.ContainsKey("Serilog.Sinks.File"))
                {
                    replacementsDictionary.AddIfNotExists("$GeneralSettingsCards$", generalSettingsCards);

                    if (WizardConfig.UseJsonSettings && WizardConfig.UseDeveloperModeSetting && WizardConfig.UseSettingsPage && WizardConfig.UseGeneralSettingPage)
                    {
                        replacementsDictionary.AddIfNotExists("$GoToLogPathEvent$", Environment.NewLine + Environment.NewLine + SettingsCardOptions.GoToLogPathEvent);
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

                if (WizardConfig.UseJsonSettings)
                {
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
                    replacementsDictionary.Add("$AppConfigFilePath$", "");
                    replacementsDictionary.Add("$AppUpdateConfig$", "");
                }
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

        public void AddSolutionFolder(Solution2 solution)
        {
            if (WizardConfig.UseSolutionFolder)
            {
                var solutionFolder = solution.AddSolutionFolder("Solution Items");
            }
        }
        public async void AddEditorConfigFile()
        {
            if (WizardConfig.UseEditorConfig)
            {
                var vsixRoot = await GetRootFolderPathAsync(_vstemplateName);

                var inputFile = vsixRoot.VSIXRootFolder + @"\Files\.editorconfig";

                var outputFile = SolutionDirectory + @"\.editorconfig";
                CopyFileToDestination(inputFile, outputFile);
            }
        }
        public async void AddGithubActionFile(Project project)
        {
            if (WizardConfig.UseGithubWorkflow)
            {
                var vsixRoot = await GetRootFolderPathAsync(_vstemplateName);
                var inputFile = vsixRoot.VSIXRootFolder + @"\Files\workflow.yml";
                string outputDir = SolutionDirectory + @"\.github\workflows\";

                if (!Directory.Exists(outputDir))
                {
                    Directory.CreateDirectory(outputDir);
                }

                var outputFile = outputDir + "dotnet-release.yml";
                CopyFileToDestination(inputFile, outputFile);

                if (File.Exists(outputFile))
                {
                    var fileContent = File.ReadAllText(outputFile);
                    fileContent = fileContent.Replace("YOUR_Folder/YOUR_APP_NAME.csproj", project.UniqueName);
                    fileContent = fileContent.Replace("YOUR_APP_NAME", project.Name);
                    var platforms = WizardConfig.Platforms.Replace(";", ", ");
                    fileContent = fileContent.Replace("[x64, x86, arm64]", $"[{platforms}]");

                    File.WriteAllText(outputFile, fileContent);
                }
            }
        }
        public async void CopyFileToDestination(string inputfile, string outputfile)
        {
            try
            {
                await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

                // Check if the file exists
                if (File.Exists(inputfile))
                {
                    // Assuming 'outputfile' is the destination path
                    string destinationPath = outputfile;

                    // Copy the file
                    File.Copy(inputfile, destinationPath, true);

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
        /// VSIXRootFolder: AppData\Local\Microsoft\VisualStudio\17.0_b6438676Exp\Extensions\{UserName}\WinUICommunity Templates for WinUI\{Version}
        /// ProjectTemplatesFolder: APPDATA\LOCAL\MICROSOFT\VISUALSTUDIO\{VS_Version}\EXTENSIONS\MAHDI HOSSEINI\WINUICOMMUNITY TEMPLATES FOR WINUI\{Version}\ProjectTemplates\CSharp\1033\{Template}
        /// </summary>
        /// <param name="vstemplateName"></param>
        /// <returns></returns>
        public async Task<(string VSIXRootFolder, string ProjectTemplatesFolder)> GetRootFolderPathAsync(string vstemplateName)
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
            return (folderPath, projectTemplatesFolder);
        }
    }
}
