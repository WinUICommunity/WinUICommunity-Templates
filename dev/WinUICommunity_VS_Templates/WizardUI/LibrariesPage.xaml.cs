using System.Collections.Generic;
using System.Windows.Controls;
using WinUICommunity_VS_Templates.WizardUI;

namespace WinUICommunity_VS_Templates
{
    public partial class LibrariesPage : Page
    {
        public static LibrariesPage Instance { get; private set; }
        public Dictionary<string, PackageRefrence> LibraryDic;
        public LibrariesPage()
        {
            InitializeComponent();
            Instance = this;
            LibraryDic = new();

            CreateBoxes();
        }

        public void CreateBoxes(List<Library> libraries, Panel panel)
        {
            foreach (var lib in libraries)
            {
                string libVersion = lib.Version;
                string libVersion2 = lib.Version;
                if (WizardConfig.UseAlwaysLatestVersion && !lib.SkipStarVersion)
                {
                    libVersion = "*";
                    libVersion2 = "Latest Stable";
                }

                var option = new LibraryOptionUC
                {
                    Title = $"{lib.Name} - {libVersion2}"
                };

                option.Checked += (s, e) =>
                {

                    LibraryDic.Add(lib.Name, new PackageRefrence($"""    <PackageReference Include="{lib.Name}" Version="{libVersion}" />""", lib.CheckBeforeInsert, lib.SkipStarVersion));
                };

                option.Unchecked += (s, e) =>
                {
                    LibraryDic.Remove(lib.Name);
                };

                panel.Children.Add(option);
            }
        }

        public void CreateBoxes()
        {
            if (WinUICommunityPanel != null)
            {
                LibraryDic = new();
                WinUICommunityPanel.Children.Clear();
                GeneralPanel.Children.Clear();
                EFCorePanel.Children.Clear();
                CommunityToolkitPanel.Children.Clear();
                MVVMPanel.Children.Clear();
                LogPanel.Children.Clear();

                CreateBoxes(PreDefinedLibrary.InitWinUICommunity(), WinUICommunityPanel);
                CreateBoxes(PreDefinedLibrary.InitUseful(), GeneralPanel);
                CreateBoxes(PreDefinedLibrary.InitEFCore(), EFCorePanel);
                CreateBoxes(PreDefinedLibrary.InitCommunityToolkit(), CommunityToolkitPanel);
                CreateBoxes(PreDefinedLibrary.InitMVVM(), MVVMPanel);
                CreateBoxes(PreDefinedLibrary.InitLog(), LogPanel);
            }
        }
    }
}
