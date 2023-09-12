using System;

namespace WinUICommunity_VS_Templates.Options
{
    public class DynamicLocalizationOption
    {
        string InitializeCode =
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
            })
            .Build();
    }
""";
        public DynamicLocalizationOption(bool useDynamicLocalization, string templatePath)
        {
            string appFileContent = WizardHelper.ReadAppFileContent(templatePath);

            if (useDynamicLocalization)
            {
                appFileContent = appFileContent.Replace("private void InitializeLocalizer { };", $"\n{InitializeCode}");
                appFileContent = appFileContent.Replace("await InitializeLocalizer(\"en-US\");", Environment.NewLine + Environment.NewLine + "        await InitializeLocalizer(\"en-US\");");
            }
            else
            {
                appFileContent = appFileContent.Replace("using Windows.Storage;", "");
                appFileContent = appFileContent.Replace("private static string StringsFolderPath { get; set; } = string.Empty;", "");
                appFileContent = appFileContent.Replace("await InitializeLocalizer(\"en-US\");", "");
                appFileContent = appFileContent.Replace("protected async override void OnLaunched", "protected override void OnLaunched");
                appFileContent = appFileContent.Replace("private void InitializeLocalizer { };", "");
            }

            WizardHelper.SaveAppFileContent(templatePath, appFileContent);
        }
    }
}
