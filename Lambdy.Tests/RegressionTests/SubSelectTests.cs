using System;
using FluentAssertions;
using Lambdy.Tests.TestModels.Tables;
using Xunit;

namespace Lambdy.Tests.RegressionTests
{
    public class SubSelectTests
    {
        [Fact]
        public void RawSubSelectExistsShouldNotDisappear()
        {
            var sqlResult = LambdyQuery
                .ByModel(new
                {
                    Table = (Person) null,
                    Table2 = (Address) null,
                })
                .Raw.From($"FROM Person Table {Environment.NewLine}" +
                         $"JOIN Address Table2 {Environment.NewLine}")
                .Raw.Where("EXISTS (SELECT 1 FROM Customer Table3 WHERE Table3.Id = Table.Id)")
                .Select(x => new
                {
                    AliasName = x.Table.Name,
                    AliasSurname = x.Table.Surname,
                    AddressLine = x.Table2.AddressLine2
                })
                .Compile();

            sqlResult.Sql
                .Should()
                .Contain("EXISTS (SELECT 1 FROM Customer Table3 WHERE Table3.Id = Table.Id)");

        }
    }
}
