using FluentAssertions;
using Lambdy.Tests.TestModels.Tables;
using Xunit;

namespace Lambdy.Tests.SkipTake
{
    public class SqlLiteSkipTakeTest
    {
        [Fact]
        public void SkipTakeShouldWork()
        {
            var expectedResult = $"LIMIT 50 OFFSET 10";

            var sqlResult = LambdyQuery
                .ByModel(new
                {
                    TableOne = (Person) null,
                    T = (Address) null,
                    Table3 = (Customer) null,
                })
                .Skip(10)
                .Take(50)
                .InDialect(LambdySqlDialect.SqlLite)
                .Compile();
            
            sqlResult
                .Sql
                .Should()
                .Contain(expectedResult);
        }
    }
}