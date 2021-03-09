using System;
using System.Text;
using Lambdy.Strategies.SkipTake;
using Lambdy.Strategies.SkipTake.Interfaces;

namespace Lambdy.Factories
{
    internal static class SqlDialectStrategyFactory
    {
        public static ISkipTakeStrategy CreateSkipTakeStrategy(
            LambdySqlDialect dialect, 
            StringBuilder stringBuilder)
        {
            switch (dialect)
            {
                case LambdySqlDialect.MsSql:
                    return new MsSqlSkipTakeStrategy(stringBuilder);
                case LambdySqlDialect.SqlLite:
                    return new SqlLiteSkipTakeStrategy(stringBuilder);
                default:
                    throw new ArgumentException("Invalid SQL dialect");
            }
        }
    }
}