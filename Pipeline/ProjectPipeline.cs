using System;
using Kusto.Language.Syntax;

namespace KustoPlayground.Pipeline
{
    public class ProjectPipeline : IPipelineComponent
    {
        SyntaxElement syntaxElement;

        public ProjectPipeline()
        {
        }

        public void Initialize(SyntaxElement element)
        {
            var item = element.GetChild(1).GetChild(0); // 0=ProjectKeyword, 1=list
            this.syntaxElement = item;
            Console.WriteLine("Projection");
            while (item != null)
            {
                var subItem = item.GetChild(0);
                switch (subItem.Kind)
                {
                    case SyntaxKind.SimpleNamedExpression: SimpleNamedExpression(subItem); break;
                    case SyntaxKind.NameReference: NameReference(subItem); break;
                    default: Console.WriteLine($"> Unknown:: {subItem.Kind} {subItem}"); break;
                }
                item = item.GetNextSibling();
            }
        }

        static void SimpleNamedExpression(SyntaxElement element)
        {
            Console.WriteLine($"> {element.Kind} {element}");
            KustoUtility.ShowChildren(element, "  | ");
        }

        static void NameReference(SyntaxElement child)
        {
            var item = child.GetChild(0).GetChild(0);
            Console.WriteLine($"> {item.Kind} {item}");
        }

        public void Process()
        {
            throw new NotImplementedException();
        }
    }
}
