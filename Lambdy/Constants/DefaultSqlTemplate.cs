namespace Lambdy.Constants
{
    internal static class DefaultSqlTemplate
    {
        public static readonly string Sql = $"{LambdyTemplateTokens.Select}" +
                                            $"{LambdyTemplateTokens.From}" +
                                            $"{LambdyTemplateTokens.Joins}" +
                                            $"{LambdyTemplateTokens.Where}" +
                                            $"{LambdyTemplateTokens.OrderBy}" +
                                            $"{LambdyTemplateTokens.SkipTake}";
    }
}