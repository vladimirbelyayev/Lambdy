using System.Linq.Expressions;
using Lambdy.TreeNodes.ExpressionNodes.Abstract;
using Lambdy.Visitors.ExpressionNodeSql.Abstract;

namespace Lambdy.TreeNodes.ExpressionNodes
{
    internal class SingleOperationNode : ExpressionNode
    {
        public ExpressionType Operator { get; set; }
        public ExpressionNode Child { get; set; }

        public override void Accept(VoidExpressionNodeVisitor visitor)
        {
            visitor.VisitSingleOperationNode(this);
        }
    }
}
