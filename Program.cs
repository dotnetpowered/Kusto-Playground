using System;
using Kusto.Language;
using Kusto.Language.Symbols;

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
            // (IReadOnlyList<ClusterSymbol> clusters, ClusterSymbol cluster, DatabaseSymbol database, IReadOnlyList<FunctionSymbol> functions, Dictionary<string, FunctionSymbol> functionMap, IReadOnlyList<FunctionSymbol> aggregates, Dictionary<string, FunctionSymbol> aggregateMap, IReadOnlyList<FunctionSymbol> plugins, Dictionary<string, FunctionSymbol> pluginMap, IReadOnlyList<OperatorSymbol> operators, Dictionary<OperatorKind, OperatorSymbol> operatorMap, IReadOnlyList<CommandSymbol> commands, Dictionary<string, CommandSymbol> commandMap, Dictionary<string, IReadOnlyList<CommandSymbol>> commandListMap, IReadOnlyList<ParameterSymbol> parameters);
            // parse query and perform semantic analysis
            KustoCode query = KustoCode.ParseAndAnalyze("let dateTimeLowerBound = datetime(2017-01-21); T | project c = a + b, a | where a > 10.0 and c < 5", globals);
            //var child = query.Syntax.GetChild(0);
            //var expansion = query.Syntax.GetTokens();
            var root = query.Syntax.Root;
            var expansion = query.Syntax.GetExpansion();
            Console.WriteLine(root);
        }
    }
}
