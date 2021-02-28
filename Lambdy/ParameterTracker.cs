using System.Collections.Generic;

namespace Lambdy
{
    internal class ParameterTracker
    {
        private readonly Dictionary<string, object> _parameters = new Dictionary<string, object>();

        public IReadOnlyDictionary<string, object> Parameters => _parameters;
        
        private int _paramIndex;

        public void AddParameter(string name, object value)
        {
            _parameters.Add(name, value);
        }

        public string AddParameter(object value)
        {
            var param = GetNextParameterName();
            _parameters.Add(param, value);
            return param;
        }

        public string GetNextParameterName()
        {
            return  $"@p_{_paramIndex++}";
        }
    }
}