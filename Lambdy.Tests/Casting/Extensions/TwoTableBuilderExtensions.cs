using System;
using Lambdy.Tests.Casting.Models;
using Lambdy;

namespace Lambdy.Tests.Casting.Extensions
{
    public static class TwoTableBuilderExtensions
    {
        public static ILambdyBuilder<TwoTableJoin> FilterTwoTable(
            this ILambdyBuilder<TwoTableJoin> builder)
        {
            builder.Where(x => x.PersonAlias.Id == Guid.Empty);
            return builder;
        }
    }
}