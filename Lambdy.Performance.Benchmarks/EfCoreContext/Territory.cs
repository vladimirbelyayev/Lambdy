using System;
using System.Collections.Generic;

#nullable disable

namespace Lambdy.Performance.Benchmarks.EfCoreContext
{
    public partial class Territory
    {
        public string Id { get; set; }
        public string TerritoryDescription { get; set; }
        public long RegionId { get; set; }
    }
}
