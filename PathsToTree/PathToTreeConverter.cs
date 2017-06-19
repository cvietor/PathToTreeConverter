using System;
using System.Collections.Generic;
using System.Linq;

namespace PathsToTree
{
    public class PathToTreeConverter
    {
        private TreeElement _rootNode = null;
        private IList<TreeElement> _folderStructure = new List<TreeElement>();


        public PathToTreeConverter()
        {
            DelimiterSymbol = System.Convert.ToChar("/");
        }

        public char DelimiterSymbol { get; private set; }

        public IList<TreeElement> Convert(string[] paths)
        {
            foreach (var path in paths)
            {
                var nodes = path.Split(DelimiterSymbol).ToList();
                var name = $"/{nodes[0]}";
                var root = _folderStructure.FirstOrDefault((n) => n.Name == name);

                if (root == null)
                {
                    root = new TreeElement() { Name = name };
                    _folderStructure.Add(root);

                    if (nodes.Count == 1)
                    {
                        continue;
                    }

                    var children = nodes.GetRange(1, nodes.Count - 1).Where((c) => c != "").ToList();
                    if (children.Count > 0)
                        CreateNode(root, children);
                }
                else
                {
                    if (nodes.Count == 1)
                    {
                        continue;
                    }

                    var children = nodes.GetRange(1, nodes.Count - 1).Where((c) => c != "").ToList();
                    if (children.Count > 0)
                        CreateNode(root, children);
                }
            }

            var debug = true;
            return _folderStructure;
        }

        private TreeElement CreateNode(TreeElement current, List<string> nodes)
        {
            string name = string.Format("{0}/{1}", current.Name, nodes[0]);

            TreeElement node = current.Children.FirstOrDefault((c) => c.Name == name);

            if (node != null)
            {
                if (nodes.Count == 1)
                {
                    return node;
                }

                var children = nodes.GetRange(1, nodes.Count - 1).Where((c) => c != "").ToList();
                if (children.Count > 0)
                    CreateNode(node, children);
            }
            else
            {
                node = new TreeElement() { Name = name };
                current.Children.Add(node);

                var children = nodes.GetRange(1, nodes.Count - 1).Where((c) => c != "").ToList();
                if (children.Count > 0)
                    CreateNode(node, children);
            }

            return node;
        }

        public void SetDelimiterSymbol(char symbol)
        {
            DelimiterSymbol = symbol;
        }
    }
}