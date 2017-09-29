using System;

namespace PathsToTree.DemoConsole.Extensions
{
    public static class ConsoleExtensions
    {
        public static void PrintPretty(this TreeElement me, string prefix, Action<string> output)
        {
            output($"{prefix} + {me.Name}");

            foreach (var n in me.Children)
                if (me.Children.IndexOf(n) == me.Children.Count - 1)
                    n.PrintPretty(prefix + "    ", output);
                else
                    n.PrintPretty(prefix + "   |", output);
        }
    }
}