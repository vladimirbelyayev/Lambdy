using System;
using FluentAssertions;
using Lambdy.Tests.Constants;
using Lambdy.Tests.TestModels.Tables;
using Xunit;

namespace Lambdy.Tests.Templating
{
    public class DefaultTemplateRawShorthandsTest
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

        [Fact]
        public void RawOrderByShouldBeUsed()
        {
            var expectedResult = $"FROM Person Table{LambdyRegex.NewLineOrWhitespace}" +
                                 $"JOIN Address AddressAlias " +
                                 $"ON Table.PersonAddressId = AddressAlias.Id{LambdyRegex.NewLineOrWhitespace}" +
                                 $"ORDER BY AddressAlias2.Id ASC";

            var sqlResult = LambdyQuery
                .ByModel(new
                {
                    Table = (Person) null,
                    AddressAlias = (Address) null,
                })
                .Raw.From("FROM Person Table")
                .Raw.Join("JOIN Address AddressAlias ON Table.PersonAddressId = AddressAlias.Id")
                .Raw.OrderBy("ORDER BY AddressAlias2.Id ASC")
                .Compile();

            sqlResult.Sql
                .Should()
                .MatchRegex(expectedResult);
        }

        [Fact]
        public void RawWhereShouldBeUsed()
        {
            var expectedResult = $"WHERE \\(AddressAlias.{nameof(Person.Id)} = {LambdyRegex.Params}\\)"
                + LambdyRegex.NewLineOrWhitespace +
                $"AND \\(AddressAlias.AddressLine2 = 'AA'\\)"
                + LambdyRegex.NewLineOrWhitespace +
                $"AND \\(AddressAlias.AddressLine1 = 'AA2'\\)";

            var sqlResult = LambdyQuery
                .ByModel(new
                {
                    Table = (Person) null,
                    AddressAlias = (Address) null,
                })
                .Where(x => x.AddressAlias.Id == Guid.Empty)
                .Raw.Where("AddressAlias.AddressLine2 = 'AA'")
                .Raw.Where("AddressAlias.AddressLine1 = 'AA2'")
                .Compile();

            sqlResult.Sql
                .Should()
                .MatchRegex(expectedResult);
        }

        [Fact]
        public void RawWhereParamShouldBeAdded()
        {
            var sqlResult = LambdyQuery
                .ByModel(new
                {
                    Table = (Person) null,
                    AddressAlias = (Address) null,
                })
                .Where(x => x.AddressAlias.Id == Guid.Empty)
                .Raw.Where(
                    "AddressAlias.AddressLine2 = @MyParam",
                    new
                    {
                        MyParam = "Hello"
                    })
                .Compile();

            sqlResult.Parameters
                .Should()
                .ContainKey("@MyParam");
        }

        [Fact]
        public void RawOrderByParamShouldBeAdded()
        {
            var sqlResult = LambdyQuery
                .ByModel(new
                {
                    Table = (Person) null,
                    AddressAlias = (Address) null,
                })
                .Raw.OrderBy("ORDER BY @Something", new
                {
                    Something = 2
                })
                .Compile();

            sqlResult.Parameters
                .Should()
                .ContainKey("@Something");
        }
    }
}
