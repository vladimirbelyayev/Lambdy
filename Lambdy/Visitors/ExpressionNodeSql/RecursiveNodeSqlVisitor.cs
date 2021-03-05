using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Lambdy.Constants.Sql;
using Lambdy.Parameters;
using Lambdy.TreeNodes.ExpressionNodes;
using Lambdy.ValueObjects;
using Lambdy.Visitors.ExpressionNodeSql.Abstract;

namespace Lambdy.Visitors.ExpressionNodeSql
{
    internal class RecursiveNodeSqlVisitor : VoidExpressionNodeVisitor
    {
        private static readonly IReadOnlyDictionary<ExpressionType, string> OperationDictionary =
            new Dictionary<ExpressionType, string>()
            {
                {ExpressionType.Equal, SqlComparisionOperators.Equal},
                {ExpressionType.NotEqual, SqlComparisionOperators.NotEqual},
                {ExpressionType.GreaterThan, SqlComparisionOperators.GreaterThan},
                {ExpressionType.LessThan, SqlComparisionOperators.LessThan},
                {ExpressionType.GreaterThanOrEqual, SqlComparisionOperators.GreaterThanOrEqual},
                {ExpressionType.LessThanOrEqual, SqlComparisionOperators.LessThanOrEqual},
                {ExpressionType.AndAlso, SqlBooleanLogicalOperators.And},
                {ExpressionType.OrElse, SqlBooleanLogicalOperators.Or},
                {ExpressionType.Not, SqlBooleanLogicalOperators.Not},
                {ExpressionType.Convert, string.Empty} //We do not do conversion operations, just pass value!
            };
        
        // ReSharper disable once NotAccessedField.Local This will be used later on
        private readonly StringBuilder _stringBuilder;

        public RecursiveNodeSqlVisitor(StringBuilder stringBuilder)
        {
            _stringBuilder = stringBuilder;
        }
        
        public ParameterTracker ParameterTracker { get; private set; }

        public void SetParameterTracker(ParameterTracker parameterTracker)
        {
            ParameterTracker = parameterTracker;
        }

        public override void VisitInNode(InNode inNode)
        {
            inNode.MemberNode.Accept(this);
            _stringBuilder.Append(' ');
            _stringBuilder.Append(SqlComparisionOperators.In);
            _stringBuilder.Append(' ');
            inNode.Value.Accept(this);
        }

        public override void VisitLikeNode(LikeNode likeNode)
        {
            likeNode.MemberNode.Accept(this);
            _stringBuilder.Append(' ');
            _stringBuilder.Append(SqlComparisionOperators.Like);
            _stringBuilder.Append(' ');
            var isNotEquals = likeNode.Method != LikeMethod.Equals;
            var isContains = likeNode.Method == LikeMethod.Contains;
            
            if (isNotEquals)
            {
                _stringBuilder.Append(SqlFunctions.Concat);
                _stringBuilder.Append('(');
            }

            if (isContains || likeNode.Method == LikeMethod.StartsWith)
            {
                _stringBuilder.Append("'%',");
            }

            likeNode.Value.Accept(this);

            if (isContains || likeNode.Method == LikeMethod.EndsWith)
            {
                _stringBuilder.Append(",'%'");
            }

            if (isNotEquals)
            {
                _stringBuilder.Append(')');
            }
        }

        public override void VisitMemberNode(MemberNode memberNode)
        {
            _stringBuilder.Append(memberNode.TableName);
            _stringBuilder.Append('.');
            _stringBuilder.Append(memberNode.FieldName);
        }

        public override void VisitOperationNode(OperationNode operationNode)
        {
            operationNode.Left.Accept(this);
            _stringBuilder.Append(' ');
            _stringBuilder.Append(OperationDictionary[operationNode.Operator]);
            _stringBuilder.Append(' ');
            operationNode.Right.Accept(this);
        }

        public override void VisitSingleOperationNode(SingleOperationNode singleOperationNode)
        {
            _stringBuilder.Append(OperationDictionary[singleOperationNode.Operator]);
            _stringBuilder.Append(' ');
            singleOperationNode.Child.Accept(this);
        }

        public override void VisitValueNode(ValueNode valueNode)
        {
            _stringBuilder.Append(ParameterTracker.AddParameter(valueNode.Value));
        }

        public override void VisitConstructorNode(ConstructorNode constructorNode)
        {
            var argumentNodes = constructorNode.ConstructorArgumentNodes;
            var argumentLength = argumentNodes.Length;
            
            for (var i = 0; i < argumentLength; i++)
            {
                argumentNodes[i].Accept(this);
                _stringBuilder.Append(", ");
            }

            
            //_stringBuilder.Length = _stringBuilder.Length - 2;
            
            //TODO: String builder remove is slow!, need to check if we can do something faster
            _stringBuilder.Remove(_stringBuilder.Length - 2, 2);
        }

        public override void VisitConstructorArgumentNode(ConstructorArgumentNode constructorArgumentNode)
        {
            constructorArgumentNode.Right.Accept(this);
            _stringBuilder.Append(' ');
            _stringBuilder.Append(SqlAliasKeywords.As);
            _stringBuilder.Append(' ');
            constructorArgumentNode.Left.Accept(this);
        }

        public override void VisitConstructorMemberNode(ConstructorMemberNode constructorMemberNode)
        {
            _stringBuilder.Append(constructorMemberNode.FieldName);
        }
    }
}