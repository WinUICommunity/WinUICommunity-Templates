using EnvDTE;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TemplateWizard;
using Microsoft.VisualStudio.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NuGet.VisualStudio;
using System.Xml.Linq;
using WinUICommunity_VS_Templates.Options;

namespace WinUICommunity_VS_Templates
{
    public class NuGetPackageInstaller : IWizard
    {
        private List<string> _packageId;
        private Project _project;
        private IComponentModel _componentModel;
        private IVsNuGetProjectUpdateEvents _nugetProjectUpdateEvents;

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            _packageId = ExtractPackageId(replacementsDictionary);
            _componentModel = (IComponentModel)ServiceProvider.GlobalProvider.GetService(typeof(SComponentModel));
            _nugetProjectUpdateEvents = _componentModel.GetService<IVsNuGetProjectUpdateEvents>();
            _nugetProjectUpdateEvents.SolutionRestoreFinished += OnSolutionRestoreFinished;
        }
        private List<string> ExtractPackageId(Dictionary<string, string> replacementsDictionary)
        {
            if (replacementsDictionary.TryGetValue("$wizarddata$", out string wizardDataXml))
            {
                XDocument xDoc = XDocument.Parse(wizardDataXml);
                XNamespace ns = xDoc.Root.GetDefaultNamespace();
                var packageId = xDoc.Descendants(ns + "package")
                                      .Attributes("id")
                                      .Select(attr => attr.Value)
                                      .ToList();

                if (packageId.Count > 0)
                {
                    return packageId;
                }
            }
            return null;
        }
        public void ProjectFinishedGenerating(Project project)
        {
            _project = project;

            VSDocumentHelper.FormatXmlBasedFile(project.FullName);
        }
        public void BeforeOpeningFile(ProjectItem _)
        {
        }
        public void ProjectItemFinishedGenerating(ProjectItem _)
        {
        }
        public void RunFinished()
        {

        }
        private void OnSolutionRestoreFinished(IReadOnlyList<string> projects)
        {
            // Debouncing prevents multiple rapid executions of 'InstallNuGetPackageAsync'
            // during solution restore.
            _nugetProjectUpdateEvents.SolutionRestoreFinished -= OnSolutionRestoreFinished;
            var joinableTaskFactory = new JoinableTaskFactory(ThreadHelper.JoinableTaskContext);
            joinableTaskFactory.RunAsync(InstallNuGetPackageAsync);

        }
        private Task InstallNuGetPackageAsync()
        {
            if (_packageId.Count == 0)
            {
                string message = "Failed to install the NuGet package. The package ID provided in the template configuration is either missing or invalid. Please ensure the template is correctly configured with a valid package ID.";
                DisplayMessageToUser(message, "Error", OLEMSGICON.OLEMSGICON_CRITICAL);
                LogError(message);
                return Task.CompletedTask;
            }
            IVsPackageInstaller installer = _componentModel.GetService<IVsPackageInstaller>();
            foreach (var item in _packageId)
            {
                try
                {
                    installer.InstallPackage(null, _project, item, "", false);
                }
                catch (Exception ex)
                {
                    string errorMessage = $"Failed to install the {item} package. You can try installing it manually from: https://www.nuget.org/packages/{item}";
                    DisplayMessageToUser(errorMessage, "Installation Error", OLEMSGICON.OLEMSGICON_CRITICAL);

                    string logMessage = $"Failed to install {item} package. Exception details: \n" +
                                        $"Message: {ex.Message}\n" +
                                        $"Source: {ex.Source}\n" +
                                        $"Stack Trace: {ex.StackTrace}\n" +
                                        $"Target Site: {ex.TargetSite}\n";

                    if (ex.InnerException != null)
                    {
                        logMessage += $"Inner Exception Message: {ex.InnerException.Message}\n" +
                                      $"Inner Exception Stack Trace: {ex.InnerException.StackTrace}\n";
                    }
                    LogError(logMessage);
                }
            }

            return Task.CompletedTask;
        }
        private void DisplayMessageToUser(string message, string title, OLEMSGICON icon)
        {
            ThreadHelper.JoinableTaskFactory.Run(async delegate
            {
                await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
                VsShellUtilities.ShowMessageBox(
                    ServiceProvider.GlobalProvider,
                    message,
                    title,
                    icon,
                    OLEMSGBUTTON.OLEMSGBUTTON_OK,
                    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
            });
        }
        private void LogError(string message)
        {
            IVsActivityLog log = ServiceProvider.GlobalProvider.GetService(typeof(SVsActivityLog)) as IVsActivityLog;
            if (log != null)
            {
                log.LogEntry(
                    (UInt32)__ACTIVITYLOG_ENTRYTYPE.ALE_ERROR,
                    "WinUICommunity_VS_Templates",
                    message);
            }
        }
        public bool ShouldAddProjectItem(string _)
        {
            return true;
        }
    }
}
