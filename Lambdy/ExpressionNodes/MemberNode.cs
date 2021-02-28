using Lambdy.ExpressionNodes.Abstract;
using Lambdy.Visitors.Abstract;

namespace Lambdy.ExpressionNodes
{
    internal class MemberNode : Node
    {
        public string TableName { get; set; }
        public string FieldName { get; set; }
        public override T Accept<T>(ExpressionNodeVisitor<T> visitor)
        {
            return visitor.VisitMemberNode(this);
        }
    }
}
