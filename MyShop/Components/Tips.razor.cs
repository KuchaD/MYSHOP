using Microsoft.AspNetCore.Components;

namespace MyShop.Components;

public partial class Tips : ComponentBase
{
    [Parameter] public EventCallback<decimal> OnSelectTip { get; set; }

    [Parameter] public decimal Price { get; set; }

    private async Task OnConfirmTip(string tip)
    {
        if (!string.IsNullOrEmpty(tip))
        {
            var value = Math.Round(decimal.Parse(tip),2);
            await OnSelectTip.InvokeAsync(value);
        }
    }

    private async Task ClickTip(decimal tip)
    {
        var round = Math.Round(tip,2);
        await OnSelectTip.InvokeAsync(round);
    }


}