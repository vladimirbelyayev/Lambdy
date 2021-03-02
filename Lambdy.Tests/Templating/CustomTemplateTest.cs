using FluentAssertions;
using Lambdy.Constants;
using Lambdy.Tests.TestModels.Tables;
using Xunit;

namespace Lambdy.Tests.Templating
{
    public class CustomTemplateTest
    {
        [Fact]
        public void CustomTemplateShouldBeUsed()
        {
            var customTemplateText = "CustomTemplateString";
            var customTemplate = $"{customTemplateText} " +
                                 $"{LambdyTemplateTokens.Where}";
            
            var sqlResult = LambdyQuery
                .Create(new
                {
                    Table = (Person) null
                })
                .Template(customTemplate)
                .Where(x => x.Table.Name == "2")
                .Compile();

            sqlResult.Sql
                .Should()
                .Contain(customTemplateText);
        }
    }
}