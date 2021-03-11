using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Lambdy.Resolvers.ExpressionResolvers;
using Lambdy.Resolvers.ExpressionResolvers.Abstract;
using Lambdy.TreeNodes.ExpressionNodes.Abstract;

namespace Lambdy.Resolvers
{
    internal static class ExpressionResolverMediator
    {
        private static readonly BinaryExpressionResolver
            BinaryExpressionResolver = new BinaryExpressionResolver();
        
        private static readonly ConstantExpressionResolver 
            ConstantExpressionResolver = new ConstantExpressionResolver();
        
        private static readonly UnaryExpressionResolver 
            UnaryExpressionResolver = new UnaryExpressionResolver();
        
        private static readonly MethodCallExpressionResolver 
            MethodCallExpressionResolver = new MethodCallExpressionResolver();
        
        private static readonly MemberExpressionResolver 
            MemberExpressionResolver = new MemberExpressionResolver();
        
        private static readonly NewExpressionResolver 
            NewExpressionResolver = new NewExpressionResolver();

        private static readonly MemberInitExpressionResolver
            MemberInitExpressionResolver = new MemberInitExpressionResolver();

        private static readonly IDictionary<ExpressionType, ExpressionResolver> Resolvers = 
            new Dictionary<ExpressionType, ExpressionResolver>
        {
            { ExpressionType.Equal, BinaryExpressionResolver },
            { ExpressionType.NotEqual, BinaryExpressionResolver },
            { ExpressionType.LessThan, BinaryExpressionResolver },
            { ExpressionType.LessThanOrEqual, BinaryExpressionResolver },
            { ExpressionType.GreaterThan,BinaryExpressionResolver },
            { ExpressionType.GreaterThanOrEqual, BinaryExpressionResolver },
            { ExpressionType.And, BinaryExpressionResolver },
            { ExpressionType.AndAlso, BinaryExpressionResolver },
            { ExpressionType.Or, BinaryExpressionResolver },
            { ExpressionType.OrElse, BinaryExpressionResolver },
            { ExpressionType.Constant, ConstantExpressionResolver },
            { ExpressionType.PreIncrementAssign, UnaryExpressionResolver },
            { ExpressionType.PostIncrementAssign, UnaryExpressionResolver },
            { ExpressionType.PreDecrementAssign, UnaryExpressionResolver },
            { ExpressionType.PostDecrementAssign, UnaryExpressionResolver },
            { ExpressionType.Convert, UnaryExpressionResolver },
            { ExpressionType.Not, UnaryExpressionResolver },
            { ExpressionType.Call, MethodCallExpressionResolver },
            { ExpressionType.MemberAccess, MemberExpressionResolver },
            { ExpressionType.New, NewExpressionResolver },
            { ExpressionType.MemberInit, MemberInitExpressionResolver }
        };
        
        public static ExpressionNode ResolveExpression(Expression expression)
        {
            try
            {
                return Resolvers[expression.NodeType]
                    .ResolveExpression(expression);
            }
            catch (KeyNotFoundException)
            {
                throw new ArgumentException(
                    $"Expression '{expression.NodeType}' is currently not supported");
            }
        }
    }
}