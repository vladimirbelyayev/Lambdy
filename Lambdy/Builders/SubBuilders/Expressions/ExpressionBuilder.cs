using System.Collections.Generic;
using Lambdy.Builders.SubBuilders.Expressions.Interfaces;
using Lambdy.Resolvers;
using Lambdy.TreeNodes.ClauseSectionNodes;
using Lambdy.ValueObjects;

namespace Lambdy.Builders.SubBuilders.Expressions
{
    internal class ExpressionBuilder : IExpressionBuilder 
    {
        private readonly ExpressionBuilderClauseReferences _clauseReferences;
        
        public ExpressionBuilder(
            ExpressionBuilderClauseReferences clauseReferences)
        {
            _clauseReferences = clauseReferences;
        }
        
        public void AddSelectExpression(System.Linq.Expressions.Expression exprBody)
        {
            _clauseReferences.SelectClause.Node = ExpressionResolverMediator
                .ResolveExpression(exprBody);
        }
        
        public void AddWhereExpression(System.Linq.Expressions.Expression exprBody)
        {
            _clauseReferences.WhereClause.Nodes
                .Add(ExpressionResolverMediator.ResolveExpression(exprBody));
        }

        public void AddOrderByExpression(System.Linq.Expressions.Expression exprBody)
        {
            _clauseReferences.OrderClause.Nodes = new List<OrderClauseEntryNode>()
            {
                new OrderClauseEntryNode()
                {
                    Node = ExpressionResolverMediator.ResolveExpression(exprBody),
                    Direction = OrderDirection.Asc
                }
            };
        }
        
        public void AddThenByExpression(System.Linq.Expressions.Expression exprBody)
        {
            _clauseReferences.OrderClause.Nodes.Add(new OrderClauseEntryNode()
            {
                Direction = OrderDirection.Asc,
                Node = ExpressionResolverMediator.ResolveExpression(exprBody)
            });
        }
        
        public void AddOrderByDescExpression(System.Linq.Expressions.Expression exprBody)
        {
            _clauseReferences.OrderClause.Nodes = new List<OrderClauseEntryNode>()
            {
                new OrderClauseEntryNode()
                {
                    Node = ExpressionResolverMediator.ResolveExpression(exprBody),
                    Direction = OrderDirection.Desc
                }
            };
        }
        
        public void AddThenByDescExpression(System.Linq.Expressions.Expression exprBody)
        {
            _clauseReferences.OrderClause.Nodes.Add(new OrderClauseEntryNode()
            {
                Direction = OrderDirection.Desc,
                Node = ExpressionResolverMediator.ResolveExpression(exprBody)
            });
        }
    }
}