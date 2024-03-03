using System;
using System.IO;

using EnvDTE;
using Microsoft.VisualStudio.Shell;
using WinUICommunity_VS_Templates.WizardUI;

namespace WinUICommunity_VS_Templates.Options
{
    public class NormalizeCSProjFile
    {
        string baseReswItemGroupCode = """<ItemGroup Label="DynamicLocalization"/>""";

        string reswItemGroupCode = """
                    <ItemGroup>
                        <Content Include="Strings\**\*.resw">
                          <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
                        </Content>
                      </ItemGroup>
                    """;

        public NormalizeCSProjFile(Project project)
        {
            Run(project);
        }

        private async void Run(Project project)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            var csprojFileContent = File.ReadAllText(project.FullName);
            if (WizardConfig.UseDynamicLocalization)
            {
                csprojFileContent = csprojFileContent.Replace(baseReswItemGroupCode, reswItemGroupCode);
            }
            else
            {
                csprojFileContent = csprojFileContent.Replace(baseReswItemGroupCode, "");
            }
            File.WriteAllText(project.FullName, csprojFileContent);
        }
    }
}
