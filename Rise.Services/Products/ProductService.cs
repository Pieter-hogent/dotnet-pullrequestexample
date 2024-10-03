using Microsoft.EntityFrameworkCore;
using Rise.Persistence;
using Rise.Shared.Products;

namespace Rise.Services.Products;

public class ProductService : IProductService
{
    private readonly ApplicationDbContext dbContext;

    public ProductService(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<ProductDto> GetProductAsync(int productId)
    {
        var product = await dbContext.Products.Select(x => new ProductDto{ Id = x.Id, Name = x.Name}).SingleAsync(p => p.Id == productId);
        return product;
    }

    public async Task<IEnumerable<ProductDto>> GetProductsAsync()
    {
        IQueryable<ProductDto> query = dbContext.Products.Select(x => new ProductDto
        {
            Id = x.Id,
            Name = x.Name,
        });

        var products = await query.ToListAsync();

        return products;
    }
}