﻿using System;
using NUnit.Framework;

namespace PathsToTree.Tests
{
    [TestFixture]
    public class PathToTreeConverterTests
    {
        [Test]
        public void Set_Delimiter_Symbol_Should_Succeed()
        {
            string delimiterSymbol = "%";

            var sut = new PathToTreeConverter();
            sut.SetDelimiterSymbol(delimiterSymbol);

            Assert.That(sut.DelimiterSymbol, Is.EqualTo("%"));
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
        public void Convert_Should_Have_Correct_Root_Count_With_Trailing_DelimiterSymbol()
        {
            var paths = new[]
            {
                "/subFolder1",
                "/subFolder1/subsubfolder1a",
                "/subFolder2",
                "/subFolder2/subsubfolder1b",
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

        [Test]
        public void Convert_Should_Have_Correct_Children_Count_When_Subfolder_Is_Equaly_Named_Like_Parent_Folder()
        {
            var paths = new[]
            {
                "subFolder1/subFolder1/subFolder1",
                "subFolder1/subFolder1/xxx",
            };

            var sut = new PathToTreeConverter();

            var result = sut.Convert(paths);

            Assert.That(result[0].Children, Has.Count.EqualTo(1));
            Assert.That(result[0].Children[0].Children, Has.Count.EqualTo(2));
        }
    }
}