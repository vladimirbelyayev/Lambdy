using Lambdy.Visitors.ClauseSectionSql.Abstract;

namespace Lambdy.TreeNodes.ClauseSectionNodes.Abstract
{
    internal abstract class ClauseSectionNode
    {
        public abstract void Accept(ClauseSectionNodeVisitor visitor);
    }
}