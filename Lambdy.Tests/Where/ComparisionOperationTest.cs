﻿using System;
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
                .Create(new
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
                .Create(new
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
                    .Create(new
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
                .Create(new
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
                .Create(new
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
                .Create(new
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
                .Create(new
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
                .Create(new
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
                .Create(new
                {
                    Table = (Person) null
                })
                .Where(x => x.Table.Age != age)
                .Compile();

            sqlResult.Sql
                .Should()
                .Contain(expectedResult);
        }
    }
}