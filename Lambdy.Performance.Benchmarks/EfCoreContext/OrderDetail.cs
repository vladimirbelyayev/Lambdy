using System;
using System.Collections.Generic;

#nullable disable

namespace Lambdy.Performance.Benchmarks.EfCoreContext
{
    public partial class OrderDetail
    {
        public string Id { get; set; }
        public long OrderId { get; set; }
        public long ProductId { get; set; }
        public Decimal UnitPrice { get; set; }
        public long Quantity { get; set; }
        public double Discount { get; set; }
    }
}
