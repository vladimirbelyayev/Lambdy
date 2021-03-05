using System;
using System.Linq.Expressions;
using Lambdy.Compilers.Query.Abstract;
using Lambdy.Compilers.Query.Input;
using Lambdy.Parameters;
using Lambdy.Resolvers;
using Lambdy.TreeNodes.ClauseSectionNodes;
using Lambdy.TreeNodes.ClauseSectionNodes.Abstract;

namespace Lambdy
{
    public sealed class LambdyBuilder<TModel> where TModel: class
    {
        private string _customTemplate;
        
        private readonly QueryCompiler _queryCompiler;
        
        private readonly ParameterTracker _parameterTracker = new ParameterTracker();

        private readonly ClauseSectionNode[] _clauseSectionNodes = new ClauseSectionNode[4];
        private readonly SelectClauseNode _selectClause = new SelectClauseNode();
        private readonly FromClauseNode _fromClause = new FromClauseNode();
        private readonly JoinClauseNode _joinClause = new JoinClauseNode();
        private readonly WhereClauseNode _whereClause = new WhereClauseNode();
        
        internal LambdyBuilder(QueryCompiler queryCompiler)
        {
            _queryCompiler = queryCompiler;
            _clauseSectionNodes[0] = _selectClause;
            _clauseSectionNodes[1] = _fromClause;
            _clauseSectionNodes[2] = _joinClause;
            _clauseSectionNodes[3] = _whereClause;
        }
        
        public LambdyBuilder<TModel> Where(Expression<Func<TModel, bool>> expression)
        {
            _whereClause.Nodes
                .Add(ExpressionResolverMediator.ResolveExpression(expression.Body));
            
            return this;
        }

        public LambdyBuilder<TModel> Select<TSelectModel>(Expression<Func<TModel, TSelectModel>> expression)
        {
            _selectClause.Node = ExpressionResolverMediator
                .ResolveExpression(expression.Body);

            return this;
        }

        public LambdyBuilder<TModel> WithTemplate(string sqlTemplate)
        {
            _customTemplate = sqlTemplate;
            return this;
        }

        public LambdyResult Compile()
        {
            return _queryCompiler.Compile(new QueryCompilerInput()
            {
                SqlTemplate = _customTemplate,
                ParameterTracker = _parameterTracker,
                ClauseNodes = _clauseSectionNodes
            });
        }
        
    }
}