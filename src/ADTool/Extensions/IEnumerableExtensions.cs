using System.Collections.Generic;
using System.Linq;

namespace ADTool.Extensions
{
    public static class IEnumerableExtensions
    {
        public static bool IsEqual(this IEnumerable<string> lh, IEnumerable<string> rh)
        {
            return lh.Count() == rh.Count() && !lh.Except(rh).Any();
        }
    }
}