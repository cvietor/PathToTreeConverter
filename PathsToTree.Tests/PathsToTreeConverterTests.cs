using System.Collections.Generic;
using Moq;
using NUnit.Framework;

namespace PathsToTree.Tests
{
    [TestFixture]
    public class PathsToTreeConverterTests
    {
        [Test]
        public void Convert_Should_Return_One_Element_When_Only_A_Simple_String_Is_Passed()
        {
            var paths = new[]
            {
                "iamalonelystringwithoutanydelimiter"
            };

            var sut = new PathsToTreeConverter();

            var result = sut.Convert(paths);

            Assert.That(result.Count, Is.EqualTo(1));
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

            var sut = new PathsToTreeConverter();

            var result = sut.Convert(paths);
            
            Assert.That(result, Has.Count.EqualTo(2));
        }

        [Test]
        public void Convert_Should_Have_Correct_Root_Count_With_MultiCharacter_DelimiterSymbol()
        {
            var paths = new[]
            {
                "//subFolder1",
                "//subFolder1//subsubfolder1a",
                "//subFolder2",
                "//subFolder2//subsubfolder1b",
            };

            var options = new PathsToTreeConverterOptions()
            {
                DelimiterSymbol = "//"
            };
            var sut = new PathsToTreeConverter(options);

            var result = sut.Convert(paths);

            Assert.That(result, Has.Count.EqualTo(2));
        }

        [Test]
        [TestCase(@"/")]
        [TestCase(@"\")]
        [TestCase(@"//")]
        [TestCase(@"\\")]
        [TestCase(@"///")]
        [TestCase(@"\\\")]
        public void Convert_Should_Have_Correct_Root_Count_With_Any_DelimiterSymbols(string delimiterSymbol)
        {
            var paths = new[]
            {
                $"{delimiterSymbol}subFolder1",
                $"{delimiterSymbol}subFolder1{delimiterSymbol}subsubfolder1a",
                $"{delimiterSymbol}subFolder2",
                $"{delimiterSymbol}subFolder2{delimiterSymbol}subsubfolder1b",
            };

            var options = new PathsToTreeConverterOptions()
            {
                DelimiterSymbol = delimiterSymbol
            };
            var sut = new PathsToTreeConverter(options);

            var result = sut.Convert(paths);

            Assert.That(result, Has.Count.EqualTo(2));
        }

        [Test]
        public void Convert_Should_Have_Correct_Root_Count_With_Trailing_DelimiterSymbol()
        {
            var paths = new[]
            {
                "/subFolder1",
                "/subFolder1/subsubfolder1a",
                "/subFolder2",
                "/subFolder2/subsubfolder1b",
            };

            var sut = new PathsToTreeConverter();

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

            var sut = new PathsToTreeConverter();

            var result = sut.Convert(paths);
            
            Assert.That(result[0].Children, Has.Count.EqualTo(1));
            Assert.That(result[1].Children, Has.Count.EqualTo(2));
            Assert.That(result[2].Children, Has.Count.EqualTo(0));
            Assert.That(result[3].Children, Has.Count.EqualTo(0));
        }

        [Test]
        public void Convert_Should_Have_Correct_Children_Count_When_Subfolder_Is_Equaly_Named_Like_Parent_Folder()
        {
            var paths = new[]
            {
                "subFolder1/subFolder1/subFolder1",
                "subFolder1/subFolder1/xxx",
            };

            var sut = new PathsToTreeConverter();

            var result = sut.Convert(paths);

            Assert.That(result[0].Children, Has.Count.EqualTo(1));
            Assert.That(result[0].Children[0].Children, Has.Count.EqualTo(2));
        }

        [Test]
        public void Convert_Should_Output_in_Correct_Format_When_No_Formatter_Is_Defined()
        {
            var paths = new[]
            {
                "subFolder1/subsubfolder1",
            };

            var sut = new PathsToTreeConverter();

            var result = sut.Convert(paths);

            Assert.That(result[0].Name, Is.EqualTo("subFolder1"));
            Assert.That(result[0].Children[0].Name, Is.EqualTo("subsubfolder1"));
        }

        [Test]
        public void Convert_Should_Output_in_Correct_Format_When_Custom_Formatter_Is_Defined()
        {
            var paths = new[]
            {
                "subFolder1/subsubfolder1",
            };

            var expectedResult = new List<TreeElement>()
            {
                new TreeElement()
                {
                    Name = "subfolder1",
                    Children = new List<TreeElement>()
                    {
                        new TreeElement()
                        {
                            Name = "subfolder1/subsubfolder1"
                        }
                    }
                }
            };

            var formatterMock = Mock.Of<ITreeNodeFormatter>(f =>
                f.Format(It.IsAny<IList<TreeElement>>()) == expectedResult);
            
            var sut = new PathsToTreeConverter();
            sut.Formatter = formatterMock;

            var result = sut.Convert(paths);

            Assert.That(result[0].Name, Is.EqualTo("subfolder1"));
            Assert.That(result[0].Children[0].Name, Is.EqualTo("subfolder1/subsubfolder1"));
        }
    }
}