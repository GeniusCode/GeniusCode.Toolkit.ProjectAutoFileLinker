using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GeniusCode.Toolkit.ProjectAutoFileLinker
{
    public class EstablishLinkParams
    {
        private readonly BuildAction _buildAction;
        private readonly string _filenameToLink;
        private readonly string _projectTargetPath;

        public EstablishLinkParams(BuildAction buildAction, string filenameToLink, string projectTargetPath)
        {
            _buildAction = buildAction;
            _filenameToLink = filenameToLink;
            _projectTargetPath = projectTargetPath;
        }

        public BuildAction BuildAction
        {
            get { return _buildAction; }
        }

        public string FilenameToLink
        {
            get { return _filenameToLink; }
        }

        public string ProjectTargetPath
        {
            get { return _projectTargetPath; }
        }

        public static IEnumerable<EstablishLinkParams> BuildParamsForMatchingFiles(BuildAction buildAction, string pathToFiles, string searchPattern,string rootProjectTarget = "")
        {
            if (!pathToFiles.EndsWith("\\"))
                throw new Exception("Path to files must end in a trailing backslash");

            if (!String.IsNullOrWhiteSpace(rootProjectTarget))
            {
                if (rootProjectTarget.StartsWith("\\"))
                    throw new Exception("Root target paths must not start with a backslash");

                if (!rootProjectTarget.EndsWith("\\"))
                    throw new Exception("Root target paths must end start with a backslash");
            }

            return from f in Directory.GetFiles(pathToFiles, searchPattern, SearchOption.AllDirectories)
                   let newPath = rootProjectTarget + f.Replace(pathToFiles, "")
                   select new EstablishLinkParams(buildAction, f, newPath);
        }
    }
}