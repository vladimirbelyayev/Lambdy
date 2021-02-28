using Lambdy.ExpressionNodes.Abstract;
using Lambdy.Visitors.Abstract;

namespace Lambdy.ExpressionNodes
{
    internal class ValueNode : Node
    {
        public object Value { get; set; }
        
        public override T Accept<T>(ExpressionNodeVisitor<T> visitor)
        {
            return visitor.VisitValueNode(this);
        }
    }
}
