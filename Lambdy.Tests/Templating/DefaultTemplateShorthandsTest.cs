using FluentAssertions;
using Lambdy.Tests.Constants;
using Lambdy.Tests.TestModels.Tables;
using Xunit;

namespace Lambdy.Tests.Templating
{
    public class DefaultTemplateShorthandsTest
    {
        [Fact]
        public void RawFromShouldBeUsed()
        {
            var expectedResult = $"SELECT Table.Age AS AgeAlias{LambdyRegex.NewLineOrWhitespace}" +
                                 $"FROM Person Table{LambdyRegex.NewLineOrWhitespace}" +
                                 $"WHERE";
            
            var sqlResult = LambdyQuery
                .ByModel(new
                {
                    Table = (Person) null
                })
                .Raw.From("FROM Person Table")
                .Where(x => x.Table.Name == "2")
                .Select(x => new { AgeAlias = x.Table.Age })
                .Compile();

            sqlResult.Sql
                .Should()
                .MatchRegex(expectedResult);
        }
        
        [Fact]
        public void RawJoinShouldBeUsed()
        {
            var expectedResult = $"SELECT Table.Age AS AgeAlias{LambdyRegex.NewLineOrWhitespace}" +
                                 $"FROM Person Table{LambdyRegex.NewLineOrWhitespace}" +
                                 $"JOIN Address AddressAlias " +
                                 $"ON Table.PersonAddressId = AddressAlias.Id{LambdyRegex.NewLineOrWhitespace}" +
                                 $"JOIN Address AddressAlias2 " +
                                 $"ON Table.PersonAddressId = AddressAlias2.Id{LambdyRegex.NewLineOrWhitespace}" +
                                 $"WHERE";
            
            var sqlResult = LambdyQuery
                .ByModel(new
                {
                    Table = (Person) null,
                    AddressAlias = (Address) null,
                    AddressAlias2 = (Address) null,
                })
                .Raw.From("FROM Person Table")
                .Raw.Join("JOIN Address AddressAlias ON Table.PersonAddressId = AddressAlias.Id")
                .Raw.Join("JOIN Address AddressAlias2 ON Table.PersonAddressId = AddressAlias2.Id")
                .Where(x => x.Table.Name == "2")
                .Select(x => new { AgeAlias = x.Table.Age })
                .Compile();

            sqlResult.Sql
                .Should()
                .MatchRegex(expectedResult);
        }
    }
}