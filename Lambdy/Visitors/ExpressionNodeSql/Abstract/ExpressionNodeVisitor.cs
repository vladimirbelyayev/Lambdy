using Lambdy.TreeNodes.ExpressionNodes;

namespace Lambdy.Visitors.ExpressionNodeSql.Abstract
{
    internal abstract class ExpressionNodeVisitor<TResult>
    {
        public abstract TResult VisitInNode(InNode inNode);
        public abstract TResult VisitLikeNode(LikeNode likeNode);
        public abstract TResult VisitMemberNode(MemberNode memberNode);
        public abstract TResult VisitOperationNode(OperationNode operationNode);
        public abstract TResult VisitSingleOperationNode(SingleOperationNode singleOperationNode);
        public abstract TResult VisitValueNode(ValueNode valueNode);
    }
}