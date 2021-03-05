using System;
using Lambdy.Compilers.Query;
using Lambdy.Compilers.Query.Abstract;

namespace Lambdy
{
    public static class LambdyQuery
    {
        private static QueryCompiler _queryCompiler = new RecursiveQueryCompiler();
        
        // ReSharper disable once UnusedMember.Global
        public static void SwitchCompiler(QueryCompilerAlgorithm queryCompilerAlgorithm)
        {
            switch (queryCompilerAlgorithm)
            {
                case QueryCompilerAlgorithm.Iterative:
                    // _queryCompiler = new IterativeQueryCompiler();
                    break;
                case QueryCompilerAlgorithm.Recursive:
                    _queryCompiler = new RecursiveQueryCompiler();
                    break;
                default:
                    throw new ArgumentException("Invalid compiler algorithm");
            }
        }

        public static LambdyBuilder<TModel> ByModel<TModel>(TModel model) where TModel: class
        {
            return ByModel<TModel>();
        }
        
        // ReSharper disable once MemberCanBePrivate.Global
        public static LambdyBuilder<TModel> ByModel<TModel>() where TModel: class
        {
            return new LambdyBuilder<TModel>(_queryCompiler);
        }
    }
}