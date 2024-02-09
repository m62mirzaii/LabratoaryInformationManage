using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Security.Claims;

namespace Share.Extentions
{
    public static class EnumExtensions
    {
        //public static string GetEnumDisplayName<T>(T value) where T : Enum
        //{
        //    var fieldName = Enum.GetName(typeof(T), value);
        //    var displayAttr = typeof(T)
        //        .GetField(fieldName)
        //        .GetCustomAttribute<DisplayAttribute>();
        //    return displayAttr?.Name ?? fieldName;
        //}

        public static string GetEnumDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetName();
        }

        public static int GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return Convert.ToInt32(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }
    } 
}
