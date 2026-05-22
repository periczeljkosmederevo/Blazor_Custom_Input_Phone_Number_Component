namespace PhoneNumberComponentLibrary
{
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
    }
}
