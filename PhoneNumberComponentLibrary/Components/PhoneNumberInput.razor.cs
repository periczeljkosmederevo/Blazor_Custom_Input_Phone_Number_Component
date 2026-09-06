using Microsoft.AspNetCore.Components;
using PhoneNumberComponentLibrary.Data;
using PhoneNumberComponentLibrary.Enums;
using PhoneNumberComponentLibrary.Models;

namespace PhoneNumberComponentLibrary.Components;

/// <summary>
/// A customizable Blazor component for phone number input featuring integrated continent and country selection dropdowns, flag rendering, and granular styling controls.
/// </summary>
public partial class PhoneNumberInput : ComponentBase
{
    #region Advanced Custom Styling Parameters

    /// <summary>
    /// Gets or sets custom CSS classes for the main wrapper container element.
    /// </summary>
    [Parameter]
    public string? CustomContainerClass { get; set; }

    /// <summary>
    /// Gets or sets custom CSS classes for the continent selection dropdown button.
    /// </summary>
    [Parameter]
    public string? CustomContinentButtonClass { get; set; }

    /// <summary>
    /// Gets or sets custom CSS classes for the country selection dropdown button.
    /// </summary>
    [Parameter]
    public string? CustomCountryButtonClass { get; set; }

    /// <summary>
    /// Gets or sets custom CSS classes for the phone number text input field.
    /// </summary>
    [Parameter]
    public string? CustomInputClass { get; set; }

    /// <summary>
    /// Gets or sets custom CSS classes for the dropdown menus.
    /// </summary>
    [Parameter]
    public string? CustomDropdownMenuClass { get; set; }

    /// <summary>
    /// Gets or sets the minimum CSS width constraint for the continent dropdown button. Defaults to "4rem".
    /// </summary>
    [Parameter]
    public string ContinentMinWidth { get; set; } = "4rem";

    /// <summary>
    /// Gets or sets the maximum CSS width constraint for the continent dropdown button.
    /// </summary>
    [Parameter]
    public string? ContinentMaxWidth { get; set; }

    /// <summary>
    /// Gets or sets the minimum CSS width constraint for the country dropdown button. Defaults to "4rem".
    /// </summary>
    [Parameter]
    public string CountryMinWidth { get; set; } = "4rem";

    /// <summary>
    /// Gets or sets the maximum CSS width constraint for the country dropdown button.
    /// </summary>
    [Parameter]
    public string? CountryMaxWidth { get; set; }

    /// <summary>
    /// Gets or sets the minimum CSS width constraint for the phone number input field.
    /// </summary>
    [Parameter]
    public string? InputMinWidth { get; set; }

    /// <summary>
    /// Gets or sets the maximum CSS width constraint for the phone number input field.
    /// </summary>
    [Parameter]
    public string? InputMaxWidth { get; set; }

    #endregion

    #region Flags Configuration

    /// <summary>
    /// Gets or sets a value indicating whether country flags should be displayed next to options. Defaults to true.
    /// </summary>
    [Parameter]
    public bool ShowFlags { get; set; } = true;

    /// <summary>
    /// Gets or sets the image format layout for country flags. Defaults to Rectangle4x3.
    /// </summary>
    [Parameter]
    public FlagFormatEnum FlagFormat { get; set; } = FlagFormatEnum.Rectangle4x3;

    /// <summary>
    /// Gets or sets the CSS width for the rendered flag image. Defaults to "20px".
    /// </summary>
    [Parameter]
    public string FlagWidth { get; set; } = "20px";

    /// <summary>
    /// Gets or sets the CSS height for the rendered flag image. Defaults to "15px".
    /// </summary>
    [Parameter]
    public string FlagHeight { get; set; } = "15px";

    /// <summary>
    /// Gets the resolved file path string for the currently selected country's flag icon.
    /// </summary>
    public string? SelectedCountryFlagPath => SelectedCountry?.FlagPath(FlagFormat);

    #endregion

    #region Continent Select

    /// <summary>
    /// Gets or sets a value indicating whether the continent selection dropdown is visible. Defaults to true.
    /// </summary>
    [Parameter]
    public bool IncludeContinentSelect { get; set; } = true;

    /// <summary>
    /// Gets or sets the currently selected continent item.
    /// </summary>
    [Parameter]
    public ContinentEnum? SelectedContinent { get; set; } = null;

    /// <summary>
    /// Callback invoked when the active continent selection changes.
    /// </summary>
    [Parameter]
    public EventCallback<ContinentEnum?> SelectedContinentChanged { get; set; }

    /// <summary>
    /// Handles internal state adjustments and event dispatches when a specific continent is chosen.
    /// </summary>
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

