using Lambdy.TreeNodes.ExpressionNodes.Abstract;
using Lambdy.ValueObjects;

namespace Lambdy.TreeNodes.ClauseSectionNodes
{
    internal class OrderClauseEntryNode
    {
        public ExpressionNode Node { get; set; }
        
        public OrderDirection Direction { get; set; }
    }
}