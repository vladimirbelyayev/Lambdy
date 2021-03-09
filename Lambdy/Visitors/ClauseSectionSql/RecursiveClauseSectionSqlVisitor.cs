using System;
using System.Text;
using Lambdy.Constants;
using Lambdy.Constants.Sql;
using Lambdy.Factories;
using Lambdy.Maps;
using Lambdy.Strategies.SkipTake.Interfaces;
using Lambdy.TreeNodes.ClauseSectionNodes;
using Lambdy.Visitors.ClauseSectionSql.Abstract;
using Lambdy.Visitors.ExpressionNodeSql;

namespace Lambdy.Visitors.ClauseSectionSql
{
    internal class RecursiveClauseSectionSqlVisitor : ClauseSectionNodeVisitor
    {
        private readonly RecursiveNodeSqlVisitor _expressionNodeSqlVisitor;
        private readonly ISkipTakeStrategy _skipTakeStrategy;

        public RecursiveClauseSectionSqlVisitor(
            RecursiveNodeSqlVisitor expressionNodeSqlVisitor,
            StringBuilder stringBuilder,
            LambdySqlDialect sqlDialect) : base(stringBuilder)
        {
            _expressionNodeSqlVisitor = expressionNodeSqlVisitor;
            _skipTakeStrategy = SqlDialectStrategyFactory.CreateSkipTakeStrategy(sqlDialect, stringBuilder);
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
            if (selectClauseNode?.Node == null)
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
            if (whereClauseNode == null || whereClauseNode.Nodes.Count == 0)
            {
                Sql = Sql.Replace(
                    LambdyTemplateTokens.Where,
                    string.Empty);
                return;
            }

            StringBuilder.Clear();
            StringBuilder.Append(SqlClauses.Where);
            StringBuilder.Append(' ');
            
            var nodeLength =  whereClauseNode.Nodes.Count;
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

        public override void VisitOrderClause(OrderClauseNode orderClauseNode)
        {
            if (orderClauseNode == null || orderClauseNode.Nodes.Count == 0)
            {
                Sql = Sql.Replace(
                    LambdyTemplateTokens.OrderBy,
                    string.Empty);
                return;
            }

            StringBuilder.Clear();
            StringBuilder.Append(SqlClauses.OrderBy);
            StringBuilder.Append(' ');
            
            var nodeLength =  orderClauseNode.Nodes.Count;
            var nodes = orderClauseNode.Nodes;
            for (var i = 0; i < nodeLength; i++)
            {
                nodes[i].Node.Accept(_expressionNodeSqlVisitor);
                StringBuilder.Append(' ');
                StringBuilder.Append(SqlOrderMap.Orders[nodes[i].Direction]);
                StringBuilder.Append(", ");
            }
            
            StringBuilder.Remove(StringBuilder.Length - 2, 2);
            StringBuilder.Append(Environment.NewLine);
            
            Sql = Sql.Replace(
                LambdyTemplateTokens.OrderBy, 
                StringBuilder.ToString());
        }

        public override void VisitSkipTakeClause(SkipTakeClauseNode skipTakeNode)
        {
            if (skipTakeNode == null || (skipTakeNode.Skip == 0 && skipTakeNode.Take == 0))
            {
                Sql = Sql.Replace(
                    LambdyTemplateTokens.SkipTake, 
                    string.Empty);
                return;
            }
            
            StringBuilder.Clear();
            _skipTakeStrategy.AddSkipTakeText(skipTakeNode.Skip, skipTakeNode.Take);
            StringBuilder.Append(Environment.NewLine);
            
            Sql = Sql.Replace(
                LambdyTemplateTokens.SkipTake, 
                StringBuilder.ToString());
        }
    }
}