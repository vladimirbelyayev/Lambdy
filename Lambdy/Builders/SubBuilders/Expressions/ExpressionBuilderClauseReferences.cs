using Lambdy.TreeNodes.ClauseSectionNodes;

namespace Lambdy.Builders.SubBuilders.Expressions
{
    internal class ExpressionBuilderClauseReferences
    {
        public SelectClauseNode SelectClause { get; set; }
        public FromClauseNode FromClause { get; set; }
        public JoinClauseNode JoinClause { get; set; }
        public WhereClauseNode WhereClause { get; set; }
        public OrderClauseNode OrderClause { get; set; }
        public SkipTakeClauseNode SkipTakeClause  { get; set; }
    }
}