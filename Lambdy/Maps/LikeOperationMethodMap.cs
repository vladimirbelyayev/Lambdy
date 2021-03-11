using System.Collections.Generic;
using Lambdy.Constants;
using Lambdy.ValueObjects;

namespace Lambdy.Maps
{
    internal static class LikeOperationMethodMap
    {
        public static readonly IReadOnlyDictionary<string, LikeMethod> OperationMethods =
            new Dictionary<string, LikeMethod>()
            {
                { LikeMethodNames.Contains, LikeMethod.Contains },
                { LikeMethodNames.Equals, LikeMethod.Equals },
                { LikeMethodNames.EndsWith, LikeMethod.EndsWith },
                { LikeMethodNames.StartsWith, LikeMethod.StartsWith }
            };
    }
}