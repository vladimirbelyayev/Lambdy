using Lambdy.Compilers.Query;

namespace Lambdy
{
    public static class LambdyQuery
    {
        private static readonly QueryCompiler QueryCompiler = new QueryCompiler();
        
        public static LambdyBuilder<TModel> Create<TModel>(TModel model) where TModel: class
        {
            return Create<TModel>();
        }
        
        public static LambdyBuilder<TModel> Create<TModel>() where TModel: class
        {
            return new LambdyBuilder<TModel>(QueryCompiler);
        }
    }
}