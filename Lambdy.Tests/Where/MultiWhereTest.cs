using FluentAssertions;
using Lambdy.Tests.TestModels.Tables;
using Xunit;

namespace Lambdy.Tests.Where
{
    public class MultiWhereTest
    {
        [Fact]
        public void MultiWhereShouldCreateAndWithBraces()
        {
            var expectedResult = $@"\(Table.{nameof(Person.Age)} = @p_[0-9]+\)[\n\r\s]+AND[\n\r\s]+\(Table.{nameof(Person.Name)} = @p_[0-9]+\)";

            var sqlResult = LambdyQuery
                .Create(new
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