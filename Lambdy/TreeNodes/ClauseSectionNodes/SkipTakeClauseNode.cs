using Lambdy.TreeNodes.ClauseSectionNodes.Abstract;
using Lambdy.Visitors.ClauseSectionSql.Abstract;

namespace Lambdy.TreeNodes.ClauseSectionNodes
{
    internal class SkipTakeClauseNode : ClauseSectionNode
    {
        public int Skip { get; set; }
        
        public int Take { get; set; }
        
        public override void Accept(ClauseSectionNodeVisitor visitor)
        {
            visitor.VisitSkipTakeClause(this);
        }
    }
}