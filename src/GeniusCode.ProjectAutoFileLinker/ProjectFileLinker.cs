using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Build.Evaluation;

namespace GeniusCode.Toolkit.ProjectAutoFileLinker
{
    public class ProjectFileLinker : IProjectFileLinker
    {
        public void LinkToFile(Project project, BuildAction buildAction, string includeValue, string projectTargetPath)
        {
            if(projectTargetPath.StartsWith("\\"))
                throw new Exception("project target path cannot begin with a backslash");

            var matchingProjectItemByTargetPath = (from t in project.Items
                                                   where
                                                       t.HasMetadata("Link") &&
                                                       t.GetMetadataValue("Link") == projectTargetPath
                                                   select t).SingleOrDefault();

            if (matchingProjectItemByTargetPath != null) 
                project.RemoveItem(matchingProjectItemByTargetPath);

            var buildActionName = Enum.GetName(typeof(BuildAction), buildAction);

            project.AddItem(buildActionName, includeValue,
                            new[] {new KeyValuePair<string, string>("Link", projectTargetPath)});
        }


    }
}