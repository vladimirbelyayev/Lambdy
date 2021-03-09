using System.Collections.Generic;
using System.Linq.Expressions;
using Lambdy.Constants.Sql;

namespace Lambdy.Maps
{
    internal static class SqlNullOperationMap
    {
        public static readonly IReadOnlyDictionary<ExpressionType, string> Operations =
            new Dictionary<ExpressionType, string>()
            {
                {ExpressionType.Equal, SqlComparisionOperators.IsNull},
                {ExpressionType.NotEqual, SqlComparisionOperators.IsNotNull}
            };
    }
}