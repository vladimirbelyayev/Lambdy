using Lambdy.TreeNodes.ClauseSectionNodes;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Lambdy.Builders.SubBuilders.Raw
{
    internal class RawBuilderClauseReferences
    {
        public SelectClauseNode SelectClause { get; set; }
        public FromClauseNode FromClause { get; set; }
        public JoinClauseNode JoinClause { get; set; }
        public WhereClauseNode WhereClause { get; set; }
        public OrderClauseNode OrderClause { get; set; }
        public SkipTakeClauseNode SkipTakeClause  { get; set; }
    }
}