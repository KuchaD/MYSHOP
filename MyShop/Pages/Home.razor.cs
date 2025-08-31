using Microsoft.AspNetCore.Components;
using MyShop.Pages.Models;
using MyShop.Product;

namespace MyShop.Pages;

public partial class Home : ComponentBase
{
    public HomeStep CurrentStep { get; set; } = HomeStep.SEARCH;

    public string? SelectedProductGTIN { get; set; }

    public void ChangeStep(HomeStep step)
    {
        CurrentStep = step;
        StateHasChanged();
    }

    public void SelectProduct(string product)
    {
        SelectedProductGTIN = product;
        StateHasChanged();
    }
}