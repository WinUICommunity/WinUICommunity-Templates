using System.Collections.Generic;

namespace WinUICommunity_VS_Templates.Shell
{
    public class Library
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public bool CheckBeforeInsert { get; set; }
        public bool SkipStarVersion { get; set; }

        public Library(string name, string version, bool checkBeforeInsert = false, bool skipStarVersion = false)
        {
            Name = name;
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
                new Library("CommunityToolkit.HighPerformance", "8.2.1"),
                new Library("CommunityToolkit.Common", "8.2.1"),
                new Library("CommunityToolkit.WinUI.Behaviors", "8.0.230907"),
                new Library("CommunityToolkit.WinUI.Extensions", "8.0.230907"),
                new Library("CommunityToolkit.WinUI.Helpers", "8.0.230907"),
                new Library("CommunityToolkit.WinUI.Triggers", "8.0.230907"),
                new Library("CommunityToolkit.WinUI.Converters", "8.0.230907"),
                new Library("CommunityToolkit.WinUI.Animations", "8.0.230907"),
                new Library("CommunityToolkit.WinUI.Media", "8.0.230907"),
                new Library("CommunityToolkit.WinUI.Collections", "8.0.230907"),
                new Library("CommunityToolkit.WinUI.Lottie", "8.0.0-rc"),
                new Library("CommunityToolkit.WinUI.Controls.Segmented", "8.0.230907"),
                new Library("CommunityToolkit.WinUI.Controls.Primitives", "8.0.230907"),
                new Library("CommunityToolkit.WinUI.Controls.Sizers", "8.0.230907"),
                new Library("CommunityToolkit.WinUI.Controls.HeaderedControls", "8.0.230907"),
                new Library("CommunityToolkit.WinUI.Controls.RangeSelector", "8.0.230907"),
                new Library("CommunityToolkit.WinUI.Controls.ImageCropper", "8.0.230907"),
                new Library("CommunityToolkit.WinUI.Controls.RichSuggestBox", "8.0.230907"),
                new Library("CommunityToolkit.WinUI.Controls.RadialGauge", "8.0.230907"),
                new Library("CommunityToolkit.WinUI.Controls.CameraPreview", "8.0.230907"),
                new Library("CommunityToolkit.WinUI.Controls.TokenizingTextBox", "8.0.230907")
            };
            return list;
        }

        public static List<Library> InitEFCore()
        {
            List<Library> list = new()
            {
                new Library("Microsoft.EntityFrameworkCore", "7.0.12"),
                new Library("Microsoft.EntityFrameworkCore.Sqlite", "7.0.12"),
                new Library("Microsoft.EntityFrameworkCore.SqlServer", "7.0.12"),
                new Library("Microsoft.EntityFrameworkCore.Cosmos", "7.0.12"),
                new Library("Microsoft.EntityFrameworkCore.InMemory", "7.0.12"),
                new Library("Microsoft.EntityFrameworkCore.Relational", "7.0.12"),
                new Library("Microsoft.EntityFrameworkCore.Abstractions", "7.0.12"),
                new Library("Microsoft.EntityFrameworkCore.Analyzers", "7.0.12"),
                new Library("Microsoft.EntityFrameworkCore.Design", "7.0.12"),
                new Library("Microsoft.EntityFrameworkCore.Proxies", "7.0.12"),
                new Library("Microsoft.EntityFrameworkCore.Tools", "7.0.12")
            };
            return list;
        }

        public static List<Library> InitUseful()
        {
            List<Library> list = new()
            {
                new Library("Newtonsoft.Json", "13.0.3"),
                new Library("HtmlAgilityPack", "1.11.54"),
                new Library("Downloader", "3.0.6"),
                new Library("Microsoft.Win32.Registry", "5.0.0"),
                new Library("YamlDotNet", "13.7.0"),
                new Library("System.Management", "7.0.2"),
                new Library("SharpCompress", "0.34.1"),
                new Library("RestSharp", "110.2.0"),
                new Library("Vanara.Windows.Shell", "3.4.16"),
                new Library("protobuf-net", "3.2.26"),
                new Library("protobuf-net.Core", "3.2.26"),
                new Library("Humanizer.Core", "2.14.1"),
                new Library("Microsoft.AppCenter", "5.0.3"),
                new Library("LiveChartsCore.SkiaSharpView.WinUI", "2.0.0-rc1.2", false, true),
            };
            return list;
        }

        public static List<Library> InitWinUICommunity()
        {
            List<Library> list = new()
            {
                new Library("WinUICommunity.ContextMenuExtensions", "5.3.0")
            };
            return list;
        }

        public static List<Library> InitLog()
        {
            List<Library> list = new()
            {
                new Library("Serilog", "3.0.1"),
                new Library("Serilog.Sinks.File", "5.0.0"),
                new Library("Serilog.Sinks.Debug", "2.0.0"),
                new Library("Serilog.Sinks.Console", "4.1.0"),
                new Library("log4net", "2.0.15"),
                new Library("NLog", "5.2.4")
            };
            return list;
        }

        public static List<Library> InitMVVM()
        {
            List<Library> list = new()
            {
                new Library("CommunityToolkit.Mvvm", "8.2.1", true),
                new Library("Microsoft.Xaml.Behaviors.WinUI.Managed", "2.0.9", true),
                new Library("Microsoft.Extensions.Hosting", "7.0.1"),
                new Library("Microsoft.Extensions.DependencyInjection", "7.0.0", true),
                new Library("Microsoft.Extensions.Logging", "7.0.0"),
                new Library("Microsoft.Extensions.Configuration", "7.0.0")
            };
            return list;
        }
    }
}
