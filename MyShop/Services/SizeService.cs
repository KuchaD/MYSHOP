using Microsoft.JSInterop;

namespace MyShop.Services;

public class SizeService(IJSRuntime jsRuntime)
{
    private IJSObjectReference? jsModule { get; set; }

    public async Task<WindowDimensions> GetWindowDimensions(bool firstRender)
    {
        if (firstRender)
        {
            jsModule = await jsRuntime.InvokeAsync<IJSObjectReference>("import", "./Scripts/getWindowSize.js");
        }

        if (jsModule is not null)
        {
            return await jsModule.InvokeAsync<WindowDimensions>("getWindowSize");
        }

        return new WindowDimensions();
    }
}