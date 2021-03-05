using System;
using System.Collections.Generic;

#nullable disable

namespace Lambdy.Performance.Benchmarks.EfCoreContext
{
    public partial class Category
    {
        public long Id { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
}
