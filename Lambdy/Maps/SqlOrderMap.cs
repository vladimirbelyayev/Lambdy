using System.Collections.Generic;
using Lambdy.Constants.Sql;
using Lambdy.ValueObjects;

namespace Lambdy.Maps
{
    internal static class SqlOrderMap
    {
        public static readonly IReadOnlyDictionary<OrderDirection, string> Orders =
            new Dictionary<OrderDirection, string>()
            {
                {OrderDirection.Undefined, string.Empty},
                {OrderDirection.Asc, SqlOrderKeywords.Asc},
                {OrderDirection.Desc, SqlOrderKeywords.Desc}
            };
    }
}