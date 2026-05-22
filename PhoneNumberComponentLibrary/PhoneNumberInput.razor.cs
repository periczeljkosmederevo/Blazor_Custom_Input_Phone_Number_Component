using Microsoft.AspNetCore.Components;

namespace PhoneNumberComponentLibrary;

public partial class PhoneNumberInput : ComponentBase
{
    #region Continent Select
    [Parameter]
    public bool IncludeContinentSelect { get; set; } = true;

    [Parameter]
    public ContinentEnum? SelectedContinent { get; set; } = null;

    [Parameter]
    public EventCallback<ContinentEnum?> SelectedContinentChanged { get; set; }

    private async Task OnContinentSelect(ContinentEnum? continent)
    {
        if (SelectedContinent != continent)
        {
            SelectedContinent = continent;

            SelectedCountry = null;

            UpdateFilteredCountries();

            SelectedCountry = FilteredCountries.FirstOrDefault();

            await SelectedContinentChanged.InvokeAsync(continent);

            await SelectedCountryChanged.InvokeAsync(SelectedCountry);
        }
    }
    #endregion

    #region Country Select
    [Parameter]
    public bool IncludeCountrySelect { get; set; } = true;

    [Parameter]
    public Country? SelectedCountry { get; set; } = null;

    [Parameter]
    public EventCallback<Country?> SelectedCountryChanged { get; set; }

    private async Task OnCountrySelect(Country? country)
    {
        if (country?.ContinentId != null && SelectedContinent != (ContinentEnum)country.ContinentId)
        {
            SelectedContinent = (ContinentEnum)country.ContinentId;

            UpdateFilteredCountries();
        }
        else if (country?.ContinentId == null)
        {
            SelectedContinent = null;
        }

        SelectedCountry = country;

        await SelectedCountryChanged.InvokeAsync(country);

        await SelectedContinentChanged.InvokeAsync(SelectedContinent);
    }

    private List<Country> FilteredCountries { get; set; } = new List<Country>();

    private void UpdateFilteredCountries()
    {
        if (IncludeContinentSelect && SelectedContinent != null)
        {
            // Filter countries by selected continent
            FilteredCountries = CountryData.GetCountries()
                                           .Where(c => c.ContinentId == (int)SelectedContinent)
                                           .ToList();
        }
        else
        {
            // Show all countries if continent filtering is not applied
            FilteredCountries = CountryData.GetCountries();
        }
    }
    #endregion

    #region Phone Number
    [Parameter]
    public string PhoneNumber { get; set; } = string.Empty;

    [Parameter]
    public EventCallback<string> PhoneNumberChanged { get; set; }

    [Parameter]
    public bool? IsRequired { get; set; } = false;
    private string? Border => IsRequired != null
                             ?
                                   IsRequired == true
                                   ? PhoneNumber == null || PhoneNumber == string.Empty
                                           ? "border-3 border-danger"
                                           : "border-2 border-success"
                                   : "border-1 border-primary"
                             : string.Empty;

    [Parameter]
    public string? TextAllign { get; set; } = "text-center";

    [Parameter]
    public string? Placeholder { get; set; } = "Enter phone number";

    public bool RequiredPlaceholder => IsRequired == true && (PhoneNumber == null || PhoneNumber == string.Empty);

    [Parameter]
    public string? AutoComplete { get; set; } = "off";

    [Parameter]
    public bool? ShowStandardRequiredMessage { get; set; } = false;

    [Parameter]
    public string? RequiredValueValidationMessage { get; set; } =
        "Phone number is required";

    private async Task OnPhoneNumberChange(ChangeEventArgs e)
    {
        PhoneNumber = e.Value?.ToString() ?? string.Empty;
        await PhoneNumberChanged.InvokeAsync(PhoneNumber);
    }
    #endregion

    protected override void OnInitialized()
    {
        UpdateFilteredCountries();
    }

    protected override void OnParametersSet()
    {
        // Automatically set the continent if a country is passed in
        if (SelectedCountry?.ContinentId != null && SelectedContinent == null)
        {
            SelectedContinent = (ContinentEnum)SelectedCountry.ContinentId;
            UpdateFilteredCountries();
        }
    }
}
