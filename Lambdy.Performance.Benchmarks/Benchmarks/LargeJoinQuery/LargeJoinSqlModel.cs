using Lambdy.Performance.Benchmarks.EfCoreContext;

namespace Lambdy.Performance.Benchmarks.Benchmarks.LargeJoinQuery
{
    public class LargeJoinSqlModel
    {
        public Product Product { get; set; }
        
        public OrderDetail OrderDetail { get; set; }
        
        public Order OrderTable { get; set; }
        
        public Supplier Supplier { get; set; }
        
        public Category Category { get; set; }
        
        public Employee Employee { get; set; }
        
        public EmployeeTerritory EmployeeTerritory { get; set; }
        
        public Territory Territory { get; set; }
        
        public Customer Customer { get; set; }
    }
}