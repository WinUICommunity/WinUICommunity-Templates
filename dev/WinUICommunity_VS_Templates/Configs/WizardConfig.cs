using System.Collections.Generic;

namespace WinUICommunity_VS_Templates.WizardUI
{
    public static class WizardConfig
    {
        public static readonly string SolutionFolderNameDefault = "Solution Items";

        public static Dictionary<string, PackageRefrence> LibraryDic = new Dictionary<string, PackageRefrence>();
        public static Dictionary<string, string> CSProjectElements = new Dictionary<string, string>();

        public static string DotNetVersion = "net8.0";
        public static string TargetFrameworkVersion = "22621";
        public static string Platforms = "x86;x64;ARM64";
        public static string RuntimeIdentifiers = "win-x86;win-x64;win-arm64";
        public static string SolutionFolderName = "Solution Items";
        public static bool UseGithubWorkflowFile;
        public static bool UseEditorConfigFile;
        public static bool UseXamlStylerFile;
        public static bool IsUnPackagedMode;
        public static bool IsBlank;
        public static bool HasPages;
        public static bool UseJsonSettings;
        public static bool UseDynamicLocalization;
        public static bool UseSolutionFolder;
        public static bool UseHomeLandingPage;
        public static bool UseSettingsPage;
        public static bool UseGeneralSettingPage;
        public static bool UseThemeSettingPage;
        public static bool UseAppUpdatePage;
        public static bool UseAboutPage;
        public static bool UseAlwaysLatestVersion;
        public static bool UseDeveloperModeSetting;
        public static bool UseColorsDic;
        public static bool UseStylesDic;
        public static bool UseConvertersDic;
        public static bool UseFontsDic;
    }
}
