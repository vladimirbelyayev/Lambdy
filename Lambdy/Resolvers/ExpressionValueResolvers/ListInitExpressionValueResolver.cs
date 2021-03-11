using System.Collections.Generic;
using System.Linq.Expressions;
using Lambdy.Resolvers.ExpressionValueResolvers.Abstract;

namespace Lambdy.Resolvers.ExpressionValueResolvers
{
    internal class ListInitExpressionValueResolver : ExpressionValueResolver
    {
        public override object GetValue(Expression expression)
        {
            var listInitExpression = (ListInitExpression) expression;
            var listLength = listInitExpression.Initializers.Count;
            var list = new List<object>(listLength);

            for (var i = 0; i < listLength; i++)
            {
                list.Add(ExpressionValueResolverMediator
                    .GetValue(listInitExpression.Initializers[i].Arguments[0]));
            }

            return list;
        }
    }
}