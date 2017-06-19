using System;
using NUnit.Framework;

namespace PathsToTree.Tests
{
    [TestFixture]
    public class PathToTreeConverterTests
    {
        [Test]
        public void Convert_Should_Throw_Exception_When_Any_Path_Starts_With_Delimiter_Symbol()
        {
            var paths = new[]
            {
                "/subFolder1",
                "subFolder1/subsubfolder1a",
                "subFolder2",
                "/subFolder2/subsubfolder1b",
            };

            var sut = new PathToTreeConverter();
            Assert.Throws<ArgumentException>(() => sut.Convert(paths));
        }

        [Test]
        public void Set_Delimiter_Symbol_Should_Succeed()
        {
            char delimiterSymbol = '%';

            var sut = new PathToTreeConverter();
            sut.SetDelimiterSymbol(delimiterSymbol);

            Assert.That(sut.DelimiterSymbol, Is.EqualTo('%'));
        }

        [Test]
        public void Convert_Should_Have_Correct_Root_Count()
        {
            var paths = new[]
            {
                "subFolder1",
                "subFolder1/subsubfolder1a",
                "subFolder2",
                "subFolder2/subsubfolder1b",
            };

            var sut = new PathToTreeConverter();

            var result = sut.Convert(paths);
            
            Assert.That(result, Has.Count.EqualTo(2));
        }

        [Test]
        public void Convert_Should_Have_Correct_Children_Count()
        {
            var paths = new[]
            {
                "subFolder1",
                "subFolder1/subsubfolder1a",
                "subFolder2",
                "subFolder2/subsubfolder1b",
                "subFolder2/subsubfolder2b",
                "subfolder3",
                "subfolder4/",
            };

            var sut = new PathToTreeConverter();

            var result = sut.Convert(paths);
            
            Assert.That(result[0].Children, Has.Count.EqualTo(1));
            Assert.That(result[1].Children, Has.Count.EqualTo(2));
            Assert.That(result[2].Children, Has.Count.EqualTo(0));
            Assert.That(result[3].Children, Has.Count.EqualTo(0));
        }
    }
}