using System.Text;
using Lambdy.Constants.Sql.SqlLite;
using Lambdy.Strategies.SkipTake.Interfaces;

namespace Lambdy.Strategies.SkipTake
{
    internal class SqlLiteSkipTakeStrategy : ISkipTakeStrategy
    {
        private readonly StringBuilder _stringBuilder;
        public SqlLiteSkipTakeStrategy(StringBuilder stringBuilder)
        {
            _stringBuilder = stringBuilder;
        }
        
        public void AddSkipTakeText(int skipAmount, int takeAmount)
        {
            _stringBuilder.Append(SqlLiteSkipTakeKeywords.Limit);
            _stringBuilder.Append(' ');
            _stringBuilder.Append(takeAmount);
            _stringBuilder.Append(' ');
            _stringBuilder.Append(SqlLiteSkipTakeKeywords.Offset);
            _stringBuilder.Append(' ');
            _stringBuilder.Append(skipAmount);
        }
    }
}