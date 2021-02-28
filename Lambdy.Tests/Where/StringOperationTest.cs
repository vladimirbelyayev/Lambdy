using FluentAssertions;
using Lambdy.Tests.TestModels.Tables;
using Xunit;

namespace Lambdy.Tests.Where
{
    public class StringOperationTest
    {
        [Fact]
        public void InlineStringContainsShouldCreateLike()
        {
            var expectedResult = $"Table.{nameof(Person.Name)} LIKE ";
            
            var sqlResult = LambdyQuery
                .Create(new
                {
                    Table = (Person) null
                })
                .Where(x => x.Table.Name.Contains("Test"))
                .Compile();

            sqlResult.Sql
                .Should()
                .Contain(expectedResult);
        }
    }
}