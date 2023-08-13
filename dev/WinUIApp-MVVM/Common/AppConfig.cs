using Nucs.JsonSettings;
using Nucs.JsonSettings.Modulation;

namespace $safeprojectname$;

public class AppConfig : JsonSettings, IVersionable
{
    [EnforcedVersion("1.0.0.0")]
    public virtual Version Version { get; set; } = new Version(1, 0, 0, 0);

    private static readonly string AppName = "$safeprojectname$-v1.0";
    private static readonly string RootPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), AppName);
    private static readonly string AppConfigPath = Path.Combine(RootPath, "AppConfig.json");

    public override string FileName { get; set; } = AppConfigPath;

    // Docs: https://github.com/Nucs/JsonSettings
}

