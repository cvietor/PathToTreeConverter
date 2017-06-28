using System;
using System.Collections.Generic;
using System.Linq;

namespace PathsToTree
{
    public class PathsToTreeConverter
    {
        public PathsToTreeConverter()
        {
            DelimiterSymbol = "/";
        }

        /// <summary>
        /// The single- or multicharacter symbol that indicates, at which part the given paths should be splitted.
        /// Default Value is "/"
        /// </summary>
        public string DelimiterSymbol { get; private set; }

        /// <summary>
        /// Converts a list of strings into a Tree model
        /// </summary>
        /// <param name="paths"></param>
        /// <returns></returns>
        public IList<TreeElement> Convert(string[] paths)
        {
            if (paths == null) throw new ArgumentNullException(nameof(paths));

            return BuildTree(paths).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbol"></param>
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
            return paths.GroupBy(path => SplitByDelimiter(path)[0]);
        }

        private string[] SplitByDelimiter(string r)
        {
            var result = r.Split(DelimiterSymbol.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            return result;
        }
    }
}