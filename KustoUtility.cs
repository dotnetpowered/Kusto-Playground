using System;
using Kusto.Language.Syntax;

namespace KustoPlayground
{
    public class KustoUtility
    {
        public static void ShowChildren(SyntaxElement element, string indent)
        {
            var child = element.GetChild(0);

            while (child != null)
            {
                Console.WriteLine($"{indent}{child.Kind} {child}");
                if (child.ChildCount > 0)
                    ShowChildren(child, indent + ' ');
                child = child.GetNextSibling();
            }
        }

        //root.WalkElements((a) =>
        //{
        //    Console.WriteLine($"Walk: {a.Kind} {a.Depth} {a.ChildCount} {a.NameInParent} {a}");
        //});
    }
}
