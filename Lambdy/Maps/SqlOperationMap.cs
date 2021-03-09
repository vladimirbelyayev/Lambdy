using System.Collections.Generic;
using System.Linq.Expressions;
using Lambdy.Constants.Sql;

namespace Lambdy.Maps
{
    internal static class SqlOperationMap
    {
        public static readonly IReadOnlyDictionary<ExpressionType, string> Operations =
            new Dictionary<ExpressionType, string>()
            {
                {ExpressionType.Equal, SqlComparisionOperators.Equal},
                {ExpressionType.NotEqual, SqlComparisionOperators.NotEqual},
                {ExpressionType.GreaterThan, SqlComparisionOperators.GreaterThan},
                {ExpressionType.LessThan, SqlComparisionOperators.LessThan},
                {ExpressionType.GreaterThanOrEqual, SqlComparisionOperators.GreaterThanOrEqual},
                {ExpressionType.LessThanOrEqual, SqlComparisionOperators.LessThanOrEqual},
                {ExpressionType.AndAlso, SqlBooleanLogicalOperators.And},
                {ExpressionType.OrElse, SqlBooleanLogicalOperators.Or},
                {ExpressionType.Not, SqlBooleanLogicalOperators.Not},
                {ExpressionType.Convert, string.Empty} //We do not do conversion operations, just pass value!
            };
    }
}