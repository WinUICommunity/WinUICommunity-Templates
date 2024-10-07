namespace WinUICommunity_VS_Templates
{
    public static class PredefinedCodes
    {
        public static string Windows11ContextMenuInitializer =
""""
ContextMenuItem menu = new ContextMenuItem
{
    Title = "Open $projectname$ Here",
    Param = @"""{path}""",
    AcceptFileFlag = (int)FileMatchFlagEnum.All,
    AcceptDirectoryFlag = (int)(DirectoryMatchFlagEnum.Directory | DirectoryMatchFlagEnum.Background | DirectoryMatchFlagEnum.Desktop),
    AcceptMultipleFilesFlag = (int)FilesMatchFlagEnum.Each,
    Index = 0,
    Enabled = true,
    Icon = ProcessInfoHelper.GetFileVersionInfo().FileName,
    Exe = "$projectname$.exe"
};

ContextMenuService menuService = new ContextMenuService();
await menuService.SaveAsync(menu);
"""";
        public static string Windows11ContextMenuMVVMInitializer =
""""
var menuService = GetService<ContextMenuService>();
if (menuService != null)
{
    ContextMenuItem menu = new ContextMenuItem
    {
        Title = "Open $projectname$ Here",
        Param = @"""{path}""",
        AcceptFileFlag = (int)FileMatchFlagEnum.All,
        AcceptDirectoryFlag = (int)(DirectoryMatchFlagEnum.Directory | DirectoryMatchFlagEnum.Background | DirectoryMatchFlagEnum.Desktop),
        AcceptMultipleFilesFlag = (int)FilesMatchFlagEnum.Each,
        Index = 0,
        Enabled = true,
        Icon = ProcessInfoHelper.GetFileVersionInfo().FileName,
        Exe = "$projectname$.exe"
    };

    await menuService.SaveAsync(menu);
}
"""";
        
        public static readonly string SettingsCardCommentCode =
"""
<!-- <wuc:SettingsCard Description="Your Description"
                              Header="Your Header"
                              HeaderIcon="{wuc:BitmapIcon Source=Assets/Fluent/icon.png}"
                              IsClickEnabled="True"
                              Tag="$safeprojectname$.Views.MySettingPage" /> -->
""";

        public static readonly string SettingsCardMVVMCommentCode =
"""
<!-- <wuc:SettingsCard Description="Your Description"
                              Header="Your Header"
                              HeaderIcon="{wuc:BitmapIcon Source=Assets/Fluent/icon.png}"
                              IsClickEnabled="True"
                              Tag="$safeprojectname$.Views.MySettingPage" /> -->
""";
        public static readonly string AboutSettingCode =
"""
<wuc:SettingsCard Description="About $safeprojectname$ and Developer"
                              Header="About us"
                              HeaderIcon="{wuc:BitmapIcon Source=Assets/Fluent/info.png}"
                              IsClickEnabled="True"
                              Tag="$safeprojectname$.Views.AboutUsSettingPage" />
""";
        public static readonly string AboutSettingMVVMCode =
"""
<wuc:SettingsCard Description="About $safeprojectname$ and Developer"
                              Header="About us"
                              HeaderIcon="{wuc:BitmapIcon Source=Assets/Fluent/info.png}"
                              IsClickEnabled="True"
                              Tag="$safeprojectname$.Views.AboutUsSettingPage" />
""";

        public static readonly string AppUpdateSettingCode =
"""
<wuc:SettingsCard Description="Check for Updates"
                              Header="Update App"
                              HeaderIcon="{wuc:BitmapIcon Source=Assets/Fluent/update.png}"
                              IsClickEnabled="True"
                              Tag="$safeprojectname$.Views.AppUpdateSettingPage" />
""";

        public static readonly string AppUpdateSettingMVVMCode =
"""
<wuc:SettingsCard Description="Check for Updates"
                              Header="Update App"
                              HeaderIcon="{wuc:BitmapIcon Source=Assets/Fluent/update.png}"
                              IsClickEnabled="True"
                              Tag="$safeprojectname$.Views.AppUpdateSettingPage" />
""";

        public static readonly string GeneralSettingCode =
"""
<wuc:SettingsCard Description="Change your app Settings"
                              Header="General"
                              HeaderIcon="{wuc:BitmapIcon Source=Assets/Fluent/settings.png}"
                              IsClickEnabled="True"
                              Tag="$safeprojectname$.Views.GeneralSettingPage" />
""";

        public static readonly string GeneralSettingMVVMCode =
"""
<wuc:SettingsCard Description="Change your app Settings"
                              Header="General"
                              HeaderIcon="{wuc:BitmapIcon Source=Assets/Fluent/settings.png}"
                              IsClickEnabled="True"
                              Tag="$safeprojectname$.Views.GeneralSettingPage" />
""";

        public static readonly string ThemeSettingCode =
"""
<wuc:SettingsCard Description="Explore the different ways to customize the appearance and behavior of your app. You can change the material, theme, accent, and more options to suit your style and preference."
                              Header="Appearance &amp; behavior"
                              HeaderIcon="{wuc:BitmapIcon Source=Assets/Fluent/theme.png}"
                              IsClickEnabled="True"
                              Tag="$safeprojectname$.Views.ThemeSettingPage" />
""";

        public static readonly string ThemeSettingMVVMCode =
"""
<wuc:SettingsCard Description="Explore the different ways to customize the appearance and behavior of your app. You can change the material, theme, accent, and more options to suit your style and preference."
                              Header="Appearance &amp; behavior"
                              HeaderIcon="{wuc:BitmapIcon Source=Assets/Fluent/theme.png}"
                              IsClickEnabled="True"
                              Tag="$safeprojectname$.Views.ThemeSettingPage" />
""";

        public static readonly string DeveloperModeSettingCode =
"""
            <wuc:SettingsCard Description="By activating this option, if an error or crash occurs, its information will be saved in a file called Log{YYYYMMDD}.txt"
                              Header="Developer Mode (Restart Required)"
                              HeaderIcon="{wuc:BitmapIcon Source=Assets/Fluent/devMode.png}">
                <ToggleSwitch />
            </wuc:SettingsCard>
""";
        public static readonly string DeveloperModeSettingCode2 =
"""
            <wuc:SettingsExpander Description="By activating this option, if an error or crash occurs, its information will be saved in a file called Log{YYYYMMDD}.txt"
                                  Header="Developer Mode (Restart Required)"
                                  HeaderIcon="{wuc:BitmapIcon Source=Assets/Fluent/devMode.png}">
                <ToggleSwitch IsOn="{x:Bind common:AppHelper.Settings.UseDeveloperMode, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}" />
                <wuc:SettingsExpander.ItemsHeader>
                    <HyperlinkButton HorizontalAlignment="Stretch"
                                     HorizontalContentAlignment="Left"
                                     Click="NavigateToLogPath_Click"
                                     Content="{x:Bind common:Constants.LogDirectoryPath}" />
                </wuc:SettingsExpander.ItemsHeader>
            </wuc:SettingsExpander>
""";
        public static readonly string GoToLogPathEvent =
""""
        private async void NavigateToLogPath_Click(object sender, RoutedEventArgs e)
        {
            string folderPath = (sender as HyperlinkButton).Content.ToString();
            if (Directory.Exists(folderPath))
            {
                Windows.Storage.StorageFolder folder = await Windows.Storage.StorageFolder.GetFolderFromPathAsync(folderPath);
                await Windows.System.Launcher.LaunchFolderAsync(folder);
            }
        }
"""";
    }
}
