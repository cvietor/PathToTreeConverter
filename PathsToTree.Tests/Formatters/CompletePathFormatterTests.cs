using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions.Common;
using PathsToTree.Formatters;

namespace PathsToTree.Tests.Formatters
{
    [TestFixture]
    public class CompletePathFormatterTests
    {
        [Test]
        public void Format_With_One_Level_Children_Contains_Parent_Path_In_Name()
        {
            var treeNodes = new List<TreeElement>()
            {
                new TreeElement() { Name = "subfolder1",
                    Children = new List<TreeElement>() { new TreeElement() { Name = "subsubfolder1" } }
                }
            };

            var expectedResult = new List<TreeElement>()
            {
                new TreeElement() { Name = "subfolder1",
                    Children = new List<TreeElement>() { new TreeElement() { Name = "subfolder1/subsubfolder1" } }
                }
            };

            var sut = new CompletePathFormatter();
            var result = sut.Format(treeNodes);

            Assert.That(result[0].Name, Is.EqualTo(expectedResult[0].Name));
            Assert.That(result[0].Children[0].Name, Is.EqualTo(expectedResult[0].Children[0].Name));
        }
    }
}
