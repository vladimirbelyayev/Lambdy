using System.Collections.Generic;

namespace Lambdy
{
    public class LambdyResult
    {
        public string Sql { get; set; }
        
        public Dictionary<string, object> Parameters { get; set; }
    }
}