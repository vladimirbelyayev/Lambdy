using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Lambdy.Tests.Constants;
using Lambdy.Tests.TestModels.Tables;
using Xunit;

namespace Lambdy.Tests.Where
{
    public class CollectionTest
    {
        [Fact]
        public void InlineArrayShouldCreateIn()
        {
            var expectedResult = $"Table.{nameof(Person.Name)} {SqlComparisionOperators.In} ";
            
            var sqlResult = LambdyQuery
                .ByModel(new
                {
                    Table = (Person) null
                })
                .Where(x => new[] {"1", "2", "3", "5"}.Contains(x.Table.Name))
                .Compile();

            sqlResult.Sql
                .Should()
                .Contain(expectedResult);
        }
        
        [Fact]
        public void ArrayShouldCreateIn()
        {
            var expectedResult = $"Table.{nameof(Person.Name)} {SqlComparisionOperators.In} ";
            var arr = new[] {"1", "2", "3", "5"};
            
            var sqlResult = LambdyQuery
                .ByModel(new
                {
                    Table = (Person) null
                })
                .Where(x => arr.Contains(x.Table.Name))
                .Compile();

            sqlResult.Sql
                .Should()
                .Contain(expectedResult);
        }
        
        [Fact]
        public void InlineListShouldCreateIn()
        {
            var expectedResult = $"Table.{nameof(Person.Name)} {SqlComparisionOperators.In} ";

            var sqlResult = LambdyQuery
                .ByModel(new
                {
                    Table = (Person) null
                })
                .Where(x => new List<string>() {"1", "2", "3", "5"}.Contains(x.Table.Name))
                .Compile();

            sqlResult.Sql
                .Should()
                .Contain(expectedResult);
        }
        
        [Fact]
        public void ListShouldCreateIn()
        {
            var expectedResult = $"Table.{nameof(Person.Name)} {SqlComparisionOperators.In} ";
            var list = new List<string>() {"1", "2", "3", "5"};
            
            var sqlResult = LambdyQuery
                .ByModel(new
                {
                    Table = (Person) null
                })
                .Where(x => list.Contains(x.Table.Name))
                .Compile();

            sqlResult.Sql
                .Should()
                .Contain(expectedResult);
        }
    }
}