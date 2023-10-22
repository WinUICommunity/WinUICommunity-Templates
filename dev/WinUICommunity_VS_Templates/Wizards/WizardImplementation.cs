using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using EnvDTE;

using EnvDTE80;
using HandyControl.Tools.Extension;
using Microsoft.VisualStudio.TemplateWizard;
using WinUICommunity_VS_Templates.Options;
using WinUICommunity_VS_Templates.Shell;

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
        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, bool isMVVMTemplate = false, bool hasNavigationView = false)
        {
            _dte = automationObject as _DTE;

            _shouldAddProjectItem = false;

            var inputForm = new Wizard();
            var result = inputForm.ShowDialog();
            if (result.HasValue && result.Value)
            {
                _shouldAddProjectItem = true;

                string wasdkVersion = "1.4.231008000";
                string wasdkBuildToolsVersion = "10.0.22621.756";
                string winUICommunityComponentsVersion = "5.3.1";
                string winUICommunityCoreVersion = "5.3.1";
                string winUICommunityLandingPagesVersion = "5.3.1";
                string communityToolkitMvvmVersion = "8.2.1";
                string dependencyInjectionVersion = "7.0.0";
                string winUIManagedVersion = "2.0.9";

                if (inputForm.UseAlwaysLatestVersion)
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

                // Add Extra Libs
                var libs = inputForm.LibraryDic;
                StringBuilder outputBuilder = new StringBuilder();
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

                new AppCenterOption().ConfigAppCenter(UseAppCenter, replacementsDictionary);

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

                if (DotNetVersion.Equals("net8.0"))
                {
                    replacementsDictionary.Add("$Net8RidGraph$", Environment.NewLine + "    <UseRidGraph>true</UseRidGraph>");
                }
                else
                {
                    replacementsDictionary.Add("$Net8RidGraph$", "");
                }

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
                UseDeveloperModeSetting = inputForm.AddDeveloperModeSetting;
                UseColorsDic = inputForm.AddColorsDic;
                UseStylesDic = inputForm.AddStylesDic;
                UseConvertersDic = inputForm.AddConvertersDic;
                UseFontsDic = inputForm.AddFontsDic;

                if (hasNavigationView)
                {
                    new ColorsDicOption().ConfigColorsDic(replacementsDictionary, UseHomeLandingPage);
                }

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
                }
                else
                {
                    replacementsDictionary.Add("$AppConfigFilePath$", "");
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
