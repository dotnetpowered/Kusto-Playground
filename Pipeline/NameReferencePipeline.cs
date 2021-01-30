using System;
using Kusto.Language.Syntax;

namespace KustoPlayground.Pipeline
{
    public class NameReferencePipeline : IPipelineComponent
    {
        SyntaxElement syntaxElement;

        public NameReferencePipeline()
        {
        }

        public void Initialize(SyntaxElement element)
        {
            var item = element.GetChild(0).GetChild(0);
            this.syntaxElement = item;
            Console.WriteLine($"> {item.Kind} {item}");
        }

        public void Process()
        {
            throw new NotImplementedException();
        }
    }
}
