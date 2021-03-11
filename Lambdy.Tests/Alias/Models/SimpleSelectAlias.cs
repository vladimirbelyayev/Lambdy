using Lambdy.Tests.TestModels.Tables;

namespace Lambdy.Tests.Alias.Models
{
    public class SimpleSelectAlias
    {
        public Person PersonAlias { get; set; }
        
        public Address AddressAlias { get; set; }
        
        public TableC TableC { get; set; }
    }
}