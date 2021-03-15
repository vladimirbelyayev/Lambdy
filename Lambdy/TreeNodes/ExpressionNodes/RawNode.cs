using Lambdy.TreeNodes.ExpressionNodes.Abstract;
using Lambdy.Visitors.ExpressionNodeSql.Abstract;

namespace Lambdy.TreeNodes.ExpressionNodes
{
    internal class RawNode : ExpressionNode
    {
        public string Sql { get; }
        
        public RawNode(string sql)
        {
            Sql = sql;
        }
        
        public override void Accept(ExpressionNodeVisitor visitor)
        {
            visitor.VisitRawNode(this);
        }
    }
}