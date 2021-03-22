using Lambdy.Builders.SubBuilders.Expressions;
using Lambdy.Builders.SubBuilders.Expressions.Interfaces;
using Lambdy.Builders.SubBuilders.Raw;
using Lambdy.Builders.SubBuilders.Raw.Interfaces;
using Lambdy.Compilers.Query.Abstract;
using Lambdy.Compilers.Query.Input;
using Lambdy.Parameters;
using Lambdy.TreeNodes.ClauseSectionNodes;
using Lambdy.TreeNodes.ClauseSectionNodes.Abstract;

namespace Lambdy.Builders
{
    internal sealed class LambdyBuilder<TModel> : ILambdyBuilder<TModel> 
        where TModel: class
    {
        private string _customTemplate;
        
        private readonly QueryCompiler _queryCompiler;
        
        private LambdySqlDialect _sqlDialect = LambdySqlDialect.MsSql;
        
        private readonly ParameterTracker _parameterTracker = new ParameterTracker();

        private readonly ClauseSectionNode[] _clauseSectionNodes = new ClauseSectionNode[6];
        private readonly SelectClauseNode _selectClause = new SelectClauseNode();
        private readonly FromClauseNode _fromClause = new FromClauseNode();
        private readonly JoinClauseNode _joinClause = new JoinClauseNode();
        private readonly WhereClauseNode _whereClause = new WhereClauseNode();
        private readonly OrderClauseNode _orderClause = new OrderClauseNode();
        private readonly SkipTakeClauseNode _skipTakeClause = new SkipTakeClauseNode();
        
        public IRawBuilder<TModel> Raw { get; }
        
        public IExpressionBuilder ExpressionBuilder { get; }

        internal LambdyBuilder(QueryCompiler queryCompiler)
        {
            _queryCompiler = queryCompiler;
            _clauseSectionNodes[0] = _selectClause;
            _clauseSectionNodes[1] = _fromClause;
            _clauseSectionNodes[2] = _joinClause;
            _clauseSectionNodes[3] = _whereClause;
            _clauseSectionNodes[4] = _orderClause;
            _clauseSectionNodes[5] = _skipTakeClause;

            Raw = new RawBuilder<TModel>(this,
                _parameterTracker,
                new RawBuilderClauseReferences
                {
                    SelectClause = _selectClause,
                    FromClause = _fromClause,
                    JoinClause = _joinClause,
                    WhereClause = _whereClause,
                    OrderClause = _orderClause,
                    SkipTakeClause = _skipTakeClause
                });

            ExpressionBuilder = new ExpressionBuilder(
                new ExpressionBuilderClauseReferences
                {
                    SelectClause = _selectClause,
                    FromClause = _fromClause,
                    JoinClause = _joinClause,
                    WhereClause = _whereClause,
                    OrderClause = _orderClause,
                    SkipTakeClause = _skipTakeClause
                });
        }

        public ILambdyBuilder<TModel> WithTemplate(string sqlTemplate)
        {
            _customTemplate = sqlTemplate;
            return this;
        }

        public ILambdyBuilder<TModel> InDialect(LambdySqlDialect dialect)
        {
            _sqlDialect = dialect;
            return this;
        }

        public ILambdyBuilder<TModel> Skip(int amount)
        {
            _skipTakeClause.Skip = amount;
            return this;
        }
        
        public ILambdyBuilder<TModel> Take(int amount)
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
                ClauseNodes = _clauseSectionNodes,
                RemoveEmptyTokens = true
            });
        }
        
        public LambdyResult Compile(LambdyCompilerOptions options)
        {
            return _queryCompiler.Compile(new QueryCompilerInput()
            {
                SqlDialect = _sqlDialect,
                SqlTemplate = _customTemplate,
                ParameterTracker = _parameterTracker,
                ClauseNodes = _clauseSectionNodes,
                RemoveEmptyTokens = options.RemoveEmptyTokens
            });
        }
        
    }
}