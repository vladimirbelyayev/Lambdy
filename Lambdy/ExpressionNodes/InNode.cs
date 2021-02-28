using Lambdy.ExpressionNodes.Abstract;
using Lambdy.Visitors.Abstract;

namespace Lambdy.ExpressionNodes
{
    internal class InNode : Node
    {
        public MemberNode MemberNode { get; set; }
        
        public ValueNode Value { get; set; }
        
        public override T Accept<T>(ExpressionNodeVisitor<T> visitor)
        {
            return visitor.VisitInNode(this);
        }
    }
}