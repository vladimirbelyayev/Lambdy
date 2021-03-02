using System.Collections.Generic;

namespace Lambdy
{
    public class LambdyResult
    {
        public string Sql { get; set; }
        
        public IReadOnlyDictionary<string, object> Parameters { get; set; }
    }
}