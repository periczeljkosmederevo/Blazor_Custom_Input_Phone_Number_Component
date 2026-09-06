using System.ComponentModel;

namespace PhoneNumberComponentLibrary.Enums;

/// <summary>
/// Specifies the primary global continents with descriptive display strings for localization and user interfaces.
/// </summary>
public enum ContinentEnum
{
    /// <summary>
    /// Represents the continent of Africa.
    /// </summary>
    [Description("Africa")]
    Africa = 1,

    /// <summary>
    /// Represents the continent of Antarctica.
    /// </summary>
    [Description("Antarctica")]
    Antarctica = 2,

    /// <summary>
    /// Represents the continent of Asia.
    /// </summary>
    [Description("Asia")]
    Asia = 3,

    /// <summary>
    /// Represents the continent of Europe.
    /// </summary>
    [Description("Europe")]
    Europe = 4,

    /// <summary>
    /// Represents the continent of North America.
    /// </summary>
    [Description("North America")]
    NorthAmerica = 5,

    /// <summary>
    /// Represents the continent of Oceania.
    /// </summary>
    [Description("Oceania")]
    Oceania = 6,

    /// <summary>
    /// Represents the continent of South America.
    /// </summary>
    [Description("South America")]
    SouthAmerica = 7
}