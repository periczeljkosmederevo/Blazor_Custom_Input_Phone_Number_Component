using System.ComponentModel;
using System.Reflection;

namespace PhoneNumberComponentLibrary.Enums;

/// <summary>
/// Provides extension methods for enumeration types, including metadata extraction like descriptions.
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Retrieves the description string defined by the <see cref="DescriptionAttribute"/> on an enum value, 
    /// or falls back to the string representation of the enum if the attribute is absent.
    /// </summary>
    /// <param name="value">The enum value to evaluate.</param>
    /// <returns>The description string or the default enum name string.</returns>
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