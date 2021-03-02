using System.Linq;
using System.Linq.Expressions;
using Lambdy.Resolvers.NameResolvers;
using Lambdy.TreeNodes.ExpressionNodes;
using Lambdy.TreeNodes.ExpressionNodes.Abstract;

namespace Lambdy.Resolvers.ExpressionResolvers.SubResolvers.MethodCall
{
    internal class InSubResolver
    {
        public ExpressionNode ResolveExpression(MethodCallExpression methodCallExpression)
        {
            Expression collectionExpression;
            MemberExpression memberExpression;
            
            if (methodCallExpression.Method.DeclaringType == typeof(Enumerable))
            {
                collectionExpression = methodCallExpression.Arguments[0];
                memberExpression = (MemberExpression) methodCallExpression.Arguments[1];
            }
            else
            {
                collectionExpression = methodCallExpression.Object;
                memberExpression = (MemberExpression) methodCallExpression.Arguments[0];
            }

            var value = ExpressionValueResolverMediator.GetValue(collectionExpression);
            
            return new InNode()
            {
                MemberNode = new MemberNode()
                {
                    TableName = TableNameResolver.GetTableName(memberExpression),
                    FieldName = ColumnNameResolver.GetColumnName(memberExpression)
                },
                Value = new ValueNode()
                {
                    Value = value
                }
            };
        }
    }
}