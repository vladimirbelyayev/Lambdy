using FluentAssertions;
using Lambdy.Tests.TestModels.Tables;
using Xunit;

namespace Lambdy.Tests.Templating
{
    public class CustomTemplateTest
    {
        [Fact]
        public void CustomTemplateShouldBeUsed()
        {
            var customTemplateText = "SELECT Table.Id " +
                                     "FROM Person Table ";
            var customTemplate = $"{customTemplateText} " +
                                 $"{LambdyTemplateTokens.Where}";
            
            var sqlResult = LambdyQuery
                .ByModel(new
                {
                    Table = (Person) null
                })
                .WithTemplate(customTemplate)
                .Where(x => x.Table.Name == "2")
                .Compile();

            sqlResult.Sql
                .Should()
                .Contain(customTemplateText);
        }
    }
}