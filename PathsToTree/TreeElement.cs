using System.Collections.Generic;

namespace PathsToTree
{
    public class TreeElement
    {
        public string Name { get; set; }
        public IList<TreeElement> Children { get; set; } = new List<TreeElement>();
    }
}