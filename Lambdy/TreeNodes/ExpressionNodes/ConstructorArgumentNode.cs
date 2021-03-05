using Lambdy.TreeNodes.ExpressionNodes.Abstract;
using Lambdy.Visitors.ExpressionNodeSql.Abstract;

namespace Lambdy.TreeNodes.ExpressionNodes
{
    internal class ConstructorArgumentNode : ExpressionNode
    {
        public ConstructorMemberNode Left { get; set; }
        public ExpressionNode Right { get; set; }

        public override void Accept(VoidExpressionNodeVisitor visitor)
        {
            visitor.VisitConstructorArgumentNode(this);
        }
    }
}