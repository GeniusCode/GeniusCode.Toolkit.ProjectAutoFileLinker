using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace GeniusCode.Toolkit.ProjectAutoFileLinker.Tests
{
    [TestFixture]
    public class ParamBuilder_tests
    {
        [Test]
        public void Should_build_target_path()
        {

            var tempPath = Path.GetTempPath() + Guid.NewGuid() + "\\";
            Directory.CreateDirectory(tempPath);

            File.Create(tempPath + "\\file1.xml");
            File.Create(tempPath + "\\file2.xml");

            Directory.CreateDirectory(tempPath + "\\Directory");
            File.Create(tempPath + "\\Directory\\file3.xml");

            var items = EstablishLinkParams.BuildParamsForMatchingFiles(BuildAction.EmbeddedResource, tempPath, "*.*").ToList();
            items.Count().Should().Be(3);

            items[0].ProjectTargetPath.Should().Be("file1.xml");

            items[2].ProjectTargetPath.Should().Be("Directory\\file3.xml");

        }

        [Test]
        public void Should_be_target_path_when_root_is_supplied()
        {

            var tempPath = Path.GetTempPath() + Guid.NewGuid() + "\\";
            Directory.CreateDirectory(tempPath);

            File.Create(tempPath + "\\file1.xml");
            File.Create(tempPath + "\\file2.xml");

            Directory.CreateDirectory(tempPath + "\\Directory");
            File.Create(tempPath + "\\Directory\\file3.xml");

            var items = EstablishLinkParams.BuildParamsForMatchingFiles(BuildAction.EmbeddedResource, tempPath, "*.*","Reports\\").ToList();
            items.Count().Should().Be(3);

            items[0].ProjectTargetPath.Should().Be("Reports\\file1.xml");

            items[2].ProjectTargetPath.Should().Be("Reports\\Directory\\file3.xml");

        }


    }
}