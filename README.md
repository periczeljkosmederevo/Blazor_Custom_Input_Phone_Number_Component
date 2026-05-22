# Blazor Custom Input Phone Number Component

This library provides a **reusable Blazor component** for phone number input. 
The component integrates logic for continent and country selection, 
automatic filtering of the country list, and visual validation, 
making data entry simpler and more intuitive for end users.

## 🚀 Key Features
* **Dynamic Filtering:** Automatically updates the list of available countries based on the selected continent.
* **Two-Way Data Binding:** Full support for `@bind` syntax for phone numbers, countries, and continents.
* **Customizable Validation:** Visual feedback through styled input borders based on the `required` status.
* **Modularity:** Option to include or exclude dropdown menus via component parameters.
* **Modern UI:** Built on the Bootstrap 5 framework for a responsive and consistent look.

## 🛠️ Installation and API Reference

To use the component, add a reference to the `PhoneNumberComponentLibrary` and ensure Bootstrap 5 CSS/JS is loaded in your application.


### Usage Example

<PhoneNumberInput 
    @bind-PhoneNumber="userPhone" 
    @bind-SelectedCountry="userCountry"
    IncludeContinentSelect="true"
    IsRequired="true" />

### Component Parameters
The component supports the following configuration parameters:
- **IncludeContinentSelect** (bool, default: true): Shows or hides the continent dropdown.
- **IncludeCountrySelect** (bool, default: true): Shows or hides the country dropdown.
- **IsRequired** (bool?, default: false): Enables visual validation by applying red/green borders based on input state.
- **Placeholder** (string, default: "Enter phone number"): Custom text displayed when the input is empty.
- **AutoComplete** (string, default: "off"): Sets the standard HTML autocomplete attribute.

### 📂 Project Structure
* **/PhoneNumberComponentLibrary**: Source code for the component, models, and Enum extensions.
* **/PhoneNumberComponentTestApp**: Blazor application for testing and demonstrating the component functionality.

### 📄 License
This project is licensed under the **CC0-1.0** license (Public Domain). Feel free to use, modify, and distribute the code without any restrictions.
