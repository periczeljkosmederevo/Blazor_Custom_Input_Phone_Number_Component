using PhoneNumberComponentLibrary.Enums;

namespace PhoneNumberComponentLibrary;

public class Country
{
    public int? Id { get; set; } = null;
    public int? ContinentId { get; set; } = null;
    public string ContinentName { get; set; } = string.Empty;

    public string CountryName { get; set; } = string.Empty;
    public string CountryCallCode { get; set; } = string.Empty;
    public string CountryIsoAlpha2 { get; set; } = string.Empty;
    public string CountryIsoAlpha3 { get; set; } = string.Empty;
    public int? CountryNumericCode { get; set; } = null;

    public string FlagPath(FlagFormatEnum format) =>
        string.IsNullOrEmpty(CountryIsoAlpha2)
            ? string.Empty
            : format == FlagFormatEnum.Square1x1
                ? $"_content/PhoneNumberInput.Blazor/flags/1x1/{CountryIsoAlpha2.ToLower()}.svg"
                : "_content/PhoneNumberInput.Blazor/flags/4x3/" + CountryIsoAlpha2.ToLower() + ".svg";
}