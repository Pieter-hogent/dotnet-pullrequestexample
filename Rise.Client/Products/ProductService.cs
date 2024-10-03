using Rise.Shared.Products;
using System.Net.Http.Json;

namespace Rise.Client.Products;

public class ProductService : IProductService
{
    private readonly HttpClient httpClient;

    public ProductService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public Task<ProductDto> GetProductAsync(int productId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<ProductDto>> GetProductsAsync()
    {
        var products = await httpClient.GetFromJsonAsync<IEnumerable<ProductDto>>("product");
        return products!;
    }
}
