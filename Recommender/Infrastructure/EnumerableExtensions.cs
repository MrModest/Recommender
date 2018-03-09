using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recommender.Infrastructure
{
    public static class EnumerableExtensions
    {

        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> source, int count)
        {
            return source
              .Select((x, y) => new { Index = y, Value = x })
              .GroupBy(x => x.Index / count)
              .Select(x => x.Select(y => y.Value).ToList())
              .ToList();
        }

    }
}
