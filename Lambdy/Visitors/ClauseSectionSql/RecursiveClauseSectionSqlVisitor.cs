using System;
using System.Text;
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
        private readonly bool _removeEmptyTokens;

        public RecursiveClauseSectionSqlVisitor(
            RecursiveNodeSqlVisitor expressionNodeSqlVisitor,
            StringBuilder stringBuilder,
            LambdySqlDialect sqlDialect,
            bool removeEmptyTokens) : base(stringBuilder)
        {
            _expressionNodeSqlVisitor = expressionNodeSqlVisitor;
            _skipTakeStrategy = SqlDialectStrategyFactory.CreateSkipTakeStrategy(sqlDialect, stringBuilder);
            _removeEmptyTokens = removeEmptyTokens;
        }
        
        public override void VisitFromClause(FromClauseNode fromClauseNode)
        {
            if (fromClauseNode?.Node == null)
            {
                if (_removeEmptyTokens)
                {
                    Sql = Sql.Replace(
                        LambdyTemplateTokens.From, 
                        string.Empty);
                }

                return;
            }
            
            StringBuilder.Clear();
            if (!Sql.Contains(SqlClauses.From))
            {
                StringBuilder.Append(SqlClauses.From);
                StringBuilder.Append(' ');
            }

            fromClauseNode.Node.Accept(_expressionNodeSqlVisitor);
            StringBuilder.Append(Environment.NewLine);

            Sql = Sql.Replace(
                LambdyTemplateTokens.From, 
                StringBuilder.ToString());
        }

        public override void VisitJoinClause(JoinClauseNode joinClauseNode)
        {
            if (joinClauseNode?.Nodes == null || joinClauseNode.Nodes.Count == 0)
            {
                if (_removeEmptyTokens)
                {
                    Sql = Sql.Replace(
                        LambdyTemplateTokens.Joins, 
                        string.Empty);
                }

                return;
            }
            
            StringBuilder.Clear();

            var nodeLength =  joinClauseNode.Nodes.Count;
            var nodes = joinClauseNode.Nodes;
            for (var i = 0; i < nodeLength; i++)
            {
                nodes[i].Accept(_expressionNodeSqlVisitor);
                StringBuilder.Append(Environment.NewLine);
            }
            
            Sql = Sql.Replace(
                LambdyTemplateTokens.Joins, 
                StringBuilder.ToString());
        }

        public override void VisitSelectClause(SelectClauseNode selectClauseNode)
        {
            if (selectClauseNode?.Node == null)
            {
                if (_removeEmptyTokens)
                {
                    Sql = Sql.Replace(
                        LambdyTemplateTokens.Select, 
                        string.Empty);
                }

                return;
            }
            
            StringBuilder.Clear();
            if (!Sql.Contains(SqlClauses.Select))
            {
                StringBuilder.Append(SqlClauses.Select);
                StringBuilder.Append(' ');
            }

            selectClauseNode.Node.Accept(_expressionNodeSqlVisitor);
            StringBuilder.Append(Environment.NewLine);
            
            Sql = Sql.Replace(
                LambdyTemplateTokens.Select, 
                StringBuilder.ToString());
        }

        public override void VisitWhereClause(WhereClauseNode whereClauseNode)
        {
            if (whereClauseNode == null || whereClauseNode.Nodes.Count == 0)
            {
                if (_removeEmptyTokens)
                {
                    Sql = Sql.Replace(
                        LambdyTemplateTokens.Where,
                        string.Empty);
                }

                return;
            }

            StringBuilder.Clear();
            if (!Sql.Contains(SqlClauses.Where))
            {
                StringBuilder.Append(SqlClauses.Where);
                StringBuilder.Append(' ');
            }

            var nodeLength =  whereClauseNode.Nodes.Count;
            var nodes = whereClauseNode.Nodes;
            var brace = nodeLength > 1;
            
            for (var i = 0; i < nodeLength; i++)
            {
                if (brace)
                {
                    StringBuilder.Append('(');
                }
                nodes[i].Accept(_expressionNodeSqlVisitor);
                if (brace)
                {
                    StringBuilder.Append(')');
                }
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
                if (_removeEmptyTokens)
                {
                    Sql = Sql.Replace(
                        LambdyTemplateTokens.OrderBy,
                        string.Empty);
                }

                return;
            }

            StringBuilder.Clear();
            if (!Sql.Contains(SqlClauses.OrderBy))
            {
                StringBuilder.Append(SqlClauses.OrderBy);
                StringBuilder.Append(' ');
            }

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
                if (_removeEmptyTokens)
                {
                    Sql = Sql.Replace(
                        LambdyTemplateTokens.SkipTake, 
                        string.Empty);
                }

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