using System;
using System.Collections.Generic;

#nullable disable

namespace Lambdy.Performance.Benchmarks.EfCoreContext
{
    public partial class EmployeeTerritory
    {
        public string Id { get; set; }
        public long EmployeeId { get; set; }
        public string TerritoryId { get; set; }
    }
}
