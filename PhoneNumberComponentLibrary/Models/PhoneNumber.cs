namespace PhoneNumberComponentLibrary.Models;

public class PhoneNumber
{
    public string Number { get; set; } = string.Empty;

    public string FormatedNumber =>
        Country != null
            ? $"(+{Country.CountryCallCode}) {Number}"
            : Number;

    public Country? Country { get; set; } = null;
}
