using Lambdy.TreeNodes.ExpressionNodes.Abstract;
using Lambdy.Visitors.ExpressionNodeSql.Abstract;

namespace Lambdy.TreeNodes.ExpressionNodes
{
    internal class ConstructorArgumentNode : ExpressionNode
    {
        public ConstructorMemberNode Left { get; set; }
        public ExpressionNode Right { get; set; }
        public override T Accept<T>(ExpressionNodeVisitor<T> visitor)
        {
            return visitor.VisitConstructorArgumentNode(this);
        }
    }
}