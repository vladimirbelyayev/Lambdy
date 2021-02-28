using System.Linq;
using System.Linq.Expressions;
using Lambdy.ExpressionNodes;
using Lambdy.ExpressionNodes.Abstract;
using Lambdy.Resolvers.NameResolvers;

namespace Lambdy.Resolvers.ExpressionResolvers.SubResolvers.MethodCall
{
    internal class InSubResolver
    {
        public Node ResolveExpression(MethodCallExpression methodCallExpression)
        {
            Expression collectionExpression;
            MemberExpression memberExpression;
            
            if (methodCallExpression.Method.DeclaringType == typeof(Enumerable))
            {
                collectionExpression = methodCallExpression.Arguments[0];
                memberExpression = methodCallExpression.Arguments[1] as MemberExpression;
            }
            else
            {
                collectionExpression = methodCallExpression.Object;
                memberExpression = methodCallExpression.Arguments[0] as MemberExpression;
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