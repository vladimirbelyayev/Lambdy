namespace Lambdy
{
    public static class LambdyQuery
    {
        public static LambdaBuilder<TModel> Create<TModel>(TModel model) where TModel: class
        {
            return Create<TModel>();
        }
        
        public static LambdaBuilder<TModel> Create<TModel>() where TModel: class
        {
            return new LambdaBuilder<TModel>();
        }
    }
}