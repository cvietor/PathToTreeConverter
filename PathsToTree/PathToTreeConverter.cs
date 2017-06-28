using System;
using System.Collections.Generic;
using System.Linq;

namespace PathsToTree
{
    public class PathToTreeConverter
    {
        public PathToTreeConverter()
        {
            DelimiterSymbol = "/";
        }

        public string DelimiterSymbol { get; private set; }

        public IList<TreeElement> Convert(string[] paths)
        {
            Validate(paths);

            return BuildTree(paths).ToList();
        }

        public void SetDelimiterSymbol(string symbol)
        {
            DelimiterSymbol = symbol;
        }

        private IList<TreeElement> BuildTree(IList<string> paths)
        {
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
        }

        private IList<string> GetChildPaths(IGrouping<string, string> rootPath)
        {
            var splitted = rootPath.Select(SplitByDelimiter).ToList();


            var result = new List<string>();
            foreach (var s in splitted)
            {
                if (s.Length <= 1) continue;

                var childPath = string.Join(DelimiterSymbol, s.ToList().GetRange(1, s.Length - 1));
                result.Add(childPath);
            }


            return result;
        }

        private IEnumerable<IGrouping<string, string>> GroupByRootPaths(IList<string> paths)
        {
            IEnumerable<IGrouping<string, string>> result;

            if (paths.All(p => p.StartsWith(DelimiterSymbol)))
                result = paths.GroupBy(path => SplitByDelimiter(path)[1]);

            result = paths.GroupBy(path => SplitByDelimiter(path)[0]);

            return result;
        }

        private string[] SplitByDelimiter(string r)
        {
            return r.Split(DelimiterSymbol.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        }
    }
}