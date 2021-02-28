using System.Linq;
using System.Linq.Expressions;
using Lambdy.Resolvers.ExpressionValueResolvers.Abstract;

namespace Lambdy.Resolvers.ExpressionValueResolvers
{
    internal class MethodCallExpressionValueResolver : ExpressionValueResolver
    {
        public override object GetValue(Expression expression)
        {
            var callExpression = (MethodCallExpression) expression;
            
            var arguments = callExpression.Arguments
                .Select(ExpressionValueResolverMediator.GetValue)
                .ToArray();
            
            var obj = callExpression.Object != null 
                ? ExpressionValueResolverMediator.GetValue(callExpression.Object) 
                : arguments.First();

            return callExpression.Method.Invoke(obj, arguments);
        }
    }
}