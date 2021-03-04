using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Lambdy.Constants.Sql;
using Lambdy.Parameters;
using Lambdy.TreeNodes.ExpressionNodes;
using Lambdy.ValueObjects;
using Lambdy.Visitors.ExpressionNodeSql.Abstract;

namespace Lambdy.Visitors.ExpressionNodeSql
{
    //TODO: Use string builder instead of return/Concat string in recursive methods
    internal class ExpressionNodeSqlVisitor : ExpressionNodeVisitor<string>
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
                {ExpressionType.Not, SqlBooleanLogicalOperators.Not}
            };
        
        private static readonly IReadOnlyDictionary<LikeMethod, string> LikeDictionary =
            new Dictionary<LikeMethod, string>()
            {
                {LikeMethod.Contains, $"{SqlComparisionOperators.Like} {SqlFunctions.Concat}('%',{0},'%')"},
                {LikeMethod.Equals, $"{SqlComparisionOperators.Like} {0}"},
                {LikeMethod.EndsWith, $"{SqlComparisionOperators.Like} {SqlFunctions.Concat}({0},'%')"},
                {LikeMethod.StartsWith,$"{SqlComparisionOperators.Like} {SqlFunctions.Concat}('%',{0})"},
            };

        // ReSharper disable once NotAccessedField.Local This will be used later on
        private readonly StringBuilder _stringBuilder;

        public ExpressionNodeSqlVisitor(StringBuilder stringBuilder)
        {
            _stringBuilder = stringBuilder;
        }
        
        public ParameterTracker ParameterTracker { get; private set; }

        public void SetParameterTracker(ParameterTracker parameterTracker)
        {
            ParameterTracker = parameterTracker;
        }
        
        public override string VisitInNode(InNode inNode)
        {
            return $"{inNode.MemberNode.Accept(this)} " +
                   $"{SqlComparisionOperators.In} " +
                   $"{inNode.Value.Accept(this)}";
        }

        public override string VisitLikeNode(LikeNode likeNode)
        {
            var likeSqlExpression = string.Format(
                LikeDictionary[likeNode.Method],
                likeNode.Value.Accept(this));
            
            return $"{likeNode.MemberNode.Accept(this)} " +
                   $"{likeSqlExpression}";
        }

        public override string VisitMemberNode(MemberNode memberNode)
        {
            return $"{memberNode.TableName}.{memberNode.FieldName}";
        }

        public override string VisitOperationNode(OperationNode operationNode)
        {
            return $"{operationNode.Left.Accept(this)} " +
                   $"{OperationDictionary[operationNode.Operator]} " +
                   $"{operationNode.Right.Accept(this)}";
        }

        public override string VisitSingleOperationNode(SingleOperationNode singleOperationNode)
        {
            return $"{OperationDictionary[singleOperationNode.Operator]} " +
                   $"{singleOperationNode.Child.Accept(this)}";
        }

        public override string VisitValueNode(ValueNode valueNode)
        {
            return ParameterTracker.AddParameter(valueNode.Value);
        }

        public override string VisitConstructorNode(ConstructorNode constructorNode)
        {
            var nodeStrings = constructorNode
                .ConstructorArgumentNodes
                .Select(x => x.Accept(this));
            
            return string.Join(
                ", ", 
                nodeStrings);
        }

        public override string VisitConstructorArgumentNode(ConstructorArgumentNode constructorArgumentNode)
        {
            return $"{constructorArgumentNode.Right.Accept(this)} " +
                   $"{SqlAliasKeywords.As} " +
                   $"{constructorArgumentNode.Left.Accept(this)}";
        }

        public override string VisitConstructorMemberNode(ConstructorMemberNode constructorNode)
        {
            return constructorNode.FieldName;
        }
    }
}