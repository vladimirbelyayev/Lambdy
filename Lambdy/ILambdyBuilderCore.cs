using Lambdy.Builders.SubBuilders.Expressions.Interfaces;

namespace Lambdy
{
    public interface ILambdyBuilderCore
    {
        IExpressionBuilder ExpressionBuilder { get; }

        LambdyResult Compile();

        LambdyResult Compile(LambdyCompilerOptions options);
    }
}