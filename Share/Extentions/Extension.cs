using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Common.Extensions;

public static class Extension
{
    private static readonly ConcurrentDictionary<Enum, string> s_cacheGetDisplayName = new ConcurrentDictionary<Enum, string>();

    public static string GetDisplayName(this Enum myEnumValue)
    {
        return s_cacheGetDisplayName.GetOrAdd(myEnumValue, v =>
        {
            var e = v.GetType()
                .GetMember(v.ToString())
                .FirstOrDefault();

            return e == null ? "" : e.GetCustomAttribute<DisplayAttribute>()?.Name;
        });
    }
}
