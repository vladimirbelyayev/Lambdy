using System;
using FluentAssertions;
using Lambdy.Tests.Alias.Models;
using Lambdy.Tests.TestModels.Tables;
using Xunit;

namespace Lambdy.Tests.Alias
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
                    Table3 = (Customer) null,
                })
                .Where(x => x.T.Id == Guid.Empty)
                .Compile();
            
            sqlResult
                .Sql
                .Should()
                .Contain(expectedResult);
        }
        
        [Fact]
        public void CreateByClassShouldAlias()
        {
            var expectedResult = "PersonAlias.Id = ";

            var sqlResult = LambdyQuery
                .ByModel<SimpleSelectAlias>()
                .Where(x => x.PersonAlias.Id == Guid.Empty)
                .Compile();
            
            sqlResult
                .Sql
                .Should()
                .Contain(expectedResult);
        }
    }
}