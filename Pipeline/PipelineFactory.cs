using System;
using System.Collections.Generic;
using Kusto.Language.Syntax;

namespace KustoPlayground.Pipeline
{
    public class PipelineFactory
    {
        public PipelineFactory()
        {
        }

        public static IPipelineComponent NewPipelineComponent(SyntaxElement element)
        {
            IPipelineComponent component;
            switch (element.Kind)
            {
                case SyntaxKind.ProjectOperator:
                    component = new ProjectPipeline();
                    break;
                case SyntaxKind.FilterOperator:
                    component = new FilterPipeline();
                    break;
                case SyntaxKind.NameReference:
                    component = new NameReferencePipeline();
                    break;
                default:
                    return null;
            }
            component.Initialize(element);
            return component;
        }

        public static void BuildPipeline(SyntaxElement element, List<IPipelineComponent> pipeline)
        {
            var child = element.GetChild(0);

            while (child != null)
            {
                var component = NewPipelineComponent(child);
                if (component != null)
                    pipeline.Add(component);
                else
                if (child.ChildCount > 0)
                    BuildPipeline(child, pipeline);
                child = child.GetNextSibling();
            }
        }
    }
}
