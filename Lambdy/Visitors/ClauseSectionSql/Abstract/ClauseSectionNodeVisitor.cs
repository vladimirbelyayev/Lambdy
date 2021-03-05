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
        
        public abstract void VisitFromClause(FromClauseNode inNode);
        
        public abstract void VisitJoinClause(JoinClauseNode inNode);
        
        public abstract void VisitSelectClause(SelectClauseNode inNode);
        
        public abstract void VisitWhereClause(WhereClauseNode inNode);
    }
}