using System;
using Lambdy.Constants.Sql;
using Lambdy.TreeNodes.ExpressionNodes;

namespace Lambdy.SubBuilders.Raw
{
    public class RawBuilder<TModel> where TModel: class
    {
        private readonly LambdyBuilder<TModel> _parentBuilder;
        private readonly RawBuilderClauseReferences _clauseReferences;
        
        internal RawBuilder(
            LambdyBuilder<TModel> parentBuilder,
            RawBuilderClauseReferences references)
        {
            _parentBuilder = parentBuilder;
            _clauseReferences = references;
        }

        public LambdyBuilder<TModel> From(string sqlFragment)
        {
            var fromIndex = sqlFragment.IndexOf(
                SqlClauses.From,
                StringComparison.InvariantCultureIgnoreCase);
            
            if (fromIndex >= 0)
            {
                var substringFrom = fromIndex + SqlClauses.From.Length + 1;
                sqlFragment = sqlFragment.Substring(substringFrom);
            }
            
            _clauseReferences.FromClause.Node = new RawNode(sqlFragment);
            return _parentBuilder;
        }
        
        public LambdyBuilder<TModel> Join(string sqlFragment)
        {
            _clauseReferences.JoinClause.Nodes.Add(new RawNode(sqlFragment));
            return _parentBuilder;
        }
    }
}