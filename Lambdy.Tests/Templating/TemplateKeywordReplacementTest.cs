using FluentAssertions;
using Lambdy.Tests.TestModels.Tables;
using Xunit;

namespace Lambdy.Tests.Templating
{
    public class TemplateKeywordReplacementTest
    {
        [Fact]
        public void CustomTemplateDoubleSelectKeywordShouldNotBeCreated()
        {
            var unexpectedResult = "SELECT SELECT ";
            
            var customTemplate = $"SELECT {LambdyTemplateTokens.Select}";
            
            var sqlResult = LambdyQuery
                .ByModel(new
                {
                    Table = (Person) null
                })
                .WithTemplate(customTemplate)
                .Select(x => new { x.Table.Id })
                .Compile();

            sqlResult.Sql
                .Should()
                .NotContain(unexpectedResult);
        }
        
        [Fact]
        public void CustomTemplateDoubleWhereKeywordShouldNotBeCreated()
        {
            var unexpectedResult = "WHERE WHERE ";
            
            var customTemplate = $"WHERE {LambdyTemplateTokens.Where}";
            
            var sqlResult = LambdyQuery
                .ByModel(new
                {
                    Table = (Person) null
                })
                .WithTemplate(customTemplate)
                .Where(x => x.Table.Age == 54)
                .Compile();

            sqlResult.Sql
                .Should()
                .NotContain(unexpectedResult);
        }
        
        [Fact]
        public void CustomTemplateDoubleOrderByKeywordShouldNotBeCreated()
        {
            var unexpectedResult = "ORDER BY ORDER BY ";
            
            var customTemplate = $"ORDER BY {LambdyTemplateTokens.OrderBy}";
            
            var sqlResult = LambdyQuery
                .ByModel(new
                {
                    Table = (Person) null
                })
                .WithTemplate(customTemplate)
                .OrderBy(x => x.Table.Age)
                .Compile();

            sqlResult.Sql
                .Should()
                .NotContain(unexpectedResult);
        }
    }
}