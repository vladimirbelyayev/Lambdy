using System;
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
        
        [Fact]
        public void AndOrShouldBeProperlyBraced()
        {
            var expectedResult = $"WHERE Table.Name = {LambdyRegex.Params} " +
                                 $"AND \\(Table.Age = {LambdyRegex.Params} OR Table.Age = {LambdyRegex.Params}\\)";

            var sqlResult = LambdyQuery
                .ByModel(new
                {
                    Table = (Person) null
                })
                .Where(x => x.Table.Name == "Dummy" && (x.Table.Age == 22 || x.Table.Age == 25))
                .Compile();

            sqlResult.Sql
                .Should()
                .MatchRegex(expectedResult);
        }
        
        [Fact]
        public void MultiWhereAndOrShouldBeProperlyBraced()
        {
            var expectedResult = $"WHERE \\(Table.Name = {LambdyRegex.Params} " +
                                 $"AND \\(Table.Age = {LambdyRegex.Params} " +
                                 $"OR Table.Age = {LambdyRegex.Params}\\)\\) " +
                                 $"AND \\(Table.Surname = {LambdyRegex.Params} " +
                                 $"AND \\(Table.PersonAddressId =  {LambdyRegex.Params} " +
                                 $"OR Table.PersonAddressId =  {LambdyRegex.Params}\\)\\)";

            var sqlResult = LambdyQuery
                .ByModel(new
                {
                    Table = (Person) null
                })
                .Where(x => x.Table.Name == "Dummy" && (x.Table.Age == 22 || x.Table.Age == 25))
                .Where(x => x.Table.Surname == "Dummy2" && 
                            (x.Table.PersonAddressId == Guid.Empty || x.Table.PersonAddressId == Guid.Empty ))
                .Compile();

            sqlResult.Sql
                .Should()
                .MatchRegex(expectedResult);
        }
    }
}