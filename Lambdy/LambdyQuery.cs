using Lambdy.Builders;
using Lambdy.Compilers.Query;
using Lambdy.Compilers.Query.Abstract;

namespace Lambdy
{
    public static class LambdyQuery
    {
        private static readonly QueryCompiler QueryCompiler = new RecursiveQueryCompiler();
        
        public static ILambdyBuilder<TModel> ByModel<TModel>(TModel model) where TModel: class
        {
            return ByModel<TModel>();
        }
        
        public static ILambdyBuilder<TModel> ByModel<TModel>() where TModel: class
        {
            return new LambdyBuilder<TModel>(QueryCompiler);
        }
    }
}