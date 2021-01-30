using System;
using Kusto.Language.Syntax;

namespace KustoPlayground.Pipeline
{
    public interface IPipelineComponent
    {
        void Initialize(SyntaxElement element);
        void Process();
    }
}
