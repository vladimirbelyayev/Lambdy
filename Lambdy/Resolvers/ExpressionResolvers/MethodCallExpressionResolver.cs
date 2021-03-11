using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using Lambdy.Maps;
using Lambdy.Resolvers.ExpressionResolvers.Abstract;
using Lambdy.Resolvers.ExpressionResolvers.SubResolvers.MethodCall;
using Lambdy.TreeNodes.ExpressionNodes.Abstract;

namespace Lambdy.Resolvers.ExpressionResolvers
{
    internal class MethodCallExpressionResolver : ExpressionResolver
    {
        private readonly LikeSubResolver _likeResolver = new LikeSubResolver();
        private readonly InSubResolver _inResolver = new InSubResolver();
        private readonly MethodResultSubResolver _resultSubResolver = new MethodResultSubResolver();

        public override ExpressionNode ResolveExpression(Expression expression)
        {
            var methodCallExpression = (MethodCallExpression) expression;
            
            if (methodCallExpression.Method.DeclaringType == typeof(string) && 
                LikeOperationMethodMap.OperationMethods.ContainsKey(methodCallExpression.Method.Name))
            {
                return _likeResolver.ResolveExpression(methodCallExpression);
            } 
            else if (typeof(IEnumerable).IsAssignableFrom(methodCallExpression.Method.DeclaringType) ||
                     typeof(Enumerable) == methodCallExpression.Method.DeclaringType)
            {
                return _inResolver.ResolveExpression(methodCallExpression);
            } 

            return _resultSubResolver.ResolveExpression(methodCallExpression);
        }
    }
}