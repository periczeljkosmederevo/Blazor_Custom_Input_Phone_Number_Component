namespace PhoneNumberComponentLibrary;
using System.ComponentModel;
using System.Reflection;

public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());

        var attribute = field?.GetCustomAttribute<DescriptionAttribute>();

        var description = attribute == null
                            ? value.ToString()
                            : attribute.Description;

        return description;
    }
}
