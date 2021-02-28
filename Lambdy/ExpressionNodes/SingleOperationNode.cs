using System.Linq.Expressions;
using Lambdy.ExpressionNodes.Abstract;
using Lambdy.Visitors.Abstract;

namespace Lambdy.ExpressionNodes
{
    internal class SingleOperationNode : Node
    {
        public ExpressionType Operator { get; set; }
        public Node Child { get; set; }
        
        public override T Accept<T>(ExpressionNodeVisitor<T> visitor)
        {
            return visitor.VisitSingleOperationNode(this);
        }
    }
}
