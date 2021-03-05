using System;
using System.Collections.Generic;

#nullable disable

namespace Lambdy.Performance.Benchmarks.EfCoreContext
{
    public partial class Shipper
    {
        public long Id { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
    }
}
