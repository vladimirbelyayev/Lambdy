using Lambdy.TreeNodes.ExpressionNodes.Abstract;
using Lambdy.Visitors.ExpressionNodeSql.Abstract;

namespace Lambdy.TreeNodes.ExpressionNodes
{
    internal class ValueNode : ExpressionNode
    {
        public object Value { get; set; }

        public override void Accept(ExpressionNodeVisitor visitor)
        {
            visitor.VisitValueNode(this);
        }
    }
}
