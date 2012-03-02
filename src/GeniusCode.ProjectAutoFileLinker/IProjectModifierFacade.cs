using System.Linq;
using Microsoft.Build.Evaluation;

namespace GeniusCode.Toolkit.ProjectAutoFileLinker
{
    public interface IProjectModifierFacade
    {
        void EstablishLinkToFile(EstablishLinkParams linkParams);
        Project Project { get; }
    }
}