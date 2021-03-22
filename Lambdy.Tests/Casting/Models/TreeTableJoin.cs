using Lambdy.Tests.TestModels.NorthwindTables;

namespace Lambdy.Tests.Casting.Models
{
    public class TreeTableJoin : TwoTableJoin
    {
        public Customer Customer { get; set; }
    }
}