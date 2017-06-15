using System;
using System.Collections.Generic;
using System.Linq;

namespace PathsToTree
{
    public class PathToTreeConverter
    {
        public PathToTreeConverter()
        {
            DelimiterSymbol = System.Convert.ToChar("/");
        }

        public char DelimiterSymbol { get; private set; }

        public IList<TreeElement> Convert(string[] paths)
        {
            return BuildTree(paths).ToList();
        }

        public void SetDelimiterSymbol(char symbol)
        {
            DelimiterSymbol = symbol;
        }

        private IList<TreeElement> BuildTree(IList<string> paths)
        {
            Validate(paths);

            var list = new List<TreeElement>();

            var rootPaths = GroupByRootPaths(paths);

            foreach (var rootPath in rootPaths)
            {
                var childPaths = GetChildPaths(rootPath);
                var childElements = BuildTree(childPaths);

                list.Add(new TreeElement
                {
                    Name = rootPath.Key,
                    Children = childElements
                });
            }
            return list;
        }

        private void Validate(IList<string> paths)
        {
            if (paths == null) throw new ArgumentNullException(nameof(paths));

            if (paths.Any(path => path.StartsWith(DelimiterSymbol.ToString())))
                throw new ArgumentException($"paths are not allowed to begin with delimiter symbol: {DelimiterSymbol}");
        }

        private IList<string> GetChildPaths(IGrouping<string, string> rootPath)
        {
            var result = rootPath
                .Where(path => !path.Equals(rootPath.Key))
                .Where(path => !path.Equals(rootPath.Key + DelimiterSymbol))
                .Select(path => path.Replace(rootPath.Key + DelimiterSymbol, string.Empty))
                .ToList();

            return result;
        }

        private IEnumerable<IGrouping<string, string>> GroupByRootPaths(IEnumerable<string> paths)
        {
            return paths.GroupBy(path => path.Split(DelimiterSymbol)[0]);
        }
    }
}