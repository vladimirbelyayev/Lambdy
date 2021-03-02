using System.Collections.Generic;
using Lambdy.TreeNodes.ClauseSectionNodes.Abstract;
using Lambdy.TreeNodes.ExpressionNodes.Abstract;
using Lambdy.Visitors.ClauseSectionSql.Abstract;

namespace Lambdy.TreeNodes.ClauseSectionNodes
{
    internal class WhereClauseNode : ClauseSectionNode
    {
        public readonly List<ExpressionNode> Nodes = new List<ExpressionNode>();
        
        public override void Accept(ClauseSectionNodeVisitor visitor)
        {
            visitor.VisitWhereClause(this);
        }
    }
}