using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Core.Common.Utilities
{
    public static class PagingUtils
    {
        public static List<T> Page<T>(this IEnumerable<T> en, int page, int pageSize)
        {
            return en.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public static IQueryable<T> Page<T>(this IQueryable<T> en, int page, int pageSize)
        {
            return en.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public static IEnumerable<IEnumerable<T>> Page<T>(this IEnumerable<T> source, int pageSize)
        {
            using (var enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    var currentPage = new List<T>(pageSize)
                    {
                        enumerator.Current
                    };

                    while (currentPage.Count < pageSize && enumerator.MoveNext())
                    {
                        currentPage.Add(enumerator.Current);
                    }
                    yield return new ReadOnlyCollection<T>(currentPage);
                }
            }
        }
    }
}
