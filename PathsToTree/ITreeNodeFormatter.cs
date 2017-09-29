using System.Collections.Generic;

namespace PathsToTree
{
    public interface ITreeNodeFormatter
    {
        IList<TreeElement> Format(IList<TreeElement> tree);
    }
}