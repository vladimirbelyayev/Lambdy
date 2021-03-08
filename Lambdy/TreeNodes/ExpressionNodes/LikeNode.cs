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

        public override void Accept(ExpressionNodeVisitor visitor)
        {
            visitor.VisitLikeNode(this);
        }
    }
}
