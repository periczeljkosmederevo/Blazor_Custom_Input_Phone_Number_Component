namespace PhoneNumberComponentLibrary.Models;

/// <summary>
/// Represents a structured phone number entity containing the local subscriber number and its associated country details.
/// </summary>
public class PhoneNumber
{
    /// <summary>
    /// Gets or sets the local subscriber phone number digits string value.
    /// </summary>
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// Gets the formatted full phone number string incorporating the international calling prefix if a country is associated.
    /// </summary>
    public string FormatedNumber =>
        Country != null
            ? $"(+{Country.CountryCallCode}) {Number}"
            : Number;

    /// <summary>
    /// Gets or sets the country model linked to the phone number for international formatting and prefix resolution.
    /// </summary>
    public Country? Country { get; set; } = null;
}