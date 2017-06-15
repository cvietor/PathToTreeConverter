using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathsToTree
{
    public class TreeElement
    {
        public string Name { get; set; }
        public IList<TreeElement> Children { get; set; } = new List<TreeElement>();
    }
}
