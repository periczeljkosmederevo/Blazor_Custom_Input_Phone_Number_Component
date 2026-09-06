using PhoneNumberComponentLibrary.Enums;

namespace PhoneNumberComponentLibrary.Models;

/// <summary>
/// Represents a country entity containing geographical, code identifiers, and dynamic flag asset path mappings.
/// </summary>
public class Country
{
    /// <summary>
    /// Gets or sets the unique database or system identifier for the country.
    /// </summary>
    public int? Id { get; set; } = null;

    /// <summary>
    /// Gets or sets the foreign key identifier linking the country to its respective continent.
    /// </summary>
    public int? ContinentId { get; set; } = null;

    /// <summary>
    /// Gets or sets the display name of the continent associated with the country.
    /// </summary>
    public string ContinentName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the official display name of the country.
    /// </summary>
    public string CountryName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the international telephone dialing call prefix code (e.g., "+381").
    /// </summary>
    public string CountryCallCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the two-letter ISO 3166-1 alpha-2 country code (e.g., "rs").
    /// </summary>
    public string CountryIsoAlpha2 { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the three-letter ISO 3166-1 alpha-3 country code.
    /// </summary>
    public string CountryIsoAlpha3 { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the numeric ISO 3166-1 country code value.
    /// </summary>
    public int? CountryNumericCode { get; set; } = null;

    /// <summary>
    /// Generates the static web asset file path pointing to the corresponding country flag image vector based on the specified format layout.
    /// </summary>
    /// <param name="format">The dimension layout format (<see cref="FlagFormatEnum"/>) for the flag asset.</param>
    /// <returns>A string representing the relative asset content path, or an empty string if the ISO code is missing.</returns>
    public string FlagPath(FlagFormatEnum format) =>
        string.IsNullOrEmpty(CountryIsoAlpha2)
            ? string.Empty
            : format == FlagFormatEnum.Square1x1
                ? $"_content/PhoneNumberInput.Blazor/flags/1x1/{CountryIsoAlpha2.ToLower()}.svg"
                : "_content/PhoneNumberInput.Blazor/flags/4x3/" + CountryIsoAlpha2.ToLower() + ".svg";
}