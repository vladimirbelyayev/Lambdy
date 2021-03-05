using Lambdy.Parameters;
using Lambdy.TreeNodes.ClauseSectionNodes.Abstract;

namespace Lambdy.Compilers.Query.Input
{
    internal class QueryCompilerInput
    {
        public string SqlTemplate { get; set; }

        public ParameterTracker ParameterTracker { get; set; }
        
        public ClauseSectionNode[] ClauseNodes { get; set; }
        
    }
}