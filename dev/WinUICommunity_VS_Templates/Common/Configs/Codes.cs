using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinUICommunity_VS_Templates
{
    public static class Codes
    {
       public static string LocalizerInitializeCode =
"""
public static async Task InitializeLocalizer(params string[] languages)
{
    // Initialize a "Strings" folder in the "LocalFolder" for the packaged app.
    if (PackageHelper.IsPackaged)
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
        public static string LocalizerItemGroupCode =
 """
<ItemGroup>
  <Content Include="Strings\**\*.resw">
    <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
  </Content>
</ItemGroup>
""";

        public static string LocalizerActivateCode =
"""
await DynamicLocalizerHelper.InitializeLocalizer("en-US");
""";
    }
}