    /// <summary>
    /// Gets or sets a value indicating whether the country selection dropdown is visible. Defaults to true.
    /// </summary>
    [Parameter]
    public bool IncludeCountrySelect { get; set; } = true;

    /// <summary>
    /// Gets or sets the currently selected country model.
    /// </summary>
    [Parameter]
    public Country? SelectedCountry { get; set; } = null;

    /// <summary>
    /// Callback invoked when the active country selection changes.
    /// </summary>
    [Parameter]
    public EventCallback<Country?> SelectedCountryChanged { get; set; }

    /// <summary>
    /// Updates component state when a country is selected, auto-syncing the parent continent if necessary.
    /// </summary>
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

    /// <summary>
    /// Gets or sets the working list of countries filtered according to the selected continent.
    /// </summary>
    private List<Country> FilteredCountries { get; set; } = new List<Country>();

    /// <summary>
    /// Refreshes the local country collection based on the current continent filter configuration.
    /// </summary>
    /// <summary>
    /// Refreshes the local country collection based on the current continent filter configuration and sorts them alphabetically.
    /// </summary>
    private void UpdateFilteredCountries()
    {
        if (IncludeContinentSelect && SelectedContinent != null)
        {
            // Filter countries by selected continent and sort alphabetically by name
            FilteredCountries = CountryData.GetCountries()
                                           .Where(c => c.ContinentId == (int)SelectedContinent)
                                           .OrderBy(c => c.CountryName)
                                           .ToList();
        }
        else
        {
            // Show all countries if continent filtering is not applied, sorted alphabetically by name
            FilteredCountries = CountryData.GetCountries()
                                           .OrderBy(c => c.CountryName)
                                           .ToList();
        }
    }
    #endregion

    #region Phone Number

    /// <summary>
    /// Gets or sets the raw phone number string value entered by the user.
    /// </summary>
    [Parameter]
    public string PhoneNumber { get; set; } = string.Empty;

    /// <summary>
    /// Callback triggered whenever the phone number input value changes.
    /// </summary>
    [Parameter]
    public EventCallback<string> PhoneNumberChanged { get; set; }

    /// <summary>
    /// Gets or sets validation state indicating whether the input field is marked as required. Defaults to false.
    /// </summary>
    [Parameter]
    public bool? IsRequired { get; set; } = false;

    /// <summary>
    /// Computes contextual Bootstrap border modifier classes depending on validation parameters and field status.
    /// </summary>
    private string? Border => IsRequired != null
                             ?
                                   IsRequired == true
                                   ? PhoneNumber == null || PhoneNumber == string.Empty
                                           ? "border-3 border-danger"
                                           : "border-2 border-success"
                                   : "border-1 border-primary"
                             : string.Empty;

    /// <summary>
    /// Gets or sets the CSS alignment class applied to input text. Defaults to "text-center".
    /// </summary>
    [Parameter]
    public string? TextAllign { get; set; } = "text-center";

    /// <summary>
    /// Gets or sets the placeholder text displayed inside the phone input box.
    /// </summary>
    [Parameter]
    public string? Placeholder { get; set; } = "Enter phone number";

    /// <summary>
    /// Gets a value indicating whether the required status placeholder warning should be shown.
    /// </summary>
    public bool RequiredPlaceholder => IsRequired == true && (PhoneNumber == null || PhoneNumber == string.Empty);

    /// <summary>
    /// Gets or sets the browser autocomplete behavior flag. Defaults to "off".
    /// </summary>
    [Parameter]
    public string? AutoComplete { get; set; } = "off";

    /// <summary>
    /// Gets or sets a value indicating whether to show standard required indicator messaging. Defaults to false.
    /// </summary>
    [Parameter]
    public bool? ShowStandardRequiredMessage { get; set; } = false;

    /// <summary>
    /// Gets or sets the warning message text displayed when a required phone number value is missing.
    /// </summary>
    [Parameter]
    public string? RequiredValueValidationMessage { get; set; } =
        "Phone number is required";

    /// <summary>
    /// Event handler executing when the underlying input value is altered by browser interaction.
    /// </summary>
    private async Task OnPhoneNumberChange(ChangeEventArgs e)
    {
        PhoneNumber = e.Value?.ToString() ?? string.Empty;
        await PhoneNumberChanged.InvokeAsync(PhoneNumber);
    }

    #endregion

    /// <summary>
    /// Performs component initialization tasks, populating the initial country dataset.
    /// </summary>
    protected override void OnInitialized()
    {
        UpdateFilteredCountries();
    }

    /// <summary>
    /// Handles component lifecycle adjustments when new parameter values arrive from parents.
    /// </summary>
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