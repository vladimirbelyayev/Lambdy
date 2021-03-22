using System;
using System.Linq.Expressions;

namespace Lambdy
{
    public static class LambdyBuilderExtensions
    {
        public static ILambdyBuilder<TModel> Select<TModel, TSelectModel>(
            this ILambdyBuilder<TModel> lambdyBuilder,
            Expression<Func<TModel, TSelectModel>> expression)
            where TModel: class
        {
            lambdyBuilder
                .ExpressionBuilder
                .AddSelectExpression(expression.Body);
            return lambdyBuilder;
        }
        
        public static ILambdyBuilder<TModel> Where<TModel>(
            this ILambdyBuilder<TModel> lambdyBuilder,
            Expression<Func<TModel, bool>> expression) 
            where TModel: class
        {
            lambdyBuilder
                .ExpressionBuilder
                .AddWhereExpression(expression.Body);
            return lambdyBuilder;
        }

        public static ILambdyBuilder<TModel> OrderBy<TModel, TKey>(
            this ILambdyBuilder<TModel> lambdyBuilder,
            Expression<Func<TModel, TKey>> expression)
            where TModel : class
        {
            lambdyBuilder
                .ExpressionBuilder
                .AddOrderByExpression(expression.Body);
            return lambdyBuilder;
        }

        public static ILambdyBuilder<TModel> ThenBy<TModel, TKey>(
            this ILambdyBuilder<TModel> lambdyBuilder,
            Expression<Func<TModel, TKey>> expression)
            where TModel : class
        {
            lambdyBuilder
                .ExpressionBuilder
                .AddThenByExpression(expression.Body);
            return lambdyBuilder;
        }

        public static ILambdyBuilder<TModel> OrderByDescending<TModel, TKey>(
            this ILambdyBuilder<TModel> lambdyBuilder,
            Expression<Func<TModel, TKey>> expression)
            where TModel : class
        {
            lambdyBuilder
                .ExpressionBuilder
                .AddOrderByDescExpression(expression.Body);
            return lambdyBuilder;
        }

        public static ILambdyBuilder<TModel> ThenByDescending<TModel, TKey>(
            this ILambdyBuilder<TModel> lambdyBuilder,
            Expression<Func<TModel, TKey>> expression)
            where TModel : class
        {
            lambdyBuilder
                .ExpressionBuilder
                .AddThenByDescExpression(expression.Body);
            return lambdyBuilder;
        }


    }
}