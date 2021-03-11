using System;
using System.Linq.Expressions;
using FluentAssertions;
using Lambdy.Tests.Constants;
using Lambdy.Tests.TestModels.Tables;
using Xunit;

namespace Lambdy.Tests.Templating
{
    public class TemplateWhereTest
    {
        [Fact]
        public void WhereClauseShouldBeCreated()
        {
            var expectedResult = $"{SqlClauses.Where} ";
            
            var sqlResult = LambdyQuery
                .ByModel(new
                {
                    Table = (Person) null
                })
                .Where(x => x.Table.Name == "2")
                .Compile();

            sqlResult.Sql
                .Should()
                .Contain(expectedResult);
        }
    }
}