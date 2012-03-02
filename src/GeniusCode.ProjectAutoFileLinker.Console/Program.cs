using System;
using System.Linq;
using GeniusCode.Components.Console;
using GeniusCode.Components.Console.Support;
using Microsoft.Build.Evaluation;

namespace GeniusCode.Toolkit.ProjectAutoFileLinker
{
    class Program
    {
        static void Main(string[] args)
        {

            var optionSet = new RequiredValuesOptionSet();
            var pathToProjectFile = optionSet.AddRequiredVariable<string>("p", "Path to project file");
            var pathToFiles = optionSet.AddRequiredVariable<string>("i", "path for files, must end in trailing slash");
            var searchPattern = optionSet.AddRequiredVariable<string>("sp", "search pattern to use");
            var buildType = optionSet.AddVariable<string>("bt", "buildtype to use.  Values are EmbeddedResource, Content, Compile, and None");
            var structureInVS = optionSet.AddVariable<string>("s", "structure inside of VS.  Must end with a backslash. eg: REPORTS\\");


            var helper = new ConsoleManager(optionSet, "Include Files As Links");

            var canProceed = helper.PerformCanProceed(Console.Out, args);

            if (canProceed)
            {
                Console.WriteLine("Loading project file at {0}", pathToProjectFile.Value);
                var modifier = new ProjectModifierFacade(new Project(pathToProjectFile.Value));

                BuildAction compileAction;

                switch (buildType.Value.ToUpper())
                {
                    case "EMBEDDEDRESOURCE":
                        compileAction = BuildAction.EmbeddedResource;
                        break;
                    case "CONTENT":
                        compileAction = BuildAction.Content;
                        break;
                    case "NONE":
                        compileAction = BuildAction.None;
                        break;
                    case "COMPILE":
                        compileAction = BuildAction.Compile;
                            break;
                    default:
                        throw new NotImplementedException(String.Format("{0} is not supported.", buildType.Value));
                }


                Console.WriteLine("Searching for files");
                var myParams = EstablishLinkParams.BuildParamsForMatchingFiles(compileAction, pathToFiles.Value,
                                                                           searchPattern.Value).ToList();

                Console.WriteLine("{0} files found to link");
                myParams.ForEach(modifier.EstablishLinkToFile);
                Console.WriteLine("Saving project file");
                modifier.Project.Save(pathToProjectFile.Value);
                Console.WriteLine("Complete");
            }
        }
    }
}
