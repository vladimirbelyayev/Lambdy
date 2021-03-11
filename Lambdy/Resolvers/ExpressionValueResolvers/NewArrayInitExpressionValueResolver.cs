using System.Linq.Expressions;
using Lambdy.Resolvers.ExpressionValueResolvers.Abstract;

namespace Lambdy.Resolvers.ExpressionValueResolvers
{
    internal class NewArrayInitExpressionValueResolver : ExpressionValueResolver
    {
        public override object GetValue(Expression expression)
        {
            var newArrayExpression = (NewArrayExpression) expression;

            var arrayValueLength = newArrayExpression.Expressions.Count;
            var array = new object[arrayValueLength];
            for (var i = 0; i < arrayValueLength; i++)
            {
                array[i] = ExpressionValueResolverMediator.GetValue(newArrayExpression.Expressions[i]);
            }

            return array;
        }
    }
}