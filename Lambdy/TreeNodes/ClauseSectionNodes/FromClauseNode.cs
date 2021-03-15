using Lambdy.TreeNodes.ClauseSectionNodes.Abstract;
using Lambdy.TreeNodes.ExpressionNodes.Abstract;
using Lambdy.Visitors.ClauseSectionSql.Abstract;

namespace Lambdy.TreeNodes.ClauseSectionNodes
{
    internal class FromClauseNode : ClauseSectionNode
    {
        public ExpressionNode Node { get; set; }

        public override void Accept(ClauseSectionNodeVisitor visitor)
        {
            visitor.VisitFromClause(this);
        }
    }
}