// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable ClassNeverInstantiated.Global
#nullable disable

namespace Lambdy.Tests.TestModels.NorthwindTables
{
    public partial class OrderDetail
    {
        public string Id { get; set; }
        public long OrderId { get; set; }
        public long ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public long Quantity { get; set; }
        public double Discount { get; set; }
    }
}
