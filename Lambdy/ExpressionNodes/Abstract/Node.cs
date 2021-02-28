using Lambdy.Visitors.Abstract;

namespace Lambdy.ExpressionNodes.Abstract
{
    internal abstract class Node
    {
        public abstract T Accept<T>(ExpressionNodeVisitor<T> visitor);
    }
}
