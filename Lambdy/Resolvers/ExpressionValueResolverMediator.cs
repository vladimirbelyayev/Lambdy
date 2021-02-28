using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Lambdy.Resolvers.ExpressionValueResolvers;
using Lambdy.Resolvers.ExpressionValueResolvers.Abstract;

namespace Lambdy.Resolvers
{
    internal static class ExpressionValueResolverMediator
    {
        private static readonly NewArrayInitExpressionValueResolver NewArrayInitExpressionValueResolver =
            new NewArrayInitExpressionValueResolver();
        
        private static readonly ConstantExpressionValueResolver ConstantExpressionValueResolver =
            new ConstantExpressionValueResolver();
        
        private static readonly MethodCallExpressionValueResolver MethodCallExpressionValueResolver =
            new MethodCallExpressionValueResolver();
        
        private static readonly ListInitExpressionValueResolver ListInitExpressionValueResolver =
            new ListInitExpressionValueResolver();
        
        private static readonly MemberAccessExpressionValueResolver MemberAccessExpressionValueResolver =
            new MemberAccessExpressionValueResolver();
        
        private static readonly ConvertExpressionValueResolver ConvertExpressionValueResolver =
            new ConvertExpressionValueResolver();
        
        private static readonly IDictionary<ExpressionType, ExpressionValueResolver> Resolvers = 
            new Dictionary<ExpressionType, ExpressionValueResolver>
            {
                { ExpressionType.NewArrayInit, NewArrayInitExpressionValueResolver },
                { ExpressionType.ListInit, ListInitExpressionValueResolver },
                { ExpressionType.Constant, ConstantExpressionValueResolver },
                { ExpressionType.Call, MethodCallExpressionValueResolver },
                { ExpressionType.MemberAccess, MemberAccessExpressionValueResolver },
                { ExpressionType.Convert, ConvertExpressionValueResolver }
            };
        
        public static object GetValue(Expression expression)
        {
            try
            {
                return Resolvers[expression.NodeType]
                    .GetValue(expression);
            }
            catch (KeyNotFoundException)
            {
                throw new ArgumentException(
                    $"Expression '{expression.NodeType}' is currently not supported");
            }
        }
    }
}