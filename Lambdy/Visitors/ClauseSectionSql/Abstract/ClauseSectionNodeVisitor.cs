using Lambdy.TreeNodes.ClauseSectionNodes;

namespace Lambdy.Visitors.ClauseSectionSql.Abstract
{
    internal abstract class ClauseSectionNodeVisitor
    {
        public abstract void VisitFromClause(FromClauseNode inNode);
        
        public abstract void VisitJoinClause(JoinClauseNode inNode);
        
        public abstract void VisitSelectClause(SelectClauseNode inNode);
        
        public abstract void VisitWhereClause(WhereClauseNode inNode);
    }
}