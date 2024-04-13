using System.Collections.Generic;

namespace WinUICommunity_VS_Templates
{
    public class Library
    {
        public string Name { get; set; }
        public string Net9Version { get; set; }
        public string Version { get; set; }
        public bool CheckBeforeInsert { get; set; }
        public bool SkipStarVersion { get; set; }

        public Library(string name, string version, string net9Version, bool checkBeforeInsert = false, bool skipStarVersion = false)
        {
            Name = name;
            Net9Version = net9Version;
            Version = version;
            CheckBeforeInsert = checkBeforeInsert;
            SkipStarVersion = skipStarVersion;
        }

        public Library(string name, string version, bool checkBeforeInsert = false, bool skipStarVersion = false)
        {
            Name = name;
            Net9Version = null;
            Version = version;
            CheckBeforeInsert = checkBeforeInsert;
            SkipStarVersion = skipStarVersion;
        }
    }

    public class PackageRefrence
    {
        public string Package { get; set; }
        public bool CheckBeforeInsert { get; set; }
        public bool SkipStarVersion { get; set; }
        public PackageRefrence(string package, bool checkBeforeInsert, bool skipStarVersion)
        {
            Package = package;
            CheckBeforeInsert = checkBeforeInsert;
            SkipStarVersion = skipStarVersion;
        }
    }

    public static class PreDefinedLibrary
    {
        public static List<Library> InitCommunityToolkit()
        {
            List<Library> list = new()
            {
                new Library("CommunityToolkit.HighPerformance", "8.2.2"),
                new Library("CommunityToolkit.Common", "8.2.2"),
                new Library("CommunityToolkit.WinUI.Behaviors", "8.0.240109"),
                new Library("CommunityToolkit.WinUI.Extensions", "8.0.240109"),
                new Library("CommunityToolkit.WinUI.Helpers", "8.0.240109"),
                new Library("CommunityToolkit.WinUI.Triggers", "8.0.240109"),
                new Library("CommunityToolkit.WinUI.Converters", "8.0.240109"),
                new Library("CommunityToolkit.WinUI.Animations", "8.0.240109"),
                new Library("CommunityToolkit.WinUI.Media", "8.0.240109"),
                new Library("CommunityToolkit.WinUI.Collections", "8.0.240109"),
                new Library("CommunityToolkit.WinUI.Lottie", "8.0.280224"),
                new Library("CommunityToolkit.WinUI.Controls.Segmented", "8.0.240109"),
                new Library("CommunityToolkit.WinUI.Controls.Primitives", "8.0.240109"),
                new Library("CommunityToolkit.WinUI.Controls.Sizers", "8.0.240109"),
                new Library("CommunityToolkit.WinUI.Controls.HeaderedControls", "8.0.240109"),
                new Library("CommunityToolkit.WinUI.Controls.RangeSelector", "8.0.240109"),
                new Library("CommunityToolkit.WinUI.Controls.ImageCropper", "8.0.240109"),
                new Library("CommunityToolkit.WinUI.Controls.RichSuggestBox", "8.0.240109"),
                new Library("CommunityToolkit.WinUI.Controls.RadialGauge", "8.0.240109"),
                new Library("CommunityToolkit.WinUI.Controls.CameraPreview", "8.0.240109"),
                new Library("CommunityToolkit.WinUI.Controls.TokenizingTextBox", "8.0.240109")
            };
            return list;
        }

        public static List<Library> InitEFCore()
        {
            List<Library> list = new()
            {
                new Library("Microsoft.EntityFrameworkCore", "8.0.4"),
                new Library("Microsoft.EntityFrameworkCore.Sqlite", "8.0.4"),
                new Library("Microsoft.EntityFrameworkCore.SqlServer", "8.0.4"),
                new Library("Microsoft.EntityFrameworkCore.Cosmos", "8.0.4"),
                new Library("Microsoft.EntityFrameworkCore.InMemory", "8.0.4"),
                new Library("Microsoft.EntityFrameworkCore.Relational", "8.0.4"),
                new Library("Microsoft.EntityFrameworkCore.Abstractions", "8.0.4"),
                new Library("Microsoft.EntityFrameworkCore.Analyzers", "8.0.4"),
                new Library("Microsoft.EntityFrameworkCore.Design", "8.0.4"),
                new Library("Microsoft.EntityFrameworkCore.Proxies", "8.0.4"),
                new Library("Microsoft.EntityFrameworkCore.Tools", "8.0.4")
            };
            return list;
        }

        public static List<Library> InitUseful()
        {
            List<Library> list = new()
            {
                new Library("TenMica", "1.0.0-beta", false, true),
                new Library("WinUI.TableView", "1.0.0-preview4", false, true),
                new Library("Microsoft.Windows.CsWinRT", "2.0.7"),
                new Library("Microsoft.Windows.CsWin32", "0.3.49-beta", false, true),
                new Library("WinUIEx", "2.3.4"),
                new Library("Microsoft.Graphics.Win2D", "1.2.0"),
                new Library("Microsoft.Graphics.Win2D", "1.0.5.1"),
                new Library("Newtonsoft.Json", "13.0.3"),
                new Library("HtmlAgilityPack", "1.11.59"),
                new Library("Downloader", "3.0.6"),
                new Library("Microsoft.Win32.Registry", "5.0.0"),
                new Library("YamlDotNet", "15.1.2"),
                new Library("System.Drawing.Common", "8.0.4"),
                new Library("System.Management", "8.0.0"),
                new Library("SharpCompress", "0.36.0"),
                new Library("RestSharp", "110.2.0"),
                new Library("Vanara.Windows.Shell", "4.0.0"),
                new Library("protobuf-net", "3.2.30"),
                new Library("protobuf-net.Core", "3.2.30"),
                new Library("Humanizer.Core", "2.14.1"),
                new Library("LiveChartsCore.SkiaSharpView.WinUI", "2.0.0-rc2.1", false, true),
            };
            return list;
        }

        public static List<Library> InitWinUICommunity()
        {
            List<Library> list = new()
            {
                new Library(Constants.WinUICommunity_Win2D, "6.7.0"),
                new Library(Constants.WinUICommunity_Core, "6.7.0"),
                new Library(Constants.WinUICommunity_Components, "6.7.0"),
                new Library(Constants.WinUICommunity_LandingPages, "6.7.0"),
                new Library(Constants.WinUICommunity_ContextMenuExtensions, "6.7.0")
            };
            return list;
        }

        public static List<Library> InitLog()
        {
            List<Library> list = new()
            {
                new Library("Serilog", "3.1.1"),
                new Library("Serilog.Sinks.File", "5.0.0"),
                new Library("Serilog.Sinks.Debug", "2.0.0"),
                new Library("Serilog.Sinks.Console", "5.0.1"),
                new Library("log4net", "2.0.17"),
                new Library("NLog", "5.2.8")
            };
            return list;
        }

        public static List<Library> InitMVVM()
        {
            List<Library> list = new()
            {
                new Library("CommunityToolkit.Mvvm", "8.2.2", true),
                new Library("Microsoft.Xaml.Behaviors.WinUI.Managed", "2.0.9", true),
                new Library("Microsoft.Extensions.Hosting", "8.0.0"),
                new Library("Microsoft.Extensions.DependencyInjection", "8.0.0", true),
                new Library("Microsoft.Extensions.Logging", "8.0.0"),
                new Library("Microsoft.Extensions.Configuration", "8.0.0")
            };
            return list;
        }
    }
}
