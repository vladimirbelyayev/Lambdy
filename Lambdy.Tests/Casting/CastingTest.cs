using System;
using FluentAssertions;
using Lambdy.Tests.Casting.Extensions;
using Lambdy.Tests.Casting.Models;
using Lambdy.Tests.TestModels.NorthwindTables;
using Xunit;

namespace Lambdy.Tests.Casting
{
    public class CastingTest
    {
        [Fact]
        public void WhereInputCastShouldWork()
        {
            var orderId = 222;
            
            Action act = () =>
            {
                LambdyQuery
                    .ByModel(new
                    {
                        Table = (OrderDetail) null
                    })
                    .Where(x => x.Table.OrderId == orderId)
                    .Compile();
            };
            
            act
                .Should()
                .NotThrow<Exception>();
        }
        
        [Fact]
        public void AllowChildToCallParentExtensions()
        {
            var expectedResult = "PersonAlias.Id = ";

            var sqlResult = LambdyQuery
                .ByModel<TreeTableJoin>()
                .FilterTwoTable()
                .Compile();

            sqlResult
                .Sql
                .Should()
                .Contain(expectedResult);
        }
        
        [Fact]
        public void CastBackToDerivedShouldWork()
        {
            var expectedResult = "PersonAlias.Id = ";

            var sqlResult = LambdyQuery
                .ByModel<TreeTableJoin>()
                .FilterTwoTable()
                .Cast<TreeTableJoin>()
                .Compile();

            sqlResult
                .Sql
                .Should()
                .Contain(expectedResult);
        }
    }
}