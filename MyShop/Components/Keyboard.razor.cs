using Microsoft.AspNetCore.Components;

namespace MyShop.Components;

public partial class Keyboard : ComponentBase
{
    private string InputValue { get; set; } = "";

    private string DispleyValue => string.IsNullOrEmpty(InputValue) ? "" : InputValue + " Kč";

    [Parameter]
    public EventCallback<string> OnConfirm { get; set; }

    private void AddDigit(int digit)
    {
        InputValue += digit.ToString();
    }

    private void ClearInput()
    {
        InputValue = "";
    }

    private async Task ConfirmInput()
    {
        if (!string.IsNullOrEmpty(InputValue))
        {
            await OnConfirm.InvokeAsync(InputValue);
            InputValue = ""; // volitelně vyčistit po odeslání
        }
    }
}