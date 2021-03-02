using Lambdy.TreeNodes.ExpressionNodes.Abstract;
using Lambdy.ValueObjects;
using Lambdy.Visitors.ExpressionNodeSql.Abstract;

namespace Lambdy.TreeNodes.ExpressionNodes
{
    internal class LikeNode : ExpressionNode
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
