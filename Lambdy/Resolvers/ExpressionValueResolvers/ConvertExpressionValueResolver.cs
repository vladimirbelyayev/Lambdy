using System.Linq.Expressions;
using Lambdy.Resolvers.ExpressionValueResolvers.Abstract;

namespace Lambdy.Resolvers.ExpressionValueResolvers
{
    internal class ConvertExpressionValueResolver : ExpressionValueResolver
    {
        public override object GetValue(Expression expression)
        {
            var convertExpression = (UnaryExpression) expression;
            return ExpressionValueResolverMediator.GetValue(convertExpression.Operand);
        }
    }
}