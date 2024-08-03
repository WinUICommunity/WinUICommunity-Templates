namespace $safeprojectname$.Common;

public static class Constants
{
    public static readonly string AppName = ProcessInfoHelper.GetProductNameAndVersion();
    public static readonly string RootDirectoryPath = Path.Combine(PathHelper.GetAppDataFolderPath(), AppName);$SerilogDirectoryPath$$SerilogFilePath$$AppConfigFilePath$
}
