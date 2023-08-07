using System.Collections.Generic;

using EnvDTE;

using EnvDTE80;

using Microsoft.VisualStudio.TemplateWizard;

namespace WinUICommunity_VS_Templates
{
    public class WizardImplementation : IWizard
    {
        _DTE _dte;
        Solution2 _solution;
        public void BeforeOpeningFile(ProjectItem projectItem)
        {
        }

        public void ProjectFinishedGenerating(Project project)
        {
        }

        public void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {
        }

        public void RunFinished()
        {
            _solution = (Solution2)_dte.Solution;

            var solutionFolder = _solution.AddSolutionFolder("Solution Items");

            //Solution2 soln = (Solution2)_dte.Solution;
            //var vstemplateFileName = soln.GetProjectTemplate("WinUIApp.vstemplate", "CSharp");

            //string folderPath = Path.GetDirectoryName(vstemplateFileName);

            //while (folderPath.Contains("ProjectTemplates"))
            //{
            //    folderPath = Directory.GetParent(folderPath).FullName;
            //}

            //string editorconfigFile = Path.Combine(folderPath, ".editorconfig");

            //if (File.Exists(editorconfigFile))
            //{
            //    solutionFolder.ProjectItems.AddFromFile(editorconfigFile);
            //}
        }

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            _dte = automationObject as _DTE;
        }

        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }
    }
}
