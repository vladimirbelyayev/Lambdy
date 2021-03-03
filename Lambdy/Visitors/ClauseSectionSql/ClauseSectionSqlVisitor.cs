using System;
using System.Text;
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
        private readonly StringBuilder _stringBuilder;

        public string Sql { get; private set; }
        
        public ClauseSectionSqlVisitor(
            ExpressionNodeSqlVisitor expressionNodeSqlVisitor,
            StringBuilder stringBuilder)
        {
            _expressionNodeSqlVisitor = expressionNodeSqlVisitor;
            _stringBuilder = stringBuilder;
        }

        public void SetTemplate(string sqlTemplate)
        {
            _stringBuilder.Clear();
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

        public override void VisitSelectClause(SelectClauseNode selectClauseNode)
        {
            if (selectClauseNode.Node == null)
            {
                Sql = Sql.Replace(
                    LambdyTemplateTokens.Select, 
                    string.Empty);
                return;
            }
            
            _stringBuilder.Clear();
            _stringBuilder.Append(SqlClauses.Select);
            _stringBuilder.Append(' ');
            
            //TODO: Expression visitor should just append to existing string builder. 
            _stringBuilder.Append(selectClauseNode.Node.Accept(_expressionNodeSqlVisitor));
            
            Sql = Sql.Replace(
                LambdyTemplateTokens.Select, 
                _stringBuilder.ToString());
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

            _stringBuilder.Clear();
            _stringBuilder.Append(SqlClauses.Where);
            _stringBuilder.Append(' ');
            
            var nodes = whereClauseNode.Nodes;
            
            for (var i = 0; i < nodeLength; i++)
            {
                _stringBuilder.Append('(');
                
                //TODO: Expression visitor should just append to existing string builder.  
                _stringBuilder.Append(nodes[i].Accept(_expressionNodeSqlVisitor));
                
                _stringBuilder.Append(')');

                _stringBuilder.Append(' ');
                _stringBuilder.Append(SqlBooleanLogicalOperators.And);
                _stringBuilder.Append(' ');
            }

            //Remove trailing AND
            _stringBuilder.Remove(_stringBuilder.Length - 5, 5);
            _stringBuilder.Append(Environment.NewLine);
            
            Sql = Sql.Replace(
                LambdyTemplateTokens.Where, 
                _stringBuilder.ToString());
        }
        
    }
}