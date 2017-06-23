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
            var splitted = rootPath.Select(r => r.Split(DelimiterSymbol.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)).ToList();


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
            if (paths.All(p => p.StartsWith(DelimiterSymbol)))
            {
                return paths.GroupBy(path => path.Split(System.Convert.ToChar(DelimiterSymbol))[1]);
            }

            return paths.GroupBy(path => path.Split(System.Convert.ToChar(DelimiterSymbol))[0]);
        }
    }
}