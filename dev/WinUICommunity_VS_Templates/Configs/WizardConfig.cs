using System.Collections.Generic;

namespace WinUICommunity_VS_Templates.WizardUI
{
    public static class WizardConfig
    {
        public static Dictionary<string, PackageRefrence> LibraryDic = new Dictionary<string, PackageRefrence>();
        public static Dictionary<string, string> CSProjectElements = new Dictionary<string, string>();

        public static string DotNetVersion = "net8.0";
        public static string TargetFrameworkVersion = "22621";
        public static string Platforms = "x86;x64;ARM64";
        public static string RuntimeIdentifiers = "win-x86;win-x64;win-arm64";
        public static bool UseGithubWorkflow = false;
        public static bool IsUnPackagedMode = false;
        public static bool IsBlank;
        public static bool HasPages;
        public static bool AddJsonSettings;
        public static bool AddDynamicLocalization;
        public static bool AddEditorConfig;
        public static bool AddSolutionFolder;
        public static bool AddHomeLandingPage;
        public static bool AddSettingsPage;
        public static bool AddGeneralSettingPage;
        public static bool AddThemeSettingPage;
        public static bool AddAppUpdatePage;
        public static bool AddAboutPage;
        public static bool AddAccelerateBuilds;
        public static bool UseAlwaysLatestVersion;
        public static bool AddDeveloperModeSetting;
        public static bool AddColorsDic;
        public static bool AddStylesDic;
        public static bool AddConvertersDic;
        public static bool AddFontsDic;
    }
}
