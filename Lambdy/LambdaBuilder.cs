using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Lambdy.ExpressionNodes.Abstract;
using Lambdy.Resolvers;
using Lambdy.Visitors;

namespace Lambdy
{
    public class LambdaBuilder<TModel> where TModel: class
    {
        private readonly List<Node> _whereRootNodes = new List<Node>();

        private readonly ParameterTracker _parameterTracker = new ParameterTracker();
        
        internal LambdaBuilder()
        {
        }
        
        public LambdaBuilder<TModel> Where(Expression<Func<TModel, bool>> expression)
        {
            _whereRootNodes
                .Add(ExpressionResolverMediator.ResolveExpression(expression.Body));
            
            return this;
        }

        public LambdaSqlResult Compile()
        {
            var visitor = new ExpressionNodeSqlVisitor(_parameterTracker);
            var nodeWheres = _whereRootNodes
                .Select(x => $"({x.Accept(visitor)})");

            var sql = $"WHERE {string.Join($"{Environment.NewLine}AND ", nodeWheres)}";

            return new LambdaSqlResult
            {
                Sql = sql,
                Parameters = _parameterTracker.Parameters
            };
        }
    }
}