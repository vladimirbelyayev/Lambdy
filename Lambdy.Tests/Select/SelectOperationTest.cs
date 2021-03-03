using System;
using FluentAssertions;
using Lambdy.Constants;
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
            var expectedResult = "SELECT Table.Name AS AliasName, " +
                                 "Table.Surname AS AliasSurname, " +
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
                .Contain(expectedResult);

        }
    }
}