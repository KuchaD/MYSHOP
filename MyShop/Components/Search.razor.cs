using LumexUI;
using Microsoft.AspNetCore.Components;
using MyShop.Pages;
using MyShop.Pages.Models;
using MyShop.Product;

namespace MyShop.Components;

public partial class Search : ComponentBase
{
    [CascadingParameter] Home Home { get; set; }

    private LumexTextbox txtSearch { get; set; }

    private string? _value = string.Empty;

    private void OnProductScan(string? ean)
    {
        if (string.IsNullOrWhiteSpace(ean))
            return;

        Home.SelectProduct(ean);
        _value = string.Empty;
        Home.ChangeStep(HomeStep.PAYMENT);
    }

    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);
        txtSearch.Autofocus = true;
    }

}