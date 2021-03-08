using Lambdy.TreeNodes.ExpressionNodes.Abstract;
using Lambdy.Visitors.ExpressionNodeSql.Abstract;

namespace Lambdy.TreeNodes.ExpressionNodes
{
    internal class ConstructorMemberNode : ExpressionNode
    {
        public string FieldName { get; set; }
        
        public override void Accept(ExpressionNodeVisitor visitor)
        {
            visitor.VisitConstructorMemberNode(this);
        }
    }
}