using System.Text;
using Lambdy.Constants.Sql.MsSql;
using Lambdy.Strategies.SkipTake.Interfaces;

namespace Lambdy.Strategies.SkipTake
{
    internal class MsSqlSkipTakeStrategy : ISkipTakeStrategy
    {
        private readonly StringBuilder _stringBuilder;
        public MsSqlSkipTakeStrategy(StringBuilder stringBuilder)
        {
            _stringBuilder = stringBuilder;
        }
        
        public void AddSkipTakeText(int skipAmount, int takeAmount)
        {
            _stringBuilder.Append(MsSqlSkipTakeKeywords.Offset);
            _stringBuilder.Append(' ');
            _stringBuilder.Append(skipAmount);
            _stringBuilder.Append(' ');
            _stringBuilder.Append(MsSqlSkipTakeKeywords.Rows);
            _stringBuilder.Append(' ');

            _stringBuilder.Append(MsSqlSkipTakeKeywords.FetchNext);
            _stringBuilder.Append(' ');
            _stringBuilder.Append(takeAmount);
            _stringBuilder.Append(' ');
            _stringBuilder.Append(MsSqlSkipTakeKeywords.RowsOnly);
        }
    }
}