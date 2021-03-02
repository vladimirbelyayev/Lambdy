using Lambdy.TreeNodes.ExpressionNodes.Abstract;
using Lambdy.Visitors.ExpressionNodeSql.Abstract;

namespace Lambdy.TreeNodes.ExpressionNodes
{
    internal class InNode : ExpressionNode
    {
        public MemberNode MemberNode { get; set; }
        
        public ValueNode Value { get; set; }
        
        public override T Accept<T>(ExpressionNodeVisitor<T> visitor)
        {
            return visitor.VisitInNode(this);
        }
    }
}