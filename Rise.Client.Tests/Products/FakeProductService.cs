using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rise.Shared.Products;

namespace Rise.Client.Products;

public class FakeProductService : IProductService
{
    public Task<IEnumerable<ProductDto>> GetProductsAsync()
    {
        var products = Enumerable.Range(1, 5)
                                 .Select(i => new ProductDto { Id = i, Name = $"Product {i}" });

        return Task.FromResult(products);
    }

    public Task<ProductDto> GetProductAsync(int productId)
    {
	    var product = new ProductDto{Id = 7, Name = $"Product 7"};
	    return Task.FromResult(product);
    }
}

