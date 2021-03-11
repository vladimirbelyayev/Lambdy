using System.Collections.Generic;

namespace Lambdy.Parameters
{
    internal class ParameterTracker
    {
        public Dictionary<string, object> Parameters { get; } = new Dictionary<string, object>();

        private int _paramIndex;

        public string AddParameter(object value)
        {
            var param = GetNextParameterName();
            Parameters.Add(param, value);
            return param;
        }

        private string GetNextParameterName()
        {
            return  $"@p{_paramIndex++}";
        }
    }
}