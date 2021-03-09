using System;
using FluentAssertions;
using Lambdy.Tests.Constants;
using Lambdy.Tests.TestModels.Tables;
using Xunit;

namespace Lambdy.Tests.Select
{
    public class SelectOperationTest
    {
        [Fact]
        public void SelectShouldBeCreated()
        {
            var expectedResult = $"{SqlClauses.Select} Table.Name AS AliasName,[\\n\\r\\s]" +
                                 "Table.Surname AS AliasSurname,[\\n\\r\\s]" +
                                 "Table2.AddressLine2 AS AddressLine";
            
            var sqlResult = LambdyQuery
                .ByModel(new
                {
                    Table = (Person) null,
                    Table2 = (Address) null,
                })
                .WithTemplate($"{LambdyTemplateTokens.Select} " +
                              $"{SqlClauses.From} {nameof(Person)} Table")
                .Where(x => x.Table.Id == Guid.Empty)
                .Select(x => new
                {
                    AliasName = x.Table.Name,
                    AliasSurname = x.Table.Surname,
                    AddressLine = x.Table2.AddressLine2
                })
                .Compile();

            sqlResult.Sql
                .Should()
                .MatchRegex(expectedResult);

        }
    }
}