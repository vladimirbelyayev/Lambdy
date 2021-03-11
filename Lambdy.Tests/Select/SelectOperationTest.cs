using System;
using FluentAssertions;
using Lambdy.Tests.Constants;
using Lambdy.Tests.Select.Models;
using Lambdy.Tests.TestModels.Tables;
using Xunit;

namespace Lambdy.Tests.Select
{
    public class SelectOperationTest
    {
        [Fact]
        public void AnonymousSelectShouldBeCreated()
        {
            var expectedResult = $"{SqlClauses.Select} Table.Name AS AliasName,{LambdyRegex.NewLineOrWhitespace}" +
                                 $"Table.Surname AS AliasSurname,{LambdyRegex.NewLineOrWhitespace}" +
                                 "Table2.AddressLine2 AS AddressLine";
            
            var sqlResult = LambdyQuery
                .ByModel(new
                {
                    Table = (Person) null,
                    Table2 = (Address) null,
                })
                .WithTemplate($"{LambdyTemplateTokens.Select} " +
                              $"{SqlClauses.From} {nameof(Person)} Table")
                .Where(x => x.Table.Id == Guid.Empty)
                .Select(x => new
                {
                    AliasName = x.Table.Name,
                    AliasSurname = x.Table.Surname,
                    AddressLine = x.Table2.AddressLine2
                })
                .Compile();

            sqlResult.Sql
                .Should()
                .MatchRegex(expectedResult);

        }
        
        [Fact]
        public void TypedSelectShouldBeCreated()
        {
            var expectedResult = $"{SqlClauses.Select} Table.Name AS AliasName,{LambdyRegex.NewLineOrWhitespace}" +
                                 $"Table.Surname AS AliasSurname,{LambdyRegex.NewLineOrWhitespace}" +
                                 "Table2.AddressLine2 AS AddressLine2";
            
            var sqlResult = LambdyQuery
                .ByModel(new
                {
                    Table = (Person) null,
                    Table2 = (Address) null,
                })
                .WithTemplate($"{LambdyTemplateTokens.Select} " +
                              $"{SqlClauses.From} {nameof(Person)} Table")
                .Where(x => x.Table.Id == Guid.Empty)
                .Select(x => new SimplePersonResult
                {
                    AliasName = x.Table.Name,
                    AliasSurname = x.Table.Surname,
                    AddressLine2 = x.Table2.AddressLine2
                })
                .Compile();

            sqlResult.Sql
                .Should()
                .MatchRegex(expectedResult);

        }
        
        [Fact]
        public void SelectByClassShouldNotThrowException()
        {
            Action act = () =>
            {
                LambdyQuery
                    .ByModel(new
                    {
                        Table = (Person) null,
                        Table2 = (Address) null,
                    })
                    .WithTemplate($"{LambdyTemplateTokens.Select} " +
                                  $"{SqlClauses.From} {nameof(Person)} Table")
                    .Where(x => x.Table.Id == Guid.Empty)
                    .Select(x => new SimplePersonResult
                    {
                        AliasName = x.Table.Name,
                        AliasSurname = x.Table.Surname,
                        AddressLine2 = x.Table2.AddressLine2
                    })
                    .Compile();
            };
            
            act
                .Should()
                .NotThrow<Exception>();
        }
    }
}