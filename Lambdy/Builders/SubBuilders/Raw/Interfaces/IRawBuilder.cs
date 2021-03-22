namespace Lambdy.Builders.SubBuilders.Raw.Interfaces
{
    public interface IRawBuilder<out TModel> 
        where TModel: class
    {
        ILambdyBuilder<TModel> From(
            string sqlFragment);

        ILambdyBuilder<TModel> Join(
            string sqlFragment);

        ILambdyBuilder<TModel> Where(
            string sqlFragment);

        ILambdyBuilder<TModel> Where(
            string sqlFragment,
            object parameters);

        ILambdyBuilder<TModel> OrderBy(
            string sqlFragment);

        ILambdyBuilder<TModel> OrderBy(
            string sqlFragment,
            object parameters);
    }
}