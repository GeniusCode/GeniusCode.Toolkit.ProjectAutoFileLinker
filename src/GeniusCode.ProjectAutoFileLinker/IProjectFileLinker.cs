using System.Linq;
using Microsoft.Build.Evaluation;

namespace GeniusCode.Toolkit.ProjectAutoFileLinker
{
    public interface IProjectFileLinker
    {
        void LinkToFile(Project project, BuildAction buildAction, string includeValue, string projectTargetPath);
    }
}