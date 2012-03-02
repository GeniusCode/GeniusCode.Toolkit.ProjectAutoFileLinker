using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace GeniusCode.Toolkit.ProjectAutoFileLinker.Tests
{
    [TestFixture]
    public class FileMapper_tests
    {

        [Test]
        public void Should_map_path_correctly_to_lower_path_when_directory_is_specified()
        {
            const string pathToProject = @"C:\Temp\MyProject\";
            const string pathToMyXml = @"C:\Temp\SampleXml.cs";

            var mapper = new RelativeFileMapper();
            mapper.ConvertPathToRelative(pathToProject, pathToMyXml).Should().Be("../SampleXml.cs");
        }

        [Test]
        public void Should_map_path_correctly_to_lower_path_when_file_is_specified()
        {
            const string pathToProject = @"C:\Temp\MyProject\Proj.csproj";
            const string pathToMyXml = @"C:\Temp\SampleXml.cs";
            
            var mapper = new RelativeFileMapper();
            mapper.ConvertPathToRelative(pathToProject, pathToMyXml).Should().Be("../SampleXml.cs");          
        }

        [Test]
        public void Should_map_path_correctly_to_upper_path()
        {

            const string pathToProject = @"C:\Temp\MyProject\Proj.csproj";
            const string pathToMyXml = @"C:\Temp\MyProject\Files\SampleXml.cs";

            var mapper = new RelativeFileMapper();
            mapper.ConvertPathToRelative(pathToProject, pathToMyXml).Should().Be("Files/SampleXml.cs");
        }
    }
}