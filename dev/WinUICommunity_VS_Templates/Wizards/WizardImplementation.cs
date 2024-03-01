using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using EnvDTE;

using EnvDTE80;
using Microsoft.VisualStudio.TemplateWizard;
using WinUICommunity_VS_Templates.Options;
using WinUICommunity_VS_Templates.WizardUI;

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
        public bool UseGithubWorkflow;
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
        public bool UseFileLogger;
        public bool UseDebugLogger;
        public bool UseAppCenter;
        public bool UseDeveloperModeSetting;
        public bool UseColorsDic;
        public bool UseStylesDic;
        public bool UseConvertersDic;
        public bool UseFontsDic;
        public void RunFinished(bool isMVVMTemplate)
        {
            _solution = (Solution2)_dte.Solution;
            project = _dte.Solution.Projects.Item(1);

            var templatePath = Directory.GetParent(project.FullName).FullName;
            new DynamicLocalizationOption(UseDynamicLocalization, templatePath);
            new AppUpdateOption(UseSettingsPage, UseAppUpdatePage, UseJsonSettings, isMVVMTemplate, templatePath);
            new NormalizeAppFile(templatePath);
            new NormalizeGlobalUsingFile(UseJsonSettings, UseFileLogger, UseDebugLogger, templatePath);
            new NormalizeGeneralSettingFile(UseJsonSettings, UseSettingsPage, UseDeveloperModeSetting, UseGeneralSettingPage, templatePath);
            new NormalizeCSProjFile(project, UseDynamicLocalization);
        }

        /// <summary>
        /// Get Parameters
        /// </summary>
        /// <param name="automationObject"></param>
        /// <param name="replacementsDictionary"></param>
        /// <param name="runKind"></param>
        /// <param name="customParams"></param>
        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, bool hasPages, bool isMVVMTemplate = false, bool hasNavigationView = false, bool isBlank = false)
        {
            _dte = automationObject as _DTE;

            _shouldAddProjectItem = false;
            WizardConfig.HasPages = hasPages;
            WizardConfig.IsBlank = isBlank;
            var inputForm = new MainWindowWizard();
            var result = inputForm.ShowDialog();
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

                Platforms = WizardConfig.Platforms;
                RuntimeIdentifiers = WizardConfig.RuntimeIdentifiers;
                UseJsonSettings = WizardConfig.AddJsonSettings;
                UseDynamicLocalization = WizardConfig.AddDynamicLocalization;
                UseEditorConfig = WizardConfig.AddEditorConfig;
                UseSolutionFolder = WizardConfig.AddSolutionFolder;
                UseHomeLandingPage = WizardConfig.AddHomeLandingPage;
                UseSettingsPage = WizardConfig.AddSettingsPage;
                UseGeneralSettingPage = WizardConfig.AddGeneralSettingPage;
                UseThemeSettingPage = WizardConfig.AddThemeSettingPage;
                UseAppUpdatePage = WizardConfig.AddAppUpdatePage;
                UseAboutPage = WizardConfig.AddAboutPage;
                UseAccelerateBuilds = WizardConfig.AddAccelerateBuilds;
                UseDeveloperModeSetting = WizardConfig.AddDeveloperModeSetting;
                UseColorsDic = WizardConfig.AddColorsDic;
                UseStylesDic = WizardConfig.AddStylesDic;
                UseConvertersDic = WizardConfig.AddConvertersDic;
                UseFontsDic = WizardConfig.AddFontsDic;
                DotNetVersion = WizardConfig.DotNetVersion;
                UseGithubWorkflow = WizardConfig.UseGithubWorkflow;

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

                replacementsDictionary.Add("$AddJsonSettings$", WizardConfig.AddJsonSettings.ToString());
                replacementsDictionary.Add("$AddDynamicLocalization$", WizardConfig.AddDynamicLocalization.ToString());
                replacementsDictionary.Add("$AddEditorConfig$", WizardConfig.AddEditorConfig.ToString());
                replacementsDictionary.Add("$AddSolutionFolder$", WizardConfig.AddSolutionFolder.ToString());
                replacementsDictionary.Add("$AddHomeLandingPage$", WizardConfig.AddHomeLandingPage.ToString());
                replacementsDictionary.Add("$AddSettingsPage$", WizardConfig.AddSettingsPage.ToString());
                replacementsDictionary.Add("$AddGeneralSettingPage$", WizardConfig.AddGeneralSettingPage.ToString());
                replacementsDictionary.Add("$AddThemeSettingPage$", WizardConfig.AddThemeSettingPage.ToString());
                replacementsDictionary.Add("$AddAppUpdatePage$", WizardConfig.AddAppUpdatePage.ToString());
                replacementsDictionary.Add("$AddAboutPage$", WizardConfig.AddAboutPage.ToString());
                replacementsDictionary.Add("$UseAccelerateBuilds$", WizardConfig.AddAccelerateBuilds.ToString());

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
                    new ColorsDicOption().ConfigColorsDic(replacementsDictionary, UseHomeLandingPage);
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

                new DictionaryOption().ConfigDictionary(replacementsDictionary, hasNavigationView, UseHomeLandingPage, UseColorsDic, UseStylesDic, UseConvertersDic, UseFontsDic);
                var serilog = new SerilogOption();
                serilog.ConfigSerilog(replacementsDictionary, libs, UseJsonSettings, UseDeveloperModeSetting);
                UseFileLogger = serilog.UseFileLogger;
                UseDebugLogger = serilog.UseDebugLogger;

                var configCodes = new ConfigCodes(UseAboutPage, UseAppUpdatePage, UseGeneralSettingPage, UseHomeLandingPage, UseSettingsPage, UseThemeSettingPage, UseDeveloperModeSetting, UseJsonSettings);

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

                    if (UseJsonSettings && UseDeveloperModeSetting && UseSettingsPage && UseGeneralSettingPage)
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

                if (UseJsonSettings)
                {
                    replacementsDictionary.Add("$AppConfigFilePath$", Environment.NewLine + """public static readonly string AppConfigPath = Path.Combine(RootDirectoryPath, "AppConfig.json");""");

                    if (UseAppUpdatePage && UseSettingsPage)
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
