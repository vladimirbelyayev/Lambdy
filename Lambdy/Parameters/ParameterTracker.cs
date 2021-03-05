using System.Collections.Generic;

namespace Lambdy.Parameters
{
    internal class ParameterTracker
    {
        public Dictionary<string, object> Parameters { get; private set; } = new Dictionary<string, object>();

        private int _paramIndex;

        public void AddParameter(string name, object value)
        {
            Parameters.Add(name, value);
        }

        public string AddParameter(object value)
        {
            var param = GetNextParameterName();
            Parameters.Add(param, value);
            return param;
        }

        public string GetNextParameterName()
        {
            return  $"@p_{_paramIndex++}";
        }
    }
}