using System.Collections.Generic;

namespace Lambdy
{
    public class LambdaSqlResult
    {
        public string Sql { get; set; }
        
        public IReadOnlyDictionary<string, object> Parameters { get; set; }
    }
}