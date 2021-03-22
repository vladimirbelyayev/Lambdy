namespace Lambdy
{
    public static class LambdyBuilderCoreExtensions
    {
        public static ILambdyBuilder<TTarget> Cast<TTarget>(
            this ILambdyBuilderCore lambdyBuilder)
            where TTarget : class
        {
            return (ILambdyBuilder<TTarget>) lambdyBuilder;
        }
    }
}