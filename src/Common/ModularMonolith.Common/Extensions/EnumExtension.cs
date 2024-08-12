using System.ComponentModel;

namespace ModularMonolith.Common.Extensions;

public static class EnumExtension
{
    public static string Description<TEnum>(this TEnum @enum)
        where TEnum : Enum
    {
        var memberInfo = typeof(TEnum).GetMember(@enum.ToString());
        var attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

        return ((DescriptionAttribute)attributes[0]).Description;
    }
}
