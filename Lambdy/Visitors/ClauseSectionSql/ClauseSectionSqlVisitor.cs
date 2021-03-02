using System;
using System.Linq;
using Lambdy.Constants;
using Lambdy.Constants.Sql;
using Lambdy.TreeNodes.ClauseSectionNodes;
using Lambdy.Visitors.ClauseSectionSql.Abstract;
using Lambdy.Visitors.ExpressionNodeSql;

namespace Lambdy.Visitors.ClauseSectionSql
{
    internal class ClauseSectionSqlVisitor : ClauseSectionNodeVisitor
    {
        private readonly ExpressionNodeSqlVisitor _expressionNodeSqlVisitor;

        public string Sql { get; private set; }
        
        public ClauseSectionSqlVisitor(ExpressionNodeSqlVisitor expressionNodeSqlVisitor)
        {
            _expressionNodeSqlVisitor = expressionNodeSqlVisitor;
        }

        public void SetTemplate(string sqlTemplate)
        {
            Sql = sqlTemplate ?? DefaultSqlTemplate.Sql;
        }
        
        public override void VisitFromClause(FromClauseNode inNode)
        {
            Sql = Sql.Replace(
                LambdyTemplateTokens.From, 
                string.Empty);
        }

        public override void VisitJoinClause(JoinClauseNode inNode)
        {
            Sql = Sql.Replace(
                LambdyTemplateTokens.Joins, 
                string.Empty);
        }

        public override void VisitSelectClause(SelectClauseNode inNode)
        {
            Sql = Sql.Replace(
                LambdyTemplateTokens.Select, 
                string.Empty);
        }

        public override void VisitWhereClause(WhereClauseNode whereClauseNode)
        {
            var nodeStrings = whereClauseNode
                .Nodes
                .Select(x => $"({x.Accept(_expressionNodeSqlVisitor)})");

            var joinedNodeStrings = string.Join(
                $"{Environment.NewLine}{SqlBooleanLogicalOperators.And} ", 
                nodeStrings);
            
            var whereClause = $"{SqlClauses.Where} {joinedNodeStrings}{Environment.NewLine}";

            Sql = Sql.Replace(
                LambdyTemplateTokens.Where,
                whereClause);
        }
    }
}