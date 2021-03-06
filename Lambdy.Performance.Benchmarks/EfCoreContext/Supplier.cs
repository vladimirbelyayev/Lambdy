﻿// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable ClassNeverInstantiated.Global
#nullable disable

namespace Lambdy.Performance.Benchmarks.EfCoreContext
{
    public partial class Supplier
    {
        public long Id { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string HomePage { get; set; }
    }
}
