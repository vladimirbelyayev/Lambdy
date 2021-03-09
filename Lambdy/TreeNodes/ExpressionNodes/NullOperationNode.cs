using System.Linq.Expressions;
using Lambdy.TreeNodes.ExpressionNodes.Abstract;
using Lambdy.Visitors.ExpressionNodeSql.Abstract;

namespace Lambdy.TreeNodes.ExpressionNodes
{
    internal class NullOperationNode : ExpressionNode
    {
        public ExpressionType Operator { get; set; }
        public ExpressionNode Left { get; set; }

        public override void Accept(ExpressionNodeVisitor visitor)
        {
            visitor.VisitNullOperationNode(this);
        }
    }
}