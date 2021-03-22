namespace Lambdy.Builders.SubBuilders.Expressions.Interfaces
{
    public interface IExpressionBuilder
    {
        void AddSelectExpression(System.Linq.Expressions.Expression exprBody);

        void AddWhereExpression(System.Linq.Expressions.Expression exprBody);

        void AddOrderByExpression(System.Linq.Expressions.Expression exprBody);

        void AddThenByExpression(System.Linq.Expressions.Expression exprBody);

        void AddOrderByDescExpression(System.Linq.Expressions.Expression exprBody);

        void AddThenByDescExpression(System.Linq.Expressions.Expression exprBody);
    }
}