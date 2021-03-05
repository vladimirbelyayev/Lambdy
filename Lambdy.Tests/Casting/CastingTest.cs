using System;
using FluentAssertions;
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
    }
}