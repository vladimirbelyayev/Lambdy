using Lambdy.TreeNodes.ClauseSectionNodes.Abstract;
using Lambdy.Visitors.ClauseSectionSql.Abstract;

namespace Lambdy.TreeNodes.ClauseSectionNodes
{
    internal class JoinClauseNode : ClauseSectionNode
    {
        public override void Accept(ClauseSectionNodeVisitor visitor)
        {
            visitor.VisitJoinClause(this);
        }
    }
}