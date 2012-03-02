using System.Linq;
using FluentAssertions;
using Microsoft.Build.Evaluation;
using NUnit.Framework;

namespace GeniusCode.Toolkit.ProjectAutoFileLinker.Tests
{
    [TestFixture]
    public class Modifier_tests
    {
        [Test]
        public void Should_add_files_to_project_as_embedded_resource()
        {
            const string pathToFiles = @"C:\Temp";
            const string searchPattern = "*.xml";
            IProjectModifierFacade modifier =  new ProjectModifierFacade(new Project());
            var args = EstablishLinkParams.BuildParamsForMatchingFiles(BuildAction.EmbeddedResource, pathToFiles, searchPattern).ToList();

            args.ToList().ForEach(modifier.EstablishLinkToFile);

            var items =  modifier.Project.GetItems("EmbeddedResource").Where(i => i.UnevaluatedInclude.EndsWith(".xml")).OrderBy(c=> c.UnevaluatedInclude).ToList();        
            items.Count.Should().Be(args.Count);      
        }
    }
}
