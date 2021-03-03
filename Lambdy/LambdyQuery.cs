using Lambdy.Compilers.Query;

namespace Lambdy
{
    public static class LambdyQuery
    {
        private static readonly QueryCompiler QueryCompiler = new QueryCompiler();
        
        public static LambdyBuilder<TModel> ByModel<TModel>(TModel model) where TModel: class
        {
            return ByModel<TModel>();
        }
        
        public static LambdyBuilder<TModel> ByModel<TModel>() where TModel: class
        {
            return new LambdyBuilder<TModel>(QueryCompiler);
        }
    }
}