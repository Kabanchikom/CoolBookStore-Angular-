using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Web.Helpers;

public static class EnumFlagsConverter
{
    /// <summary>
    /// Gets the display name for an enum.
    /// </summary>
    /// <param name="enumValue"></param>
    /// <exception cref="ArgumentException"></exception>
    /// <returns></returns>
    public static List<string?> GetDisplayNames(this Enum enumValue)
    {
        var enumType = enumValue.GetType();
        var names = new List<string?>();
        foreach (var e in Enum.GetValues(enumType))
        {
            var flag = (Enum)e;              
            if(enumValue.HasFlag(flag))
            {
                names.Add(GetDisplayName(flag));
            }
        }
        if (names.Count <= 0) throw new ArgumentException();
        return names;
    }

    /// <summary>
    /// Gets the display value for a single enum flag or 
    /// name of that flag if the display value is not set
    /// </summary>
    /// <param name="flag"></param>
    /// <returns></returns>
    public static string? GetDisplayName(this Enum flag)
    {
        try
        {
            return flag.GetType()
                .GetMember(flag.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>()
                ?.Name;
        }
        catch
        {
            return flag.ToString();
        }
    }
}