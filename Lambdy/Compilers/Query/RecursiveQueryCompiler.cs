using System.Text;
using Lambdy.Compilers.Query.Abstract;
using Lambdy.Compilers.Query.Input;
using Lambdy.Visitors.ClauseSectionSql;
using Lambdy.Visitors.ExpressionNodeSql;

namespace Lambdy.Compilers.Query
{
    internal class RecursiveQueryCompiler : QueryCompiler
    {
        public override LambdyResult Compile(QueryCompilerInput queryCompilerInput)
        {
            var stringBuilder = new StringBuilder();
            var expressionNodeSqlVisitor = new RecursiveNodeSqlVisitor(stringBuilder);
            var clauseSectionSqlVisitor = new RecursiveClauseSectionSqlVisitor(
                expressionNodeSqlVisitor,
                stringBuilder);
            
            clauseSectionSqlVisitor.SetTemplate(queryCompilerInput.SqlTemplate);
            expressionNodeSqlVisitor.SetParameterTracker(queryCompilerInput.ParameterTracker);

            // ReSharper disable once ForCanBeConvertedToForeach (reason - performance)
            for (var i = 0; i < queryCompilerInput.ClauseNodes.Length; i++)
            {
                queryCompilerInput
                    .ClauseNodes[i]
                    .Accept(clauseSectionSqlVisitor);
            }

            return new LambdyResult
            {
                Sql = clauseSectionSqlVisitor.Sql,
                Parameters = expressionNodeSqlVisitor.ParameterTracker.Parameters
            };
        }
    }
}