using Lambdy.Visitors.ExpressionNodeSql.Abstract;

namespace Lambdy.TreeNodes.ExpressionNodes.Abstract
{
    internal abstract class ExpressionNode
    {
        public abstract T Accept<T>(ExpressionNodeVisitor<T> visitor);
    }
}
