using System.Collections.Generic;

namespace WinUICommunity_VS_Templates
{
    public class Library
    {
        public string Name { get; set; }
        public string Net6Version { get; set; }
        public string Net7Version { get; set; }
        public string Version { get; set; }
        public bool CheckBeforeInsert { get; set; }
        public bool SkipStarVersion { get; set; }

        public Library(string name, string version, string net7Version, string net6Version, bool checkBeforeInsert = false, bool skipStarVersion = false)
        {
            Name = name;
            Net6Version = net6Version;
            Net7Version = net7Version;
            Version = version;
            CheckBeforeInsert = checkBeforeInsert;
            SkipStarVersion = skipStarVersion;
        }

        public Library(string name, string version, bool checkBeforeInsert = false, bool skipStarVersion = false)
        {
            Name = name;
            Net6Version = null;
            Net7Version = null;
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
                new Library("CommunityToolkit.WinUI.Lottie", "8.0.0-rc", false, true),
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
                new Library("Microsoft.EntityFrameworkCore", "8.0.1", "7.0.14", "6.0.25"),
                new Library("Microsoft.EntityFrameworkCore.Sqlite", "8.0.1", "7.0.14", "6.0.25"),
                new Library("Microsoft.EntityFrameworkCore.SqlServer", "8.0.1", "7.0.14", "6.0.25"),
                new Library("Microsoft.EntityFrameworkCore.Cosmos", "8.0.1", "7.0.14", "6.0.25"),
                new Library("Microsoft.EntityFrameworkCore.InMemory", "8.0.1", "7.0.14", "6.0.25"),
                new Library("Microsoft.EntityFrameworkCore.Relational", "8.0.1", "7.0.14", "6.0.25"),
                new Library("Microsoft.EntityFrameworkCore.Abstractions", "8.0.1", "7.0.14", "6.0.25"),
                new Library("Microsoft.EntityFrameworkCore.Analyzers", "8.0.1", "7.0.14", "6.0.25"),
                new Library("Microsoft.EntityFrameworkCore.Design", "8.0.1", "7.0.14", "6.0.25"),
                new Library("Microsoft.EntityFrameworkCore.Proxies", "8.0.1", "7.0.14", "6.0.25"),
                new Library("Microsoft.EntityFrameworkCore.Tools", "8.0.1", "7.0.14", "6.0.25")
            };
            return list;
        }

        public static List<Library> InitUseful()
        {
            List<Library> list = new()
            {
                new Library("Microsoft.Windows.CsWinRT", "2.0.4"),
                new Library("Microsoft.Windows.CsWin32", "0.3.49-beta", false, true),
                new Library("WinUIEx", "2.3.3"),
                new Library("Microsoft.Graphics.Win2D", "1.0.5.1"),
                new Library("Newtonsoft.Json", "13.0.3"),
                new Library("HtmlAgilityPack", "1.11.57"),
                new Library("Downloader", "3.0.6"),
                new Library("Microsoft.Win32.Registry", "5.0.0"),
                new Library("YamlDotNet", "13.7.1"),
                new Library("System.Drawing.Common", "8.0.1", "7.0.0", "6.0.0"),
                new Library("System.Management", "8.0.0", "7.0.2", "6.0.2"),
                new Library("SharpCompress", "0.36.0"),
                new Library("RestSharp", "110.2.0"),
                new Library("Vanara.Windows.Shell", "3.4.17"),
                new Library("protobuf-net", "3.2.30"),
                new Library("protobuf-net.Core", "3.2.30"),
                new Library("Humanizer.Core", "2.14.1"),
                new Library("Microsoft.AppCenter", "5.0.3"),
                new Library("LiveChartsCore.SkiaSharpView.WinUI", "2.0.0-rc2.1", false, true),
            };
            return list;
        }

        public static List<Library> InitWinUICommunity()
        {
            List<Library> list = new()
            {
                new Library("WinUICommunity.Win2D", "6.3.0"),
                new Library("WinUICommunity.Core", "6.3.0"),
                new Library("WinUICommunity.Components", "6.3.0"),
                new Library("WinUICommunity.LandingPages", "6.3.0"),
                new Library("WinUICommunity.ContextMenuExtensions", "6.3.0")
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
                new Library("log4net", "2.0.15"),
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
                new Library("Microsoft.Extensions.Hosting", "8.0.0", "7.0.1", "6.0.1"),
                new Library("Microsoft.Extensions.DependencyInjection", "8.0.0", "7.0.0", "6.0.1", true),
                new Library("Microsoft.Extensions.Logging", "8.0.0", "7.0.0", "6.0.0"),
                new Library("Microsoft.Extensions.Configuration", "8.0.0", "7.0.0", "6.0.1")
            };
            return list;
        }
    }
}
