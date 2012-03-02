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

        public static IEnumerable<EstablishLinkParams> BuildParamsForMatchingFiles(BuildAction buildAction, string pathToFiles, string searchPattern)
        {
            return from f in Directory.GetFiles(pathToFiles, searchPattern, SearchOption.AllDirectories)
                   select new EstablishLinkParams(buildAction, f, f.Replace(pathToFiles, ""));
        }
    }
}