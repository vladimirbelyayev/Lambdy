using Lambdy.TreeNodes.ExpressionNodes.Abstract;
using Lambdy.Visitors.ExpressionNodeSql.Abstract;

namespace Lambdy.TreeNodes.ExpressionNodes
{
    internal class ConstructorNode : ExpressionNode
    {
        public ConstructorArgumentNode[] ConstructorArgumentNodes { get; set; }
        
        public override T Accept<T>(ExpressionNodeVisitor<T> visitor)
        {
            return visitor.VisitConstructorNode(this);
        }
    }
}