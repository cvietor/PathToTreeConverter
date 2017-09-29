using System;
using PathsToTree.DemoConsole.Extensions;

namespace PathsToTree.DemoConsole
{
    internal class Program
    {
        private static void Main()
        {
            var paths = new[]
            {
                "//subFolder1",
                "//subFolder1//subsubfolder1a",
                "//subFolder1//subsubfolder1a//sub-sub-sub",
                "//subFolder2",
                "//subFolder2//subsubfolder1b"
            };

            var converter = new PathsToTreeConverter(new PathsToTreeConverterOptions()
            {
                DelimiterSymbol = "//"
            });

            var result = converter.Convert(paths);

            foreach (var treeElement in result)
                treeElement.PrintPretty("", Console.WriteLine);

            Console.Read();
        }
    }
}