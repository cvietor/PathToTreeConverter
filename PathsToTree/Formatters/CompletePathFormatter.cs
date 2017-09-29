using System.Collections.Generic;

namespace PathsToTree.Formatters
{
    public class CompletePathFormatter : ITreeNodeFormatter
    {
        public IList<TreeElement> Format(IList<TreeElement> tree)
        {
            tree = FormatRecursivly(tree);

            return tree;
        }

        private IList<TreeElement> FormatRecursivly(IList<TreeElement> tree, TreeElement parentElement = null)
        {
            foreach (var treeNode in tree)
            {
                if (parentElement != null)
                {
                    treeNode.Name = $"{parentElement.Name}/{treeNode.Name}";
                }

                if (treeNode.Children.Count > 0)
                {
                    FormatRecursivly(treeNode.Children, treeNode);
                }
            }

            return tree;
        }
    }
}