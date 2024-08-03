namespace $safeprojectname$.Common;

public static class Constants
{
    public static readonly string AppName = ProcessInfoHelper.GetProductNameAndVersion();
    public static readonly string RootDirectoryPath = Path.Combine(PathHelper.GetApplicationDataFolderPath(), AppName);$SerilogDirectoryPath$$SerilogFilePath$$AppConfigFilePath$
}
