using System;
using System.IO;
using System.Linq;

namespace GeniusCode.Toolkit.ProjectAutoFileLinker
{
    public class RelativeFileMapper : IRelativeFileMapper
    {
        public string ConvertPathToRelative(string basePath, string secondaryPath)
        {
            if (basePath == null) throw new ArgumentNullException("basePath");
            if (secondaryPath == null) throw new ArgumentNullException("secondaryPath");

            var pathToProjectUri = new Uri(basePath);
            var pathToResourceUri = new Uri(secondaryPath);
            var relativePath = pathToProjectUri.MakeRelativeUri(pathToResourceUri);
            var path = Uri.UnescapeDataString(relativePath.OriginalString);
            return path;
        }
    }

    
}