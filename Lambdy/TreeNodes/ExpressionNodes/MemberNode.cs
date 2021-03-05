using Lambdy.TreeNodes.ExpressionNodes.Abstract;
using Lambdy.Visitors.ExpressionNodeSql.Abstract;

namespace Lambdy.TreeNodes.ExpressionNodes
{
    internal class MemberNode : ExpressionNode
    {
        public string TableName { get; set; }
        public string FieldName { get; set; }

        public override void Accept(VoidExpressionNodeVisitor visitor)
        {
            visitor.VisitMemberNode(this);
        }
    }
}
