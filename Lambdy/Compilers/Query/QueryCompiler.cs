using System.Text;
using Lambdy.Visitors.ClauseSectionSql;
using Lambdy.Visitors.ExpressionNodeSql;

namespace Lambdy.Compilers.Query
{
    internal class QueryCompiler
    {
        private readonly ExpressionNodeSqlVisitor _expressionNodeSqlVisitor;
        private readonly ClauseSectionSqlVisitor _clauseSectionSqlVisitor;

        public QueryCompiler()
        {
            var stringBuilder = new StringBuilder();
            _expressionNodeSqlVisitor = new ExpressionNodeSqlVisitor(stringBuilder);
            _clauseSectionSqlVisitor = new ClauseSectionSqlVisitor(
                _expressionNodeSqlVisitor,
                stringBuilder);
        }
        
        public LambdyResult Compile(QueryCompilerInput queryCompilerInput)
        {
            // Lock is needed for thread safety
            // as we are not creating new instances of visitors.
            // We could in theory spawn visitors on compile,
            // but that would hurt performance if compile() gets called multiple times
            // as we would need to allocate new visitors each time.
            lock (this)
            {
                _clauseSectionSqlVisitor.SetTemplate(queryCompilerInput.SqlTemplate);
                _expressionNodeSqlVisitor.SetParameterTracker(queryCompilerInput.ParameterTracker);

                // ReSharper disable once ForCanBeConvertedToForeach (reason - performance)
                for (var i = 0; i < queryCompilerInput.ClauseNodes.Length; i++)
                {
                    queryCompilerInput
                        .ClauseNodes[i]
                        .Accept(_clauseSectionSqlVisitor);
                }

                return new LambdyResult
                {
                    Sql = _clauseSectionSqlVisitor.Sql,
                    Parameters = _expressionNodeSqlVisitor.ParameterTracker.Parameters
                };
            }
        }
        
    }
}