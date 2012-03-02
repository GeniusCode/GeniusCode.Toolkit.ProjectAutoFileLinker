using System;
using System.Linq;

namespace GeniusCode.Toolkit.ProjectAutoFileLinker
{
    public class RelativeFileMapper : IRelativeFileMapper
    {
        public string ConvertPathToRelative(string basePath, string secondaryPath)
        {
            var pathToProjectUri = new Uri(basePath);
            var pathToResourceUri = new Uri(secondaryPath);
            var relativePath = pathToProjectUri.MakeRelativeUri(pathToResourceUri);
            var path = Uri.UnescapeDataString(relativePath.OriginalString);
            return path;
        }
    }

    
}