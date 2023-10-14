namespace WinUICommunity_VS_Templates.Shell
{
    public static class SettingsCardOptions
    {
        public static readonly string SettingsCardCommentCode =
"""
<!-- <wuc:SettingsCard x:Name="MySetting"
                              Click="OnSettingCard_Click"
                              Description="Your Description"
                              Header="Your Header"
                              HeaderIcon="{wuc:BitmapIcon Source=Assets/Fluent/icon.png}"
                              IsClickEnabled="True"
                              Tag="MySettingPage" /> -->
""";

        public static readonly string SettingsCardMVVMCommentCode =
"""
<!-- <wuc:SettingsCard x:Name="MySetting"
                              Command="{x:Bind ViewModel.GoToSettingPageCommand}"
                              CommandParameter="{Binding ElementName=MySetting}"
                              Description="Your Description"
                              Header="Your Header"
                              HeaderIcon="{wuc:BitmapIcon Source=Assets/Fluent/icon.png}"
                              IsClickEnabled="True"
                              Tag="MySettingPage" /> -->
""";
        public static readonly string AboutSettingCode =
"""
<wuc:SettingsCard x:Name="AboutSetting"
                              Click="OnSettingCard_Click"
                              Description="About $safeprojectname$ and Developer"
                              Header="About us"
                              HeaderIcon="{wuc:BitmapIcon Source=Assets/Fluent/info.png}"
                              IsClickEnabled="True"
                              Tag="AboutUsSettingPage" />
""";
        public static readonly string AboutSettingMVVMCode =
"""
<wuc:SettingsCard x:Name="AboutSetting"
                              Command="{x:Bind ViewModel.GoToSettingPageCommand}"
                              CommandParameter="{Binding ElementName=AboutSetting}"
                              Description="About App and Developer"
                              Header="About us"
                              HeaderIcon="{wuc:BitmapIcon Source=Assets/Fluent/info.png}"
                              IsClickEnabled="True"
                              Tag="AboutUsSettingPage" />
""";

        public static readonly string AppUpdateSettingCode =
"""
<wuc:SettingsCard x:Name="AppUpdateSetting"
                              Click="OnSettingCard_Click"
                              Description="Check for Updates"
                              Header="Update App"
                              HeaderIcon="{wuc:BitmapIcon Source=Assets/Fluent/update.png}"
                              IsClickEnabled="True"
                              Tag="AppUpdateSettingPage" />
""";

        public static readonly string AppUpdateSettingMVVMCode =
"""
<wuc:SettingsCard x:Name="AppUpdateSetting"
                              Command="{x:Bind ViewModel.GoToSettingPageCommand}"
                              CommandParameter="{Binding ElementName=AppUpdateSetting}"
                              Description="Check for Updates"
                              Header="Update App"
                              HeaderIcon="{wuc:BitmapIcon Source=Assets/Fluent/update.png}"
                              IsClickEnabled="True"
                              Tag="AppUpdateSettingPage" />
""";

        public static readonly string GeneralSettingCode =
"""
<wuc:SettingsCard x:Name="GeneralSetting"
                              Click="OnSettingCard_Click"
                              Description="Change your app Settings"
                              Header="General"
                              HeaderIcon="{wuc:BitmapIcon Source=Assets/Fluent/settings.png}"
                              IsClickEnabled="True"
                              Tag="GeneralSettingPage" />
""";

        public static readonly string GeneralSettingMVVMCode =
"""
<wuc:SettingsCard x:Name="GeneralSetting"
                              Command="{x:Bind ViewModel.GoToSettingPageCommand}"
                              CommandParameter="{Binding ElementName=GeneralSetting}"
                              Description="Change your app Settings"
                              Header="General"
                              HeaderIcon="{wuc:BitmapIcon Source=Assets/Fluent/settings.png}"
                              IsClickEnabled="True"
                              Tag="GeneralSettingPage" />
""";

        public static readonly string ThemeSettingCode =
"""
<wuc:SettingsCard x:Name="ThemeSetting"
                              Click="OnSettingCard_Click"
                              Description="Select your Theme and Material"
                              Header="Appearance &amp; behavior"
                              HeaderIcon="{wuc:BitmapIcon Source=Assets/Fluent/theme.png}"
                              IsClickEnabled="True"
                              Tag="ThemeSettingPage" />
""";

        public static readonly string ThemeSettingMVVMCode =
"""
<wuc:SettingsCard x:Name="ThemeSetting"
                              Command="{x:Bind ViewModel.GoToSettingPageCommand}"
                              CommandParameter="{Binding ElementName=ThemeSetting}"
                              Description="Select your Theme and Material"
                              Header="Appearance &amp; behavior"
                              HeaderIcon="{wuc:BitmapIcon Source=Assets/Fluent/theme.png}"
                              IsClickEnabled="True"
                              Tag="ThemeSettingPage" />
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
                <ToggleSwitch IsOn="{x:Bind local:AppHelper.Settings.UseDeveloperMode, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}" />
                <wuc:SettingsExpander.ItemsHeader>
                    <HyperlinkButton HorizontalAlignment="Stretch"
                                     HorizontalContentAlignment="Left"
                                     Click="NavigateToLogPath_Click"
                                     Content="{x:Bind local:Constants.LogDirectoryPath}" />
                </wuc:SettingsExpander.ItemsHeader>
            </wuc:SettingsExpander>
""";
        public static readonly string GoToLogPathEvent =
""""
        private async void NavigateToLogPath_Click(object sender, RoutedEventArgs e)
        {
            string folderPath = (sender as HyperlinkButton).Content.ToString();
            Windows.Storage.StorageFolder folder = await Windows.Storage.StorageFolder.GetFolderFromPathAsync(folderPath);
            await Windows.System.Launcher.LaunchFolderAsync(folder);
        }
"""";
    }
}
