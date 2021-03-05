// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable ClassNeverInstantiated.Global
#nullable disable

namespace Lambdy.Tests.TestModels.NorthwindTables
{
    public partial class Product
    {
        public long Id { get; set; }
        public string ProductName { get; set; }
        public long SupplierId { get; set; }
        public long CategoryId { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public long UnitsInStock { get; set; }
        public long UnitsOnOrder { get; set; }
        public long ReorderLevel { get; set; }
        public long Discontinued { get; set; }
    }
}
