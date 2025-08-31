using LumexUI.Extensions;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MongoDB.Driver;
using MyShop;
using MyShop.Product;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddLumexServices();

builder.Services.AddScoped<IMongoDatabase>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var mongoSetting = configuration.GetSection("MongoDb");

    var client = new MongoClient(mongoSetting.Value);
    var database = client.GetDatabase("MyShop");
    return database;
});


builder.Services.AddProductModule();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();