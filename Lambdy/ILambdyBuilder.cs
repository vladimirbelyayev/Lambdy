using Lambdy.Builders.SubBuilders.Raw.Interfaces;

namespace Lambdy
{
    public interface ILambdyBuilder<out TModel> : ILambdyBuilderCore
        where TModel: class
    {
        IRawBuilder<TModel> Raw { get; }

        ILambdyBuilder<TModel> WithTemplate(string sqlTemplate);

        ILambdyBuilder<TModel> InDialect(LambdySqlDialect dialect);
        
        ILambdyBuilder<TModel> Skip(int amount);

        ILambdyBuilder<TModel> Take(int amount);
    }
}