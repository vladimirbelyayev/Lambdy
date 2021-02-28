using System.Linq.Expressions;
using Lambdy.ExpressionNodes.Abstract;
using Lambdy.Visitors.Abstract;

namespace Lambdy.ExpressionNodes
{
    internal class OperationNode : Node
    {
        public ExpressionType Operator { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        
        public override T Accept<T>(ExpressionNodeVisitor<T> visitor)
        {
            return visitor.VisitOperationNode(this);
        }
    }
}
