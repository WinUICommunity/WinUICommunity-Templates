using System.IO;

using EnvDTE;

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

        public NormalizeCSProjFile(Project project, bool useDynamicLocalization)
        {
            var csprojFileContent = File.ReadAllText(project.FullName);
            if (useDynamicLocalization)
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
