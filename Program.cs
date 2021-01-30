using System;
using System.Collections.Generic;
using Kusto.Language;
using Kusto.Language.Symbols;
using Kusto.Language.Syntax;
using KustoPlayground.Pipeline;

namespace KustoPlayground
{
    class Program
    {
        static void Main(string[] args)
        {
            // semantic analysis needs any schema of tables and functions the query might reference
            var database =
              new DatabaseSymbol("db",
                new TableSymbol("T",
                  new ColumnSymbol("a", ScalarTypes.Real),
                  new ColumnSymbol("b", ScalarTypes.Real)));

            // create new globals with default database set
            var globals = GlobalState.Default.WithDatabase(database);

            // parse query and perform semantic analysis
            KustoCode query = KustoCode.ParseAndAnalyze("T | project c = a + b, a | where a > 10.0 and c < 5", globals);
 

            Console.WriteLine("Query: "+query.Text);

            var diagnostics = query.GetDiagnostics();
            if (diagnostics.Count > 0)
            {
                Console.WriteLine("Invalid query:");
                foreach (var d in query.GetDiagnostics())
                {
                    Console.WriteLine(" * " + d.Message);
                }
            }
            else
            {
                var root = query.Syntax.Root;
                Console.WriteLine("Result Type:" + query.ResultType.Display);

                //Console.WriteLine("Walking query elements");
                //KustoUtility.ShowChildren(root, "");

                Console.WriteLine("Processing expression into pipeline");
                var pipeline = new List<IPipelineComponent>();
                PipelineFactory.BuildPipeline(root, pipeline);

                Console.WriteLine("Pipeline result:");
                foreach (var component in pipeline)
                {
                    Console.WriteLine($" {component.GetType()}");
                }

            }
        }
    }
}
