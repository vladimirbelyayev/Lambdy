using System.Linq.Expressions;
using Lambdy.Resolvers.ExpressionValueResolvers.Abstract;

namespace Lambdy.Resolvers.ExpressionValueResolvers
{
    internal class NewArrayInitExpressionValueResolver : ExpressionValueResolver
    {
        public override object GetValue(Expression expression)
        {
            var newArrayExpression = (NewArrayExpression) expression;
                    
            var array = new object[newArrayExpression.Expressions.Count];
            var i = 0;
            foreach (var arrItemExpression in newArrayExpression.Expressions)
            {
                array[i++] = ExpressionValueResolverMediator.GetValue(arrItemExpression);
            }

            return array;
        }
    }
}