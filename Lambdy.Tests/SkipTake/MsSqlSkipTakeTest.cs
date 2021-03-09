using FluentAssertions;
using Lambdy.Tests.TestModels.Tables;
using Xunit;

namespace Lambdy.Tests.SkipTake
{
    public class MsSqlSkipTakeTest
    {
        [Fact]
        public void SkipTakeShouldWork()
        {
            var expectedResult = $"OFFSET 10 ROWS FETCH NEXT 50 ROWS ONLY";
            var sqlResult = LambdyQuery
                .ByModel(new
                {
                    TableOne = (Person) null,
                    T = (Address) null,
                    Table3 = (TableC) null,
                })
                .Skip(10)
                .Take(50)
                .InDialect(LambdySqlDialect.MsSql)
                .Compile();
            
            sqlResult
                .Sql
                .Should()
                .Contain(expectedResult);
        }
    }
}