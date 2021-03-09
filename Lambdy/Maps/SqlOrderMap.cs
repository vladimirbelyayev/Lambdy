﻿using System.Collections.Generic;
using Lambdy.Constants.Sql;
using Lambdy.ValueObjects;

namespace Lambdy.Maps
{
    internal class SqlOrderMap
    {
        public static readonly IReadOnlyDictionary<OrderDirection, string> Orders =
            new Dictionary<OrderDirection, string>()
            {
                {OrderDirection.Asc, SqlOrderKeywords.Asc},
                {OrderDirection.Desc, SqlOrderKeywords.Desc}
            };
    }
}