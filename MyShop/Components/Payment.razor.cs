using LumexUI;
using Microsoft.AspNetCore.Components;
using MyShop.Pages;
using MyShop.Pages.Models;
using MyShop.Product;
using QRCoder;

namespace MyShop.Components;

public partial class Payment(IProductProxy productProxy) : ComponentBase
{
    [CascadingParameter] Home Home { get; set; }

    private bool _loading = true;

    private ProductDetail _product = new();

    private string _qrCodeImage = string.Empty;

    private decimal _tip = 0;

    public string HTMLQRCode => $"data:image/png;base64,{_qrCodeImage}";

    private async Task TipChanged(decimal tip)
    {
        _tip = tip;
        await Load();
    }

    private async Task Load()
    {
        _loading = true;
        await Task.Delay(1000); // Simulate loading delay
        if (string.IsNullOrEmpty(Home.SelectedProductGTIN))
        {
            Home.ChangeStep(HomeStep.SEARCH);
            return;
        }

        var result = await productProxy.GetProductByEan(Home.SelectedProductGTIN, _tip);
        _product = result;
        GenerateQRCode();
        _loading = false;
        StateHasChanged();
    }

    protected override Task OnInitializedAsync()
    {
        _ = Load();
        return base.OnInitializedAsync();
    }

    private void GenerateQRCode()
    {
        if (string.IsNullOrEmpty(_product.QRData))
            return;
        var qrGenerator = new QRCodeGenerator();
        var qrCodeData = qrGenerator.CreateQrCode(_product.QRData, QRCodeGenerator.ECCLevel.Q);
        var qrCode = new Base64QRCode(qrCodeData);
        _qrCodeImage = qrCode.GetGraphic(20);
    }

    private void OnBackClick()
    {
        Home.ChangeStep(HomeStep.SEARCH);
    }

}

