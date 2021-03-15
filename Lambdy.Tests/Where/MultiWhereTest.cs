using FluentAssertions;
using Lambdy.Tests.Constants;
using Lambdy.Tests.TestModels.Tables;
using Xunit;

namespace Lambdy.Tests.Where
{
    public class MultiWhereTest
    {
        [Fact]
        public void MultiWhereShouldCreateAndWithBraces()
        {
            var expectedResult = $"\\(Table.{nameof(Person.Age)} = {LambdyRegex.Params}\\)" +
                                 $"{LambdyRegex.NewLineOrWhitespace}" +
                                 $"AND{LambdyRegex.NewLineOrWhitespace}" +
                                 $"\\(Table.{nameof(Person.Name)} = {LambdyRegex.Params}\\)";

            var sqlResult = LambdyQuery
                .ByModel(new
                {
                    Table = (Person) null
                })
                .Where(x => x.Table.Age == 5)
                .Where(x => x.Table.Name == "Name1")
                .Compile();

            sqlResult.Sql
                .Should()
                .MatchRegex(expectedResult);
        }
    }
}