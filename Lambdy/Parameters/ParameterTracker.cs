using System;
using System.Collections.Generic;

namespace Lambdy.Parameters
{
    internal class ParameterTracker
    {
        public Dictionary<string, object> Parameters { get; } = new Dictionary<string, object>();

        private int _paramIndex;

        private static string _paramPrefix = "@";

        public void AddParameter(
            string key,
            object value)
        {
            if (key.IndexOf(_paramPrefix, StringComparison.Ordinal) < 0)
            {
                key = $"{_paramPrefix}{key}";
            }
            
            Parameters.Add(key, value);
        }
        
        public string AddParameter(object value)
        {
            var param = GetNextParameterName();
            Parameters.Add(param, value);
            return param;
        }

        private string GetNextParameterName()
        {
            return  $"{_paramPrefix}p{_paramIndex++}";
        }
    }
}