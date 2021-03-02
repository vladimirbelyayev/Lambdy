using Lambdy.TreeNodes.ExpressionNodes.Abstract;
using Lambdy.Visitors.ExpressionNodeSql.Abstract;

namespace Lambdy.TreeNodes.ExpressionNodes
{
    internal class ValueNode : ExpressionNode
    {
        public object Value { get; set; }
        
        public override T Accept<T>(ExpressionNodeVisitor<T> visitor)
        {
            return visitor.VisitValueNode(this);
        }
    }
}
