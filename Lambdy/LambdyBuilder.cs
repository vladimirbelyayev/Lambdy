using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Lambdy.Compilers.Query.Abstract;
using Lambdy.Compilers.Query.Input;
using Lambdy.Parameters;
using Lambdy.Resolvers;
using Lambdy.TreeNodes.ClauseSectionNodes;
using Lambdy.TreeNodes.ClauseSectionNodes.Abstract;
using Lambdy.ValueObjects;

namespace Lambdy
{
    public sealed class LambdyBuilder<TModel> where TModel: class
    {
        private string _customTemplate;
        
        private readonly QueryCompiler _queryCompiler;
        
        private readonly ParameterTracker _parameterTracker = new ParameterTracker();

        private LambdySqlDialect _sqlDialect = LambdySqlDialect.MsSql;

        private readonly ClauseSectionNode[] _clauseSectionNodes = new ClauseSectionNode[6];
        private readonly SelectClauseNode _selectClause = new SelectClauseNode();
        private readonly FromClauseNode _fromClause = new FromClauseNode();
        private readonly JoinClauseNode _joinClause = new JoinClauseNode();
        private readonly WhereClauseNode _whereClause = new WhereClauseNode();
        private readonly OrderClauseNode _orderClause = new OrderClauseNode();
        private readonly SkipTakeClauseNode _skipTakeClause = new SkipTakeClauseNode();
        
        internal LambdyBuilder(QueryCompiler queryCompiler)
        {
            _queryCompiler = queryCompiler;
            _clauseSectionNodes[0] = _selectClause;
            _clauseSectionNodes[1] = _fromClause;
            _clauseSectionNodes[2] = _joinClause;
            _clauseSectionNodes[3] = _whereClause;
            _clauseSectionNodes[4] = _orderClause;
            _clauseSectionNodes[5] = _skipTakeClause;
        }
        
        public LambdyBuilder<TModel> WithTemplate(string sqlTemplate)
        {
            _customTemplate = sqlTemplate;
            return this;
        }

        public LambdyBuilder<TModel> InDialect(LambdySqlDialect dialect)
        {
            _sqlDialect = dialect;
            return this;
        }
        
        public LambdyBuilder<TModel> Select<TSelectModel>(Expression<Func<TModel, TSelectModel>> expression)
        {
            _selectClause.Node = ExpressionResolverMediator
                .ResolveExpression(expression.Body);

            return this;
        }
        
        public LambdyBuilder<TModel> Where(Expression<Func<TModel, bool>> expression)
        {
            _whereClause.Nodes
                .Add(ExpressionResolverMediator.ResolveExpression(expression.Body));
            
            return this;
        }

        public LambdyBuilder<TModel> OrderBy<TKey>(Expression<Func<TModel, TKey>> expression)
        {
            _orderClause.Nodes = new List<OrderClauseEntryNode>()
            {
                new OrderClauseEntryNode()
                {
                    Node = ExpressionResolverMediator.ResolveExpression(expression.Body),
                    Direction = OrderDirection.Asc
                }
            };
            
            return this;
        }

        public LambdyBuilder<TModel> ThenBy<TKey>(Expression<Func<TModel, TKey>> expression)
        {
            _orderClause.Nodes.Add(new OrderClauseEntryNode()
            {
                Direction = OrderDirection.Asc,
                Node = ExpressionResolverMediator.ResolveExpression(expression.Body)
            });

            return this;
        }
        
        public LambdyBuilder<TModel> OrderByDescending<TKey>(Expression<Func<TModel, TKey>> expression)
        {
            _orderClause.Nodes = new List<OrderClauseEntryNode>()
            {
                new OrderClauseEntryNode()
                {
                    Node = ExpressionResolverMediator.ResolveExpression(expression.Body),
                    Direction = OrderDirection.Desc
                }
            };
            
            return this;
        }

        public LambdyBuilder<TModel> ThenByDescending<TKey>(Expression<Func<TModel, TKey>> expression)
        {
            _orderClause.Nodes.Add(new OrderClauseEntryNode()
            {
                Direction = OrderDirection.Desc,
                Node = ExpressionResolverMediator.ResolveExpression(expression.Body)
            });

            return this;
        }

        public LambdyBuilder<TModel> Skip(int amount)
        {
            _skipTakeClause.Skip = amount;
            return this;
        }
        
        public LambdyBuilder<TModel> Take(int amount)
        {
            _skipTakeClause.Take = amount;
            return this;
        }
        
        public LambdyResult Compile()
        {
            return _queryCompiler.Compile(new QueryCompilerInput()
            {
                SqlDialect = _sqlDialect,
                SqlTemplate = _customTemplate,
                ParameterTracker = _parameterTracker,
                ClauseNodes = _clauseSectionNodes
            });
        }
        
    }
}