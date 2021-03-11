using System;
using FluentAssertions;
using Lambdy.Tests.Constants;
using Lambdy.Tests.TestModels.Tables;
using Xunit;

namespace Lambdy.Tests.Where
{
    public class ComparisionOperationTest
    {
        [Fact]
        public void InlineEqualityShouldCreateEquality()
        {
            var expectedResult = $"Table.{nameof(Person.Name)} {SqlComparisionOperators.Equal} ";
            
            var sqlResult = LambdyQuery
                .ByModel(new
                {
                    Table = (Person) null
                })
                .Where(x => x.Table.Name == "test")
                .Compile();

            sqlResult.Sql
                .Should()
                .Contain(expectedResult);
        }
        
        [Fact]
        public void EqualityShouldCreateEquality()
        {
            var expectedResult = $"Table.{nameof(Person.Name)} {SqlComparisionOperators.Equal} ";
            var variableToEqual = "test";
            
            var sqlResult = LambdyQuery
                .ByModel(new
                {
                    Table = (Person) null
                })
                .Where(x => x.Table.Name == variableToEqual)
                .Compile();

            sqlResult.Sql
                .Should()
                .Contain(expectedResult);
        }
        
        [Fact]
        public void InlineGuidEmptyShouldNotThrowException()
        {
            Action act = () =>
            {
                LambdyQuery
                    .ByModel(new
                    {
                        Table = (Person) null
                    })
                    .Where(x => x.Table.Id == Guid.Empty)
                    .Compile();
            };
            
            act
                .Should()
                .NotThrow<Exception>();
        }
        
        [Fact]
        public void InlineLessThanShouldCreateLessThan()
        {
            var expectedResult = $"Table.{nameof(Person.Age)} {SqlComparisionOperators.LessThan} ";
            
            var sqlResult = LambdyQuery
                .ByModel(new
                {
                    Table = (Person) null
                })
                .Where(x => x.Table.Age < 5)
                .Compile();

            sqlResult.Sql
                .Should()
                .Contain(expectedResult);
        }
        
        [Fact]
        public void InlineLessThanOrEqualShouldCreateLessThanOrEqual()
        {
            var expectedResult = $"Table.{nameof(Person.Age)} {SqlComparisionOperators.LessThanOrEqual} ";
            
            var sqlResult = LambdyQuery
                .ByModel(new
                {
                    Table = (Person) null
                })
                .Where(x => x.Table.Age <= 5)
                .Compile();

            sqlResult.Sql
                .Should()
                .Contain(expectedResult);
        }
        
        [Fact]
        public void InlineGreaterThanShouldCreateGreaterThan()
        {
            var expectedResult = $"Table.{nameof(Person.Age)} {SqlComparisionOperators.GreaterThan} ";
            
            var sqlResult = LambdyQuery
                .ByModel(new
                {
                    Table = (Person) null
                })
                .Where(x => x.Table.Age > 5)
                .Compile();

            sqlResult.Sql
                .Should()
                .Contain(expectedResult);
        }
        
        [Fact]
        public void InlineGreaterThanOrEqualShouldCreateGreaterThanOrEqual()
        {
            var expectedResult = $"Table.{nameof(Person.Age)} {SqlComparisionOperators.GreaterThanOrEqual} ";
            
            var sqlResult = LambdyQuery
                .ByModel(new
                {
                    Table = (Person) null
                })
                .Where(x => x.Table.Age >= 5)
                .Compile();

            sqlResult.Sql
                .Should()
                .Contain(expectedResult);
        }
        
        [Fact]
        public void InlineNotEqualShouldCreateNotEqual()
        {
            var expectedResult = $"Table.{nameof(Person.Age)} {SqlComparisionOperators.NotEqual} ";
            
            var sqlResult = LambdyQuery
                .ByModel(new
                {
                    Table = (Person) null
                })
                .Where(x => x.Table.Age != 5)
                .Compile();

            sqlResult.Sql
                .Should()
                .Contain(expectedResult);
        }
        
        [Fact]
        public void NotEqualShouldCreateNotEqual()
        {
            var expectedResult = $"Table.{nameof(Person.Age)} {SqlComparisionOperators.NotEqual} ";
            var age = 5;
            
            var sqlResult = LambdyQuery
                .ByModel(new
                {
                    Table = (Person) null
                })
                .Where(x => x.Table.Age != age)
                .Compile();

            sqlResult.Sql
                .Should()
                .Contain(expectedResult);
        }
        
        [Fact]
        public void NullShouldCreateIsNull()
        {
            var expectedResult = $"Table.{nameof(Person.PersonAddressId)} IS NULL";

            var sqlResult = LambdyQuery
                .ByModel(new
                {
                    Table = (Person) null
                })
                .Where(x => x.Table.PersonAddressId == null)
                .Compile();

            sqlResult.Sql
                .Should()
                .Contain(expectedResult);
        }
        
        [Fact]
        public void NotNullShouldCreateIsNotNull()
        {
            var expectedResult = $"Table.{nameof(Person.PersonAddressId)} IS NOT NULL";

            var sqlResult = LambdyQuery
                .ByModel(new
                {
                    Table = (Person) null
                })
                .Where(x => x.Table.PersonAddressId != null)
                .Compile();

            sqlResult.Sql
                .Should()
                .Contain(expectedResult);
        }
        
        [Fact]
        public void NullableValuesShouldBeComparedCorrectly()
        {
            var expectedResult = $"{SqlClauses.Where} Table.{nameof(Person.Value)} = {LambdyRegex.Params}";

            var parameters = new
            {
                Value = (double?) 2
            };
            
            var sqlResult = LambdyQuery
                .ByModel(new
                {
                    Table = (Person) null
                })
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                .Where(x => x.Table.Value.Value == parameters.Value.Value)
                .Compile();

            sqlResult.Sql
                .Should()
                .MatchRegex(expectedResult);
        }
        
    }
}