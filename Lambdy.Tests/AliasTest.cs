using System;
using FluentAssertions;
using Lambdy.Tests.TestModels.Tables;
using Xunit;

namespace Lambdy.Tests
{
    public class AliasTest
    {
        [Fact]
        public void CreateShouldAlias()
        {
            var expectedResult = "T.Id";
            
            var sqlResult = LambdyQuery
                .ByModel(new
                {
                    TableOne = (Person) null,
                    T = (Address) null,
                    Table3 = (TableC) null,
                })
                .Where(x => x.T.Id == Guid.Empty)
                .Compile();



            sqlResult
                .Sql
                .Should()
                .Contain(expectedResult);
        }
    }
}