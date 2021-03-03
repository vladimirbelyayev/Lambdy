using FluentAssertions;
using Lambdy.Tests.Constants;
using Lambdy.Tests.TestModels.Tables;
using Xunit;

namespace Lambdy.Tests.Where
{
    public class BooleanLogicalOperationTest
    {
        [Fact]
        public void AndShouldBeCreated()
        {
            var expectedResult = $" {SqlBooleanLogicalOperators.And} ";

            var sqlResult = LambdyQuery
                .ByModel(new
                {
                    Table = (Person) null
                })
                .Where(x => x.Table.Name == "Dummy" && x.Table.Age == 22)
                .Compile();

            sqlResult.Sql
                .Should()
                .Contain(expectedResult);
        }
        
        [Fact]
        public void OrShouldBeCreated()
        {
            var expectedResult = $" {SqlBooleanLogicalOperators.Or} ";

            var sqlResult = LambdyQuery
                .ByModel(new
                {
                    Table = (Person) null
                })
                .Where(x => x.Table.Name == "Dummy" || x.Table.Age == 22)
                .Compile();

            sqlResult.Sql
                .Should()
                .Contain(expectedResult);
        }
        
        [Fact]
        public void NotShouldBeCreated()
        {
            var expectedResult = $"{SqlBooleanLogicalOperators.Not} ";

            var sqlResult = LambdyQuery
                .ByModel(new
                {
                    Table = (Person) null
                })
                // ReSharper disable once NegativeEqualityExpression
                .Where(x => !(x.Table.Name == "Dummy"))
                .Compile();

            sqlResult.Sql
                .Should()
                .Contain(expectedResult);
        }
    }
}