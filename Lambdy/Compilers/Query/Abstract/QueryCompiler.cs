using Lambdy.Compilers.Query.Input;

namespace Lambdy.Compilers.Query.Abstract
{
    internal abstract class QueryCompiler
    {
        public abstract LambdyResult Compile(QueryCompilerInput queryCompilerInput);
    }
}