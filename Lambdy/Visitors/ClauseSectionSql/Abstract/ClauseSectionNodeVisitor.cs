using System.Text;
using Lambdy.Constants;
using Lambdy.TreeNodes.ClauseSectionNodes;

namespace Lambdy.Visitors.ClauseSectionSql.Abstract
{
    internal abstract class ClauseSectionNodeVisitor
    {
        public string Sql { get; set; }
        
        protected readonly StringBuilder StringBuilder;
        
        public void SetTemplate(string sqlTemplate)
        {
            StringBuilder.Clear();
            Sql = sqlTemplate ?? DefaultSqlTemplate.Sql;
        }

        public ClauseSectionNodeVisitor(StringBuilder stringBuilder)
        {
            StringBuilder = stringBuilder;
        }
        
        public abstract void VisitFromClause(FromClauseNode fromNode);
        
        public abstract void VisitJoinClause(JoinClauseNode joinNode);
        
        public abstract void VisitSelectClause(SelectClauseNode selectNode);
        
        public abstract void VisitWhereClause(WhereClauseNode whereNode);

        public abstract void VisitOrderClause(OrderClauseNode orderClauseNode);

        public abstract void VisitSkipTakeClause(SkipTakeClauseNode skipTakeNode);
    }
}