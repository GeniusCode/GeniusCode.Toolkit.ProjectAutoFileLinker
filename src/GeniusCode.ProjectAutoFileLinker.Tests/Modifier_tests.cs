using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using FluentAssertions;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Framework;
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
            IProjectModifierFacade modifier =  new ProjectModifierFacade(new Project());          

            var pathToProjectDirectory = Path.Combine(pathToFiles, Guid.NewGuid().ToString());
            Directory.CreateDirectory(pathToProjectDirectory);
            var pathToProjectFile = pathToProjectDirectory + "\\testproject.csproj";
            modifier.Project.Save(pathToProjectFile);

            var param = new EstablishLinkParams(BuildAction.EmbeddedResource, @"C:\Temp\SampleResponse.xml", @"Docs\SampleResponse.xml");

            modifier.EstablishLinkToFile(param);

            var item = modifier.Project.GetItems("EmbeddedResource").Single(i => i.UnevaluatedInclude.EndsWith(".xml"));

            item.UnevaluatedInclude.Should().Be("../SampleResponse.xml");
            item.GetMetadataValue("Link").Should().Be("Docs\\SampleResponse.xml");
        }
    }
}
