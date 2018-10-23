using System.IO;
using GitTfs.VsCommon;
using Microsoft.TeamFoundation.VersionControl.Client;
using StructureMap;

namespace GitTfs.Vs2015
{
    public class TfsHelper : TfsHelperVs2012Base
    {
        protected override string TfsVersionString { get { return "14.0"; } }

        public TfsHelper(TfsApiBridge bridge, IContainer container)
            : base(bridge, container)
        { }

        protected override string GetDialogAssemblyPath()
        {
            var tfsExtensionsFolder = TryGetUserRegStringStartingWithName(@"Software\Microsoft\VisualStudio\14.0\ExtensionManager\EnabledExtensions", "Microsoft.VisualStudio.TeamFoundation.TeamExplorer.Extensions");
            return Path.Combine(tfsExtensionsFolder, DialogAssemblyName + ".dll");
        }

        protected override Workspace CreateWorkSpace(string workSpaceName)
        {
            System.Diagnostics.Debug.WriteLine("Initialize the WorkSpace for 2015 in Server type");
            //Create directly from Server
            return VersionControl.CreateWorkspace(new CreateWorkspaceParameters(workSpaceName)
                                { Location = Microsoft.TeamFoundation.VersionControl.Common.WorkspaceLocation.Server });
        }
    }
}
