using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Lambdy.Resolvers.ExpressionValueResolvers.Abstract;

namespace Lambdy.Resolvers.ExpressionValueResolvers
{
    internal class ListInitExpressionValueResolver : ExpressionValueResolver
    {
        public override object GetValue(Expression expression)
        {
            var listInitExpression = (ListInitExpression) expression;
            var list = new List<object>(listInitExpression.Initializers.Count);

            for (var i = 0; i < list.Capacity; i++)
            {
                list.Add(ExpressionValueResolverMediator
                    .GetValue(listInitExpression.Initializers[i].Arguments.Single()));
            }

            return list;
        }
    }
}