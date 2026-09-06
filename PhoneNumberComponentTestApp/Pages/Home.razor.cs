using Microsoft.AspNetCore.Components.Forms;
using PhoneNumberComponentLibrary;

namespace PhoneNumberComponentTestApp.Pages;

public partial class Home
{
    public PhoneNumber VM = new();


    override protected void OnInitialized()
    {

    }

    public void OnSubmit(EditContext ec)
    {
        bool valid = ec.Validate();
    }
}
