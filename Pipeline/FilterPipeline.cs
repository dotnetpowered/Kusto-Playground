using System;
using Kusto.Language.Syntax;

namespace KustoPlayground.Pipeline
{
    public class FilterPipeline : IPipelineComponent
    {
        SyntaxElement syntaxElement;

        public FilterPipeline()
        {
        }

        public void Initialize(SyntaxElement element)
        {
            var item = element.GetChild(2).GetChild(0); // 0=FilterKeyword, 1=list, 2=
            this.syntaxElement = item;
            Console.WriteLine("Filter");
            while (item != null)
            {
                Console.WriteLine($"> {item.Kind} {item}");
                if (item.ChildCount > 0)
                    KustoUtility.ShowChildren(item, "  | ");
                item = item.GetNextSibling();
            }
        }

        public void Process()
        {
            throw new NotImplementedException();
        }
    }
}
