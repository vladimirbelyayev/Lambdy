﻿using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using Lambdy.ExpressionNodes.Abstract;
using Lambdy.Resolvers.ExpressionResolvers.Abstract;
using Lambdy.Resolvers.ExpressionResolvers.SubResolvers.MethodCall;
using Lambdy.ValueObjects;

namespace Lambdy.Resolvers.ExpressionResolvers
{
    internal class MethodCallExpressionResolver : ExpressionResolver
    {
        private readonly LikeSubResolver _likeResolver = new LikeSubResolver();
        private readonly InSubResolver _inResolver = new InSubResolver();
        private readonly MethodResultResolver _resultResolver = new MethodResultResolver();

        public override Node ResolveExpression(Expression expression)
        {
            var methodCallExpression = (MethodCallExpression) expression;
            
            if (methodCallExpression.Method.DeclaringType == typeof(string) && 
                Enum.TryParse(methodCallExpression.Method.Name, true, out LikeMethod _))
            {
                return _likeResolver.ResolveExpression(methodCallExpression);
            } 
            else if (typeof(IEnumerable).IsAssignableFrom(methodCallExpression.Method.DeclaringType) ||
                     typeof(Enumerable) == methodCallExpression.Method.DeclaringType)
            {
                return _inResolver.ResolveExpression(methodCallExpression);
            } 

            return _resultResolver.ResolveExpression(methodCallExpression);
        }
    }
}