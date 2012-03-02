using System.Linq;

namespace GeniusCode.Toolkit.ProjectAutoFileLinker
{
    public interface IRelativeFileMapper
    {
        string ConvertPathToRelative(string basePath, string secondaryPath);
    }
}