using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Rise.Client.Products;
using Rise.Shared.Products;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IProductService, ProductService>();

await builder.Build().RunAsync();
