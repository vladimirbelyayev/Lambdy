using System.Collections.Generic;
using Lambdy.TreeNodes.ClauseSectionNodes.Abstract;
using Lambdy.Visitors.ClauseSectionSql.Abstract;

namespace Lambdy.TreeNodes.ClauseSectionNodes
{
    internal class OrderClauseNode : ClauseSectionNode
    {
        public List<OrderClauseEntryNode> Nodes = new List<OrderClauseEntryNode>();
        public override void Accept(ClauseSectionNodeVisitor visitor)
        {
            visitor.VisitOrderClause(this);
        }
    }
}