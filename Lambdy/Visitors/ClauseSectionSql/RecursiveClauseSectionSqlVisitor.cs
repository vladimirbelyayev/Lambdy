using System;
using System.Text;
using Lambdy.Constants;
using Lambdy.Constants.Sql;
using Lambdy.TreeNodes.ClauseSectionNodes;
using Lambdy.Visitors.ClauseSectionSql.Abstract;
using Lambdy.Visitors.ExpressionNodeSql;

namespace Lambdy.Visitors.ClauseSectionSql
{
    internal class RecursiveClauseSectionSqlVisitor : ClauseSectionNodeVisitor
    {
        private readonly RecursiveNodeSqlVisitor _expressionNodeSqlVisitor;


        public RecursiveClauseSectionSqlVisitor(
            RecursiveNodeSqlVisitor expressionNodeSqlVisitor,
            StringBuilder stringBuilder) : base(stringBuilder)
        {
            _expressionNodeSqlVisitor = expressionNodeSqlVisitor;
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

        public override void VisitSelectClause(SelectClauseNode selectClauseNode)
        {
            if (selectClauseNode.Node == null)
            {
                Sql = Sql.Replace(
                    LambdyTemplateTokens.Select, 
                    string.Empty);
                return;
            }
            
            StringBuilder.Clear();
            StringBuilder.Append(SqlClauses.Select);
            StringBuilder.Append(' ');
            selectClauseNode.Node.Accept(_expressionNodeSqlVisitor);
            
            Sql = Sql.Replace(
                LambdyTemplateTokens.Select, 
                StringBuilder.ToString());
        }

        public override void VisitWhereClause(WhereClauseNode whereClauseNode)
        {
            var nodeLength =  whereClauseNode.Nodes.Count;
            if (nodeLength == 0)
            {
                Sql = Sql.Replace(
                    LambdyTemplateTokens.Where,
                    string.Empty);
                return;
            }

            StringBuilder.Clear();
            StringBuilder.Append(SqlClauses.Where);
            StringBuilder.Append(' ');
            
            var nodes = whereClauseNode.Nodes;
            
            for (var i = 0; i < nodeLength; i++)
            {
                StringBuilder.Append('(');
                nodes[i].Accept(_expressionNodeSqlVisitor);
                StringBuilder.Append(')');
                StringBuilder.Append(' ');
                StringBuilder.Append(SqlBooleanLogicalOperators.And);
                StringBuilder.Append(' ');
            }

            //Remove trailing AND
            StringBuilder.Remove(StringBuilder.Length - 5, 5);
            StringBuilder.Append(Environment.NewLine);
            
            Sql = Sql.Replace(
                LambdyTemplateTokens.Where, 
                StringBuilder.ToString());
        }
    }
}