using Lambdy.ExpressionNodes.Abstract;
using Lambdy.ValueObjects;
using Lambdy.Visitors.Abstract;

namespace Lambdy.ExpressionNodes
{
    internal class LikeNode : Node
    {
        public LikeMethod Method { get; set; }
        public MemberNode MemberNode { get; set; }
        public ValueNode Value { get; set; }
        
        public override T Accept<T>(ExpressionNodeVisitor<T> visitor)
        {
            return visitor.VisitLikeNode(this);
        }
    }
}
