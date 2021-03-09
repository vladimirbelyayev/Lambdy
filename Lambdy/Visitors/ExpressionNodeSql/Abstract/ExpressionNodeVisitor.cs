using Lambdy.TreeNodes.ExpressionNodes;

namespace Lambdy.Visitors.ExpressionNodeSql.Abstract
{
    internal abstract class ExpressionNodeVisitor
    {
        public abstract void VisitInNode(InNode inNode);
        public abstract void VisitLikeNode(LikeNode likeNode);
        public abstract void VisitMemberNode(MemberNode memberNode);
        public abstract void VisitOperationNode(OperationNode operationNode);
        public abstract void VisitNullOperationNode(NullOperationNode operationNode);
        public abstract void VisitSingleOperationNode(SingleOperationNode singleOperationNode);
        public abstract void VisitValueNode(ValueNode valueNode);
        public abstract void VisitConstructorNode(ConstructorNode constructorNode);
        
        public abstract void VisitConstructorArgumentNode(ConstructorArgumentNode constructorNode);
        
        public abstract void VisitConstructorMemberNode(ConstructorMemberNode constructorNode);
    }
}