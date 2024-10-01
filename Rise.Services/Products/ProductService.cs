using Microsoft.EntityFrameworkCore;
using Rise.Persistence;
using Rise.Services.Auth;
using Rise.Shared.Auth;
using Rise.Shared.Products;

namespace Rise.Services.Products;

public class ProductService : IProductService
{
    private readonly ApplicationDbContext dbContext;
    private readonly IAuthContextProvider authContextProvider;

    public ProductService(ApplicationDbContext dbContext, IAuthContextProvider authContextProvider)
    {
        if (authContextProvider.User is null)
            throw new ArgumentNullException($"{nameof(ProductService)} requires a {nameof(authContextProvider)}");

        this.dbContext = dbContext;
        this.authContextProvider = authContextProvider;
    }

    public async Task<IEnumerable<ProductDto>> GetProductsAsync()
    {
        Console.WriteLine("Logging from Services");
        Console.WriteLine("=====================================");
        foreach (var claim in authContextProvider.User!.Claims)
        {
            Console.WriteLine($"{claim.Type}: {claim.Value}");
        }
        Console.WriteLine("User is in role 'Administrator': {0}", authContextProvider.User.IsInRole("Administrator"));
        Console.WriteLine("User is {0}", authContextProvider.User.Identity!.Name);
        Console.WriteLine("=====================================");

        IQueryable<ProductDto> query = dbContext.Products.Select(x => new ProductDto
        {
            Id = x.Id,
            Name = x.Name,
        });

        var products = await query.ToListAsync();

        return products;
    }
}